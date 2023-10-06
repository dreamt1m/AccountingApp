using AccountingApp.Web.Positions;

namespace AccountingApp.Web.Employees;

public record EmployeeRecord(Guid Id, string Name, PositionRecord Position);