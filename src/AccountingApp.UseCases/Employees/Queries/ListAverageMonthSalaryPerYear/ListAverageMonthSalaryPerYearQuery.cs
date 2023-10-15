namespace AccountingApp.UseCases.Employees.Queries.ListAverageMonthSalaryPerYear;

public record ListAverageMonthSalaryPerYearQuery(DateOnly Date) : IQuery<Result<IEnumerable<AverageMonthSalaryPerYearDto>>>;