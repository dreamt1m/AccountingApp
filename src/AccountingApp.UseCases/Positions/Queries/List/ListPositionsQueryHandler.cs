using AccountingApp.Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Positions.Queries.List;

public class ListPositionsQueryHandler : IQueryHandler<ListPositionsQuery, Result<IEnumerable<PositionDto>>>
{
    private readonly AccountingDbContext _context;

    public ListPositionsQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<PositionDto>>> Handle(ListPositionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Positions.AsQueryable().ProjectToType<PositionDto>().ToListAsync(cancellationToken);
    }
}