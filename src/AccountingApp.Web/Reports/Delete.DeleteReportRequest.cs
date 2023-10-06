using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Reports;

public class DeleteReportRequest
{
    public const string Route = "/Reports/{EmployeeId:Guid}/{ReportId:Guid}";

    [Required] public Guid ReportId { get; set; }

    [Required] public Guid EmployeeId { get; set; }
}