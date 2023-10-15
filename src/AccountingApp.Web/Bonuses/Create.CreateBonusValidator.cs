using FluentValidation;

namespace AccountingApp.Web.Bonuses;

public class CreateBonusValidator : Validator<CreateBonusRequest>
{
    public CreateBonusValidator()
    {
        RuleFor(e => e.EmployeeId)
            .NotEmpty();

        RuleFor(e => e.Date)
            .NotEmpty();

        RuleFor(e => e.Value)
            .NotNull()
            .GreaterThan(0);

        RuleFor(e => e.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(e => e.BonusType)
            .NotNull()
            .NotEmpty();
    }
}