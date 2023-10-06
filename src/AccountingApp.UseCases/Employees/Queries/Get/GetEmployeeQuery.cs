namespace AccountingApp.UseCases.Employees.Queries.Get;

public record GetEmployeeQuery(Guid EmployeeId) : IQuery<Result<EmployeeDto>>;