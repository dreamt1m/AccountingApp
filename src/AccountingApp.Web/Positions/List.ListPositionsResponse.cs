namespace AccountingApp.Web.Positions;

public class ListPositionsResponse
{
    public List<PositionRecord> Positions { get; set; } = new();
}