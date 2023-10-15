using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Bonuses;

public class DeleteBonusRequest
{
    public const string Route = "/Bonuses/{EmployeeId:Guid}/{BonusId:Guid}";

    [Required] public Guid BonusId { get; set; }

    [Required] public Guid EmployeeId { get; set; }
}