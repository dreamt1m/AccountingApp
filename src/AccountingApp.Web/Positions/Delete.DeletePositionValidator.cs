using FastEndpoints;
using FluentValidation;

namespace AccountingApp.Web.Positions
{
    public class DeletePositionValidator : Validator<DeletePositionRequest>
    {
        public DeletePositionValidator()
        {
            RuleFor(e => e.PositionId)
                .NotEmpty();
        }
    }
}
