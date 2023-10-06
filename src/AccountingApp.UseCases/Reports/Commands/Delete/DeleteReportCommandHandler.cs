using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Reports.Commands.Delete;

public class DeleteReportCommandHandler : ICommandHandler<DeleteReportCommand, Result>
{
    private readonly AccountingDbContext _context;

    public DeleteReportCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.Reports)
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return Result.NotFound("Employee not found");
        }

        var report = employee.Reports.FirstOrDefault(e => e.Id == request.ReportId);

        if (report == null)
        {
            return Result.Error("Report not found");
        }

        employee.RemoveReport(report);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}