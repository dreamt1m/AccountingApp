namespace AccountingApp.UseCases.Bonuses.Commands.Delete;

public record DeleteBonusCommand(Guid EmployeeId, Guid BonusId) : ICommand<Result>;