using MediatR;

using PaymentGateway.Api.Payments.Queries;

namespace PaymentGateway.Api.Payments;

public class PaymentEventHandler : INotificationHandler<PaymentEvent>
{
    private readonly IPaymentEventRepository _paymentEventRepository;

    public PaymentEventHandler(IPaymentEventRepository paymentEventRepository)
    {
        _paymentEventRepository = paymentEventRepository ?? throw new ArgumentNullException(nameof(paymentEventRepository));
    }

    public async Task Handle(PaymentEvent paymentEvent, CancellationToken cancellationToken)
    {
        await _paymentEventRepository.SaveAsync(paymentEvent);
    }
}
