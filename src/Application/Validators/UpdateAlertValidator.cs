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
                .NotEmpty().WithMessage("Symbol is required")
                .MaximumLength(10).WithMessage("Symbol cannot exceed 10 characters");

            RuleFor(x => x.TargetPrice)
                .GreaterThan(0).WithMessage("Target price must be greater than zero");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Alert type is invalid");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters")
                .When(x => x.Notes != null);
        }
    }
}