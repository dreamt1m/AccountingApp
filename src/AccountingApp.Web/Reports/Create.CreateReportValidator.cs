using FluentValidation;

namespace AccountingApp.Web.Reports
{
    public class CreateReportValidator : Validator<CreateReportRequest>
    {
        public CreateReportValidator()
        {
            RuleFor(e => e.EmployeeId)
                .NotEmpty();

            RuleFor(e => e.Date)
                .NotEmpty();

            RuleFor(e => e.HoursWorked)
                .InclusiveBetween((ushort)1, (ushort)24);
        }
    }
}