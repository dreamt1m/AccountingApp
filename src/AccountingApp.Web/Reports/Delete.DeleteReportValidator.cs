using FluentValidation;

namespace AccountingApp.Web.Reports;

public class DeleteReportValidator : Validator<DeleteReportRequest>
{
    public DeleteReportValidator()
    {
        RuleFor(e => e.EmployeeId)
            .NotEmpty();

        RuleFor(e => e.ReportId)
            .NotEmpty();
    }
}