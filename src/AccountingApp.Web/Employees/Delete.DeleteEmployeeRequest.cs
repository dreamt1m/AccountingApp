using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Employees;

public class DeleteEmployeeRequest
{
    public const string Route = "/Employees/{EmployeeId:Guid}";

    [Required] public Guid EmployeeId { get; set; }
}