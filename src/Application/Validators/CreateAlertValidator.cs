using FluentValidation;
using VibeTrader.Application.Commands.CreateAlert;

namespace VibeTrader.Application.Validators
{
    /// <summary>
    /// Validator for the CreateAlertCommand
    /// </summary>
    public class CreateAlertValidator : AbstractValidator<CreateAlertCommand>
    {
        public CreateAlertValidator()
        {
            RuleFor(x => x.Symbol)
                .NotEmpty().WithMessage("Stock symbol is required")
                .MaximumLength(10).WithMessage("Stock symbol cannot be longer than 10 characters");

            RuleFor(x => x.TargetPrice)
                .GreaterThan(0).WithMessage("Target price must be greater than 0");
        }
    }
}