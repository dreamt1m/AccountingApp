using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Employees.Queries.Get;

public class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, Result<EmployeeDto>>
{
    private readonly AccountingDbContext _context;

    public GetEmployeeQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<EmployeeDto>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.Position)
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return Result.NotFound();
        }

        return Result.Success(employee.Adapt<EmployeeDto>());
    }
}