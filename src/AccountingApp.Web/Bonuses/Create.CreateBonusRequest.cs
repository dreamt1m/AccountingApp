using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Bonuses;

public class CreateBonusRequest
{
    public const string Route = "/Bonuses";

    [Required] public Guid EmployeeId { get; set; }

    [Required] public string? Title { get; set; }

    [Required] public DateOnly Date { get; set; }

    [Required] public double? Value { get; set; }

    [Required] public string? BonusType { get; set; }
}