using System.Text.Json;
using Microsoft.Extensions.Options;
using PaymentGateway.Api.Bank;

namespace PaymentGateway.Api.Acquirers.BankSimulator;

public class BankSimulatorAcquirer : IAcquirer
{
    private readonly HttpClient _httpClient;
    private readonly AcquirerOptions _acquirerOptions;

    public BankSimulatorAcquirer(IHttpClientFactory httpClientFactory, IOptions<AcquirerOptions> acquirerOptions)
    {
        _ = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _ = acquirerOptions ?? throw new ArgumentNullException(nameof(acquirerOptions));

        _httpClient = httpClientFactory.CreateClient();
        _acquirerOptions = acquirerOptions.Value;
    }

    public async Task<AcquirerResponse?> ProcessPaymentAsync(AcquirerRequest acquirerRequest, CancellationToken cancellationToken)
    {
        using var httpResponse = await _httpClient.PostAsJsonAsync(_acquirerOptions.Uri, acquirerRequest, cancellationToken);
        using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

        var acquirerResponse = await JsonSerializer.DeserializeAsync<AcquirerResponse>(responseStream, cancellationToken: cancellationToken);
        return acquirerResponse;
    }
}
