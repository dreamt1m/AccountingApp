namespace AccountingApp.UseCases.Employees.Commands.Delete;

public record DeleteEmployeeCommand(Guid Id) : ICommand<Result>;