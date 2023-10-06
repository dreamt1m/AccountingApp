using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Reports;

public class ListReportsRequest
{
    public const string Route = "/Reports/{EmployeeId:Guid}/{Date:DateTime}";

    [Required] public Guid EmployeeId { get; set; }

    [Required] public DateTime Date { get; set; }
}