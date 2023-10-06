using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Employees;

public class UpdateEmployeeRequest
{
    public const string Route = "/Employees/{EmployeeId:Guid}";

    [Required] public Guid EmployeeId { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public Guid PositionId { get; set; }
}