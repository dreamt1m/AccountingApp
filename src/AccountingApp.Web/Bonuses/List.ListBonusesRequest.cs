using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Bonuses;

public class ListBonusesRequest
{
    public const string Route = "/Bonuses/{EmployeeId:Guid}";

    [Required] public Guid EmployeeId { get; set; }
}