using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Positions.Commands.Delete;

public class DeletePositionCommandHandler : ICommandHandler<DeletePositionCommand, Result>
{
    private readonly AccountingDbContext _context;

    public DeletePositionCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (position == null)
        {
            return Result.NotFound($"Position with id {request.Id} wasn't found.");
        }

        _context.Positions.Remove(position);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}