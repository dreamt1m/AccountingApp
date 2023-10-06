using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Employees;

public class CreateEmployeeRequest
{
    public const string Route = "/Employees";

    [Required] public string? Name { get; set; }

    [Required] public Guid PositionId { get; set; }
}