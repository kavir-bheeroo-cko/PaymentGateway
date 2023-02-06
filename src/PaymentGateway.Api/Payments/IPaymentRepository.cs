namespace PaymentGateway.Api.Payments;

public interface IPaymentRepository
{
    public Task Save(Payment payment);
}
