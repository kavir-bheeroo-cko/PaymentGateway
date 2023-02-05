namespace PaymentGateway.Api.Acquirers;

public class AcquirerOptions
{
    public string Uri { get; }

    public AcquirerOptions(string uri)
    {
        Uri = uri ?? throw new ArgumentNullException(nameof(uri));
    }
}
