using AccountingApp.UseCases.Positions;

namespace AccountingApp.UseCases.Employees.Queries.ListMonthStatistics;

public record EmployeeMonthStatisticsDto(Guid Id, string Name, PositionDto Position, ushort WorkedHours, ushort OvertimeHours, double Salary);