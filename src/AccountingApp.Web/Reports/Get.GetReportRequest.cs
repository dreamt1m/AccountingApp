using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Reports;

public class GetReportRequest
{
    public const string Route = "/Reports/{ReportId:Guid}";

    [Required] public Guid ReportId { get; set; }
}