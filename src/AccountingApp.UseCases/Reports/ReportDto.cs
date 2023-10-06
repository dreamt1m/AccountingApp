namespace AccountingApp.UseCases.Reports;

public record ReportDto(Guid Id, DateOnly Date, ushort HoursWorked);