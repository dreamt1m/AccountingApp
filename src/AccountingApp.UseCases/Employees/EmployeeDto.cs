using AccountingApp.UseCases.Positions;

namespace AccountingApp.UseCases.Employees;

public record EmployeeDto(Guid Id, string Name, PositionDto Position);