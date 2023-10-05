using Ardalis.Result;

namespace AccountingApp.UseCases.Positions.Commands.Create;

public record CreatePositionCommand(
    string Name,
    double RatePerHour, 
    double OvertimeMultiplier,
    ushort WorkingHoursPerMonth, 
    string FormOfRemuneration) 
    : ICommand<Result<Guid>>;