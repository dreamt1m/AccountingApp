using FluentValidation;

namespace AccountingApp.Web.Reports;

public class ListReportsValidator : Validator<ListReportsRequest>
{
    public ListReportsValidator()
    {
        RuleFor(e => e.EmployeeId)
            .NotEmpty();

        RuleFor(e => e.Date)
            .NotEmpty();
    }
}