using AccountingApp.Core.Entities;
using AccountingApp.Infrastructure.Data;

namespace AccountingApp.UseCases.Positions.Commands.Create;

public class CreatePositionCommandHandler : ICommandHandler<CreatePositionCommand, Result<Guid>>
{
    private readonly AccountingDbContext _context;

    public CreatePositionCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.FormOfRemuneration, true, out FormOfRemuneration formOfRemuneration))
            return Result.Error($"Form of remuneration {request.FormOfRemuneration} not found");

        var newPosition = formOfRemuneration switch
        {
            FormOfRemuneration.Fixed => Position.CreateFixedPosition(request.Name, request.RatePerHour,
                request.OvertimeMultiplier, request.WorkingHoursPerMonth),
            FormOfRemuneration.Hourly => Position.CreateHourlyPosition(request.Name, request.RatePerHour),
            _ => throw new InvalidOperationException("Unknown form of remuneration.")
        };

        _context.Positions.Add(newPosition);

        await _context.SaveChangesAsync(cancellationToken);

        return newPosition.Id;
    }
}