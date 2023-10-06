namespace AccountingApp.UseCases.Employees.Commands.Update;

public record UpdateEmployeeCommand(Guid Id, string Name, Guid PositionId) : ICommand<Result<EmployeeDto>>;