using MediatR;
using OneOf;

namespace PaymentGateway.Api.Payments.Queries;

public class PaymentQueryHandler : IRequestHandler<PaymentQueryRequest, OneOf<PaymentQueryResponse?, Exception>>
{
    private readonly IPaymentEventRepository _paymentEventRepository;

    public PaymentQueryHandler(IPaymentEventRepository paymentEventRepository)
    {
        _paymentEventRepository = paymentEventRepository ?? throw new ArgumentNullException(nameof(paymentEventRepository));
    }

    public async Task<OneOf<PaymentQueryResponse?, Exception>> Handle(PaymentQueryRequest request, CancellationToken cancellationToken)
    {
        var paymentEvent = await _paymentEventRepository.GetByIdAsync(request.Id);

        if (paymentEvent is null)
        {
            return null as PaymentQueryResponse;
        }

        return new PaymentQueryResponse(
            paymentEvent.Id,
            paymentEvent.CardNumberLast4,
            paymentEvent.ExpiryMonth,
            paymentEvent.ExpiryYear,
            paymentEvent.Amount,
            paymentEvent.Currency,
            paymentEvent.Status);
    }
}
