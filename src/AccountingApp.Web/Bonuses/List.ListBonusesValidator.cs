using FluentValidation;

namespace AccountingApp.Web.Bonuses;

public class ListBonusesValidator : Validator<ListBonusesRequest>
{
    public ListBonusesValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty();
    }
}