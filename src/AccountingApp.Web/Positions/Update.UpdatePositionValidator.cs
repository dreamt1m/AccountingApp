using FastEndpoints;
using FluentValidation;

namespace AccountingApp.Web.Positions;

public class UpdatePositionValidator : Validator<UpdatePositionRequest>
{
    public UpdatePositionValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty();
    }
}