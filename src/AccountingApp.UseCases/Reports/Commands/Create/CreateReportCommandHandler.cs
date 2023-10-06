using AccountingApp.Core.Entities;
using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Reports.Commands.Create;

public class CreateReportCommandHandler : ICommandHandler<CreateReportCommand, Result<Guid>>
{
    private readonly AccountingDbContext _context;

    public CreateReportCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return Result.NotFound("Employee not found.");
        }

        var newReport = new Report(request.Date, request.HoursWorked);

        employee.AddReport(newReport);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(newReport.Id);
    }
}