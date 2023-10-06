namespace AccountingApp.UseCases.Reports.Queries.List;

public record ListReportsQuery(Guid EmployeeId, DateOnly Date) : IQuery<Result<IEnumerable<ReportDto>>>;