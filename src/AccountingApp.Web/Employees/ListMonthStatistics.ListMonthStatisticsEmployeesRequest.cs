using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Employees;

public class ListMonthStatisticsEmployeesRequest
{
    public const string Route = "/EmployeeMonthStatistics/{Date:DateTime}";

    [Required] public DateTime Date { get; set; }
}