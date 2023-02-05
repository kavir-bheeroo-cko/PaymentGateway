using MediatR;
using OneOf;

using PaymentGateway.Api.Acquirers;
using PaymentGateway.Api.Bank;

namespace PaymentGateway.Api.Payments;

public class PaymentHandler : IRequestHandler<PaymentRequest,  OneOf<PaymentResponse, Exception>>
{
    private readonly IAcquirer _acquirer;

    public PaymentHandler(IAcquirer acquirer)
    {
        _acquirer = acquirer ?? throw new ArgumentNullException(nameof(acquirer));
    }

    public async Task<OneOf<PaymentResponse, Exception>> Handle(PaymentRequest paymentRequest, CancellationToken cancellationToken)
    {
        // validate

        // send to bank
        var acquirerRequest = BuildAcquirerRequest(paymentRequest);
        var acquirerResponse = await _acquirer.ProcessPaymentAsync(acquirerRequest, cancellationToken);

        // create payment
        var payment = CreatePayment(paymentRequest, acquirerResponse);

        // store in DB

        // dispatch event

        // build response

        throw new NotImplementedException();
    }

    private AcquirerRequest BuildAcquirerRequest(PaymentRequest paymentRequest) =>
        new(
            paymentRequest.CardNumber,
            paymentRequest.ExpiryMonth,
            paymentRequest.ExpiryYear,
            paymentRequest.Amount,
            paymentRequest.Currency,
            paymentRequest.Cvv);

    private Payment CreatePayment(PaymentRequest paymentRequest, AcquirerResponse? acquirerResponse) =>
        new(
            paymentRequest.CardNumber,
            paymentRequest.ExpiryMonth,
            paymentRequest.ExpiryYear,
            paymentRequest.Amount,
            paymentRequest.Currency,
            acquirerResponse?.Authorized,
            acquirerResponse?.AuthorizationCode,
            "");
}
