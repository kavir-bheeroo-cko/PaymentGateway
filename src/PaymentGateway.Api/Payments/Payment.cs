namespace PaymentGateway.Api.Payments;

public class Payment
{
    public Guid Id { get; }

    public Card Card { get; }

    public uint Amount { get; }

    public string Currency { get; }

    public string Status { get; }

    public string AuthorizationCode { get; }

    public string AcquirerName { get; }

    public Payment(Guid id, Card card, uint amount, string currency, string status, string authorizationCode, string acquirerName)
    {
        Id = id;
        Card = card;
        Amount = amount;
        Currency = currency;
        Status = status;
        AuthorizationCode = authorizationCode;
        AcquirerName = acquirerName;
    }
}

public class Card
{
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