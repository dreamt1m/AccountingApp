using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Reports.Queries.List;

public class ListReportsQueryHandler : IQueryHandler<ListReportsQuery, Result<IEnumerable<ReportDto>>>
{
    private readonly AccountingDbContext _context;

    public ListReportsQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<ReportDto>>> Handle(ListReportsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Reports.Include(e => e.Employee).AsQueryable()
            .Where(e => e.Employee.Id == request.EmployeeId
                        && e.Date.Year == request.Date.Year
                        && e.Date.Month == request.Date.Month)
            .ProjectToType<ReportDto>()
            .ToListAsync(cancellationToken);
    }
}