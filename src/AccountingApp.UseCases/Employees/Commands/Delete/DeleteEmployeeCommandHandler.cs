using AccountingApp.Core.Entities;
using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Employees.Commands.Delete;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, Result>
{
    private readonly AccountingDbContext _context;

    public DeleteEmployeeCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
        {
            return Result.NotFound($"Employee with id {request.Id} wasn't found.");
        }

        _context.Employees.Remove(employee);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}