using PaymentGateway.Api.Acquirers;

namespace PaymentGateway.Api.Bank;

public interface IAcquirer
{
    Task<AcquirerResponse?> ProcessPaymentAsync(AcquirerRequest acquirerRequest, CancellationToken cancellationToken);
}
