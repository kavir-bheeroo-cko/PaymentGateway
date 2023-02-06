using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;
using OneOf;

namespace PaymentGateway.Api.Payments.Commands;

public record PaymentRequest : IRequest<OneOf<PaymentResponse, ValidationResult, Exception>>
{
    [JsonPropertyName("card_number")]
    public string CardNumber { get; init; }

    [JsonPropertyName("expiry_month")]
    public int ExpiryMonth { get; init; }

    [JsonPropertyName("expiry_year")]
    public int ExpiryYear { get; init; }

    [JsonPropertyName("amount")]
    public uint Amount { get; init; }

    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("cvv")]
    public string Cvv { get; init; }
}
