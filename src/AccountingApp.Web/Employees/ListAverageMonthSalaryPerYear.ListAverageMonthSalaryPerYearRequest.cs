using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Employees;

public class ListAverageMonthSalaryPerYearRequest
{
    public const string Route = "/Employees/AverageMonthSalaryPerYearRequest/{Date:DateTime}";

    [Required] public DateTime Date { get; set; }
}