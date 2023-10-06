using FluentValidation;

namespace AccountingApp.Web.Employees;

public class ListMonthStatisticsEmployeesValidator : Validator<ListMonthStatisticsEmployeesRequest>
{
    public ListMonthStatisticsEmployeesValidator()
    {
        RuleFor(e => e.Date)
            .NotEmpty();
    }
}