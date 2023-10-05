namespace AccountingApp.Web.Positions;

public record PositionRecord(
    Guid Id,
    string Name,
    double RatePerHour,
    double OvertimeMultiplier,
    ushort WorkingHoursPerMonth,
    string FormOfRemuneration);