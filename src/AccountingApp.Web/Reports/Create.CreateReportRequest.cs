using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Reports;

public class CreateReportRequest
{
    public const string Route = "/Reports";

    [Required] public Guid EmployeeId { get; set; }

    [Required] public DateOnly Date { get; set; }

    [Required] public ushort HoursWorked { get; set; }
}