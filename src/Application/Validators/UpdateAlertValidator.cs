using FluentValidation;
using VibeTrader.Application.Commands.UpdateAlert;

namespace VibeTrader.Application.Validators
{
    /// <summary>
    /// Validator for the UpdateAlertCommand
    /// </summary>
    public class UpdateAlertValidator : AbstractValidator<UpdateAlertCommand>
    {
        public UpdateAlertValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Alert ID is required");

            RuleFor(x => x.Symbol)
                .NotEmpty().WithMessage("Stock symbol is required")
                .MaximumLength(10).WithMessage("Stock symbol cannot be longer than 10 characters");

            RuleFor(x => x.TargetPrice)
                .GreaterThan(0).WithMessage("Target price must be greater than 0");
        }
    }
}