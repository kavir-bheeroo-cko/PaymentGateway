using MediatR;

using PaymentGateway.Api.Payments;

namespace PaymentGateway.Api.Queries
{
    public class PaymentEventHandler : INotificationHandler<PaymentEvent>
    {
        public PaymentEventHandler()
        {
        }

        public Task Handle(PaymentEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
