using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Employees.Queries.ListAverageMonthSalaryPerYear;

public class ListAverageMonthSalaryPerYearQueryHandler : IQueryHandler<ListAverageMonthSalaryPerYearQuery,
    Result<IEnumerable<AverageMonthSalaryPerYearDto>>>
{
    private readonly AccountingDbContext _context;

    public ListAverageMonthSalaryPerYearQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<AverageMonthSalaryPerYearDto>>> Handle(
        ListAverageMonthSalaryPerYearQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await _context.Employees
            .Include(e => e.Position)
            .Include(e => e.Reports)
            .Include(e => e.Bonuses)
            .ToListAsync(cancellationToken);

        return Result.Success(GetAverageMonthSalaryPerYear());

        IEnumerable<AverageMonthSalaryPerYearDto> GetAverageMonthSalaryPerYear()
        {
            for (int i = 1; i <= 12; i++)
            {
                var targetMonth = new DateOnly(request.Date.Year, i, 1);
                double salary = 0;

                foreach (var employee in employees)
                {
                    salary += employee.GetSalary(targetMonth);
                }

                yield return new AverageMonthSalaryPerYearDto(targetMonth, salary);
            }
        }
    }
}