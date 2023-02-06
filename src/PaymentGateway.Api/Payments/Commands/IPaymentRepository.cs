namespace PaymentGateway.Api.Payments.Commands;

public interface IPaymentRepository
{
    public Task SaveAsync(Payment payment);
}
