using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Employees.Queries.List;

public class ListEmployeesQueryHandler : IQueryHandler<ListEmployeesQuery, Result<IEnumerable<EmployeeDto>>>
{
    private readonly AccountingDbContext _context;

    public ListEmployeesQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<EmployeeDto>>> Handle(ListEmployeesQuery request, CancellationToken cancellationToken)
    {
        return 
            await _context.Employees.Include(e => e.Position)
            .AsQueryable().ProjectToType<EmployeeDto>()
            .ToListAsync(cancellationToken);
    }
}