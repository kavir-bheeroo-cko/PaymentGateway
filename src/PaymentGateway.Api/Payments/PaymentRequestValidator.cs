using FluentValidation;
using PaymentGateway.Api.Payments.Commands;

namespace PaymentGateway.Api.Payments;

public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
{
    public PaymentRequestValidator()
    {
        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("card_number_missing")
            .NotNull().WithMessage("card_number_missing")
            .Length(14, 19).WithMessage("card_number_invalid");

        RuleFor(x => x.ExpiryMonth)
            .NotEmpty().WithMessage("card_expiry_month_missing")
            .NotNull().WithMessage("card_expiry_month_missing")
            .InclusiveBetween(1, 12).WithMessage("card_expiry_month_invalid")
            .GreaterThan(DateTime.UtcNow.Month).When(x => x.ExpiryYear == DateTime.UtcNow.Year).WithMessage("card_expiry_month_invalid");

        RuleFor(x => x.ExpiryYear)
            .NotEmpty().WithMessage("card_expiry_year_missing")
            .NotNull().WithMessage("card_expiry_year_missing")
            .InclusiveBetween(2023, 9999).WithMessage("card_expiry_year_invalid");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("currency_missing")
            .NotNull().WithMessage("currency_missing")
            .Must(x => new string[] { "USD", "GBP", "EUR" }.Contains(x.ToUpperInvariant())).WithMessage("currency_invalid");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("amount_missing")
            .NotNull().WithMessage("amount_missing")
            .GreaterThanOrEqualTo(0u).WithMessage("amount_invalid");

        RuleFor(x => x.Cvv)
            .NotEmpty().WithMessage("cvv_missing")
            .NotNull().WithMessage("cvv_missing")
            .Must(x => (x.Length >= 3 || x.Length <= 4) && int.TryParse(x, out _)).WithMessage("cvv_invalid");
    }
}
