using System.Text.Json.Serialization;

namespace PaymentGateway.Api.Acquirers;

public record AcquirerResponse
{
    [JsonPropertyName("authorized")]
    public bool Authorized { get; }

    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; }

    public AcquirerResponse(bool authorized, string authorizationCode)
    {
        Authorized = authorized;
        AuthorizationCode = authorizationCode;
    }
}
