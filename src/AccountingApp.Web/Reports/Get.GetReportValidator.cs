using FluentValidation;

namespace AccountingApp.Web.Reports;

public class GetReportValidator : Validator<GetReportRequest>
{
    public GetReportValidator()
    {
        RuleFor(e => e.ReportId)
            .NotEmpty();
    }
}