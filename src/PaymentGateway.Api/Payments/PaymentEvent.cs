using MediatR;

namespace PaymentGateway.Api.Payments
{
    public class PaymentEvent : INotification
    {
        public Guid Id { get; }

        public PaymentEvent(Payment payment)
        {
            Id = payment.Id;    
        }
    }
}
