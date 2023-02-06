namespace PaymentGateway.Api.Payments.Commands.InMemoryRepository;

public class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly IDictionary<Guid, Payment> _keyValuePairs = new Dictionary<Guid, Payment>();

    public Task SaveAsync(Payment payment)
    {
        _keyValuePairs.Add(payment.Id, payment);
        return Task.CompletedTask;
    }
}
