using FastEndpoints;
using FluentValidation;

namespace AccountingApp.Web.Positions;

public class GetPositionValidator : Validator<GetPositionRequest>
{
    public GetPositionValidator()
    {
        RuleFor(e => e.PositionId)
            .NotEmpty();
    }
}