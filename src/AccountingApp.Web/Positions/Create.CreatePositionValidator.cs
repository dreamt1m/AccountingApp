using AccountingApp.Infrastructure.Data.Configurations;
using FastEndpoints;
using FluentValidation;

namespace AccountingApp.Web.Positions
{
    public class CreatePositionValidator : Validator<CreatePositionRequest>
    {
        public CreatePositionValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(2)
                .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        }
    }
}
