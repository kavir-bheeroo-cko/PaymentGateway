namespace PaymentGateway.Api.Payments.Queries.InMemoryRepository
{
    public class InMemoryPaymentEventRepository : IPaymentEventRepository
    {
        private readonly IDictionary<Guid, PaymentEvent> _keyValuePairs = new Dictionary<Guid, PaymentEvent>();

        public Task<PaymentEvent?> GetByIdAsync(Guid id)
        {
            if (!_keyValuePairs.ContainsKey(id))
                return Task.FromResult<PaymentEvent?>(null);

            return Task.FromResult<PaymentEvent?>(_keyValuePairs[id]);
        }

        public Task SaveAsync(PaymentEvent paymentEvent)
        {
            _keyValuePairs.Add(paymentEvent.Id, paymentEvent);
            return Task.CompletedTask;
        }
    }
}
