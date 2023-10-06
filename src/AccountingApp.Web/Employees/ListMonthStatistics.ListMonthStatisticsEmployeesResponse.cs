using AccountingApp.UseCases.Positions;

namespace AccountingApp.Web.Employees;

public class ListMonthStatisticsEmployeesResponse
{
    public List<EmployeeMonthStatisticsRecord> Employees { get; set; } = new();
}

public record EmployeeMonthStatisticsRecord(Guid Id, string Name, PositionDto Position, ushort WorkedHours, ushort OvertimeHours, double Salary);