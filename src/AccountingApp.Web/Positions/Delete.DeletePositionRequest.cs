using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Web.Positions;

public class DeletePositionRequest
{
    public const string Route = "/Positions/{PositionId:Guid}";

    [Required] public Guid PositionId { get; set; }
}