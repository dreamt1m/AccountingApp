namespace AccountingApp.UseCases.Bonuses.Commands.Create;

public record CreateBonusCommand(Guid EmployeeId, string Title, DateOnly Date, double Value, string BonusType) : ICommand<Result<Guid>>;