namespace AccountingApp.Web.Bonuses;

public record BonusRecord(Guid Id, string Title, DateOnly Date, double Value, string BonusType);