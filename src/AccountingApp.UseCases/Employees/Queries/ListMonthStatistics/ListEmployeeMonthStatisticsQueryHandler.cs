using AccountingApp.Infrastructure.Data;
using AccountingApp.UseCases.Positions;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Employees.Queries.ListMonthStatistics;

public class ListEmployeeMonthStatisticsQueryHandler : IQueryHandler<ListEmployeeMonthStatisticsQuery, Result<IEnumerable<EmployeeMonthStatisticsDto>>>
{
    private readonly AccountingDbContext _context;

    public ListEmployeeMonthStatisticsQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<EmployeeMonthStatisticsDto>>> Handle(ListEmployeeMonthStatisticsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Include(e => e.Position)
            .Include(e => e.Reports)
            .Include(e => e.Bonuses)
            .AsQueryable()
            .Select(e =>
                new EmployeeMonthStatisticsDto(
                    e.Id,
                    e.Name,
                    e.Position.Adapt<PositionDto>(),
                    e.GetWorkedHours(request.Date),
                    e.GetOvertimeHours(request.Date),
                    e.GetSalary(request.Date)))
            .ToListAsync(cancellationToken);
    }
}