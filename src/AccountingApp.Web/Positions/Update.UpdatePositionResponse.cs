namespace AccountingApp.Web.Positions;

public class UpdatePositionResponse
{
    public UpdatePositionResponse(PositionRecord position)
    {
        Position = position;
    }

    public PositionRecord Position { get; set; }
}