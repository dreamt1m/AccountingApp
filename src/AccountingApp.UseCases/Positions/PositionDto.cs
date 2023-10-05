namespace AccountingApp.UseCases.Positions;

public record PositionDto(
    Guid Id,
    string Name,
    double RatePerHour,
    double OvertimeMultiplier,
    ushort WorkingHoursPerMonth,
    string FormOfRemuneration);