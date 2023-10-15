using FluentValidation;

namespace AccountingApp.Web.Bonuses;

public class DeleteBonusValidator : Validator<DeleteBonusRequest>
{
    public DeleteBonusValidator()
    {
        RuleFor(e => e.EmployeeId)
            .NotEmpty();

        RuleFor(e => e.BonusId)
            .NotEmpty();
    }
}