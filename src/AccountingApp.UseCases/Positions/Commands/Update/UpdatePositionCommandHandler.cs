using AccountingApp.Infrastructure.Data;
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


    }
}