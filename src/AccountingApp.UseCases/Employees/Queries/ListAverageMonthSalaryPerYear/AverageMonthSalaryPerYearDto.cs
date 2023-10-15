namespace AccountingApp.UseCases.Employees.Queries.ListAverageMonthSalaryPerYear;

public record AverageMonthSalaryPerYearDto(DateOnly Date, double Salary);