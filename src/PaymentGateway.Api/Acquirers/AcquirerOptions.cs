namespace PaymentGateway.Api.Acquirers;

public class AcquirerOptions
{
    public const string SectionName = "Acquirer";

    public string? Uri { get; init; }
}
