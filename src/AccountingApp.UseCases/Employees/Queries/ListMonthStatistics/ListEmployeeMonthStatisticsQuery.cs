namespace AccountingApp.UseCases.Employees.Queries.ListMonthStatistics;

public record ListEmployeeMonthStatisticsQuery(DateOnly Date) : IQuery<Result<IEnumerable<EmployeeMonthStatisticsDto>>>;