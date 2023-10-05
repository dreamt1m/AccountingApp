namespace AccountingApp.Web.Positions;

public class GetPositionRequest
{
    public const string Route = "/Positions/{PositionId:Guid}";

    public Guid PositionId { get; set; }
}