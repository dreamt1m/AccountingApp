namespace AccountingApp.UseCases.Reports.Commands.Create;

public record CreateReportCommand(Guid EmployeeId, DateOnly Date, ushort HoursWorked) : ICommand<Result<Guid>>;