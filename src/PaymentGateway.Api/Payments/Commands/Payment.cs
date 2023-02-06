namespace PaymentGateway.Api.Payments.Commands;

public class Payment
{
    public Guid Id { get; }

    public Card Card { get; }

    public uint Amount { get; }

    public string Currency { get; }

    public string Status { get; }

    public string? AuthorizationCode { get; }

    public string AcquirerName { get; }

    public Payment(string cardNumber, int expiryMonth, int expiryYear, uint amount, string currency, bool? authorized, string? authorizationCode, string acquirerName)
    {
        Id = Guid.NewGuid();
        Card = new Card(cardNumber, expiryMonth, expiryYear);
        Amount = amount;
        Currency = currency;
        AuthorizationCode = authorizationCode;
        AcquirerName = acquirerName;

        Status = authorized switch
        {
            true => PaymentStatus.Authorized,
            false => PaymentStatus.Declined,
            null => PaymentStatus.Declined
        };
    }
}

public class Card
{
    // todo: mask
    public string Number { get; }

    public int ExpiryMonth { get; }

    public int ExpiryYear { get; }

    public Card(string number, int expiryMonth, int expiryYear)
    {
        Number = number;
        ExpiryMonth = expiryMonth;
        ExpiryYear = expiryYear;
    }
}
