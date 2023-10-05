namespace AccountingApp.UseCases.Positions.Commands.Delete;

public record DeletePositionCommand(Guid Id) : ICommand<Result>;