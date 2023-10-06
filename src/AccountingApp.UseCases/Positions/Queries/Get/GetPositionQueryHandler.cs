using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Positions.Queries.Get;

public class GetPositionQueryHandler : IQueryHandler<GetPositionQuery, Result<PositionDto>>
{
    private readonly AccountingDbContext _context;

    public GetPositionQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PositionDto>> Handle(GetPositionQuery request, CancellationToken cancellationToken)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(e => e.Id == request.PositionId, cancellationToken);

        if (position == null)
        {
            return Result.NotFound();
        }

        return Result.Success(position.Adapt<PositionDto>());
    }
}