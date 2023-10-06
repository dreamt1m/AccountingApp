using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Positions;

public class UpdatePositionRequest
{
    public const string Route = "/Positions/{PositionId:Guid}";

    [Required] public Guid PositionId { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public double? RatePerHour { get; set; }

    [Required] public double? OvertimeMultiplier { get; set; }

    [Required] public ushort? WorkingHoursPerMonth { get; set; }

    [Required] public string? FormOfRemuneration { get; set; }
}