using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OneOf;
using PaymentGateway.Api.Acquirers;
using PaymentGateway.Api.Bank;

namespace PaymentGateway.Api.Payments.Commands;

public class PaymentHandler : IRequestHandler<PaymentRequest, OneOf<PaymentResponse, ValidationResult, Exception>>
{
    private readonly IAcquirer _acquirer;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMediator _mediator;
    private readonly IValidator<PaymentRequest> _validator;

    public PaymentHandler(IAcquirer acquirer, IPaymentRepository paymentRepository, IMediator mediator, IValidator<PaymentRequest> validator)
    {
        _acquirer = acquirer ?? throw new ArgumentNullException(nameof(acquirer));
        _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<OneOf<PaymentResponse, ValidationResult, Exception>> Handle(PaymentRequest paymentRequest, CancellationToken cancellationToken)
    {
        // validate
        var validationResult = await _validator.ValidateAsync(paymentRequest, cancellationToken);

        if (validationResult != null && !validationResult.IsValid)
        {
            return validationResult;
        }

        // send to bank
        var acquirerRequest = BuildAcquirerRequest(paymentRequest);
        var acquirerResponse = await _acquirer.ProcessPaymentAsync(acquirerRequest, cancellationToken);

        // create payment
        var payment = CreatePayment(paymentRequest, acquirerResponse);

        // store in DB
        await _paymentRepository.SaveAsync(payment);

        // dispatch event
        await _mediator.Publish(payment.ToEvent(), cancellationToken);

        // build response
        var paymentResponse = BuildPaymentResponse(payment);

        return paymentResponse;
    }

    private static AcquirerRequest BuildAcquirerRequest(PaymentRequest paymentRequest) =>
        new(
            paymentRequest.CardNumber,
            paymentRequest.ExpiryMonth,
            paymentRequest.ExpiryYear,
            paymentRequest.Amount,
            paymentRequest.Currency,
            paymentRequest.Cvv);

    private static Payment CreatePayment(PaymentRequest paymentRequest, AcquirerResponse? acquirerResponse) =>
        new(
            paymentRequest.CardNumber,
            paymentRequest.ExpiryMonth,
            paymentRequest.ExpiryYear,
            paymentRequest.Amount,
            paymentRequest.Currency,
            acquirerResponse?.Authorized,
            acquirerResponse?.AuthorizationCode,
            "");

    private static PaymentResponse BuildPaymentResponse(Payment payment) =>
        payment.Status == PaymentStatus.Authorized
            ? PaymentResponse.BuildAuthorizedResponse(payment.Id)
            : PaymentResponse.BuildDeclinedResponse(payment.Id);
}
