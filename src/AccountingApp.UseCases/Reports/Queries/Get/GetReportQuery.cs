namespace AccountingApp.UseCases.Reports.Queries.Get;

public record GetReportQuery(Guid ReportId) : IQuery<Result<ReportDto>>;