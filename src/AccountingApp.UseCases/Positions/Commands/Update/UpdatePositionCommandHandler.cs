using AccountingApp.Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Positions.Commands.Update;

public class UpdatePositionCommandHandler : ICommandHandler<UpdatePositionCommand, Result<PositionDto>>
{
    private readonly AccountingDbContext _context;

    public UpdatePositionCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PositionDto>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (position == null)
        {
            return Result.NotFound();
        }

        position.UpdateName(request.Name);
        position.UpdateRatePerHour(request.RatePerHour);
        position.UpdateOvertimeMultiplier(request.OvertimeMultiplier);
        position.UpdateWorkingHoursPerMonth(request.WorkingHoursPerMonth);
        position.UpdateFormOfRemuneration(request.FormOfRemuneration);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(position.Adapt<PositionDto>());
    }
}