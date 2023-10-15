using FluentValidation;

namespace AccountingApp.Web.Employees;

public class ListAverageMonthSalaryPerYearValidator : Validator<ListAverageMonthSalaryPerYearRequest>
{
    public ListAverageMonthSalaryPerYearValidator()
    {
        RuleFor(e => e.Date)
            .NotEmpty();
    }
}