using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Employees;

public class UpdateEmployeeRequest
{
    public const string Route = "/Employees/{Id:Guid}";

    [Required] public Guid Id { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public Guid PositionId { get; set; }
}