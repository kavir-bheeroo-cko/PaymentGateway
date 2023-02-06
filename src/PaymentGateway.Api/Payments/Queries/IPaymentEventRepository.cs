namespace PaymentGateway.Api.Payments.Queries;

public interface IPaymentEventRepository
{
    Task SaveAsync(PaymentEvent paymentEvent);
    Task<PaymentEvent?> GetByIdAsync(Guid id);
}
