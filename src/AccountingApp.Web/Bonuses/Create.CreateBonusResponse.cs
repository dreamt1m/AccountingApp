namespace AccountingApp.Web.Bonuses;

public class CreateBonusResponse
{
    public CreateBonusResponse(Guid bonusId)
    {
        BonusId = bonusId;
    }

    public Guid BonusId { get; set; }
}