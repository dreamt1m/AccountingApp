namespace AccountingApp.UseCases.Reports.Commands.Delete;

public record DeleteReportCommand(Guid EmployeeId, Guid ReportId) : ICommand<Result>;