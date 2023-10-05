using Ardalis.Result;

namespace AccountingApp.UseCases.Employees.Commands.Create;

public record CreateEmployeeCommand(string Name, Guid PositionId) : ICommand<Result<Guid>>;