namespace AccountingApp.UseCases.Employees.Queries.List;

public record ListEmployeesQuery : IQuery<Result<IEnumerable<EmployeeDto>>>;