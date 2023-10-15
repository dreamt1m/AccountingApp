using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Bonuses.Queries.List;

public class ListBonusesQueryHandler : IQueryHandler<ListBonusesQuery, Result<IEnumerable<BonusDto>>>
{
    private readonly AccountingDbContext _context;

    public ListBonusesQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<BonusDto>>> Handle(ListBonusesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Bonuses.Include(e => e.Employee).AsQueryable()
            .Where(e => e.Employee.Id == request.EmployeeId)
            .ProjectToType<BonusDto>()
            .ToListAsync(cancellationToken);
    }
}