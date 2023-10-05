namespace AccountingApp.UseCases.Positions.Commands.Update;

public record UpdatePositionCommand(
    Guid Id,
    string Name,
    double RatePerHour,
    double OvertimeMultiplier,
    ushort WorkingHoursPerMonth,
    string FormOfRemuneration)
    : ICommand<Result<PositionDto>>;