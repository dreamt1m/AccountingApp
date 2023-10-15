namespace AccountingApp.UseCases.Bonuses;

public record BonusDto(Guid Id, string Title, DateOnly Date, double Value, string BonusType);