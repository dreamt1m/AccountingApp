using AccountingApp.Core.Entities;
using AccountingApp.Infrastructure.Data;
using Ardalis.Result;

using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Employees.Commands.Create;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, Result<Guid>>
{
    private readonly AccountingDbContext _context;

    public CreateEmployeeCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(e => e.Id == request.PositionId, cancellationToken);

        if (position == null)
        {
            return Result.NotFound("Invalid position");
        }

        var newEmployee = new Employee(request.Name, position);

        _context.Employees.Add(newEmployee);

        await _context.SaveChangesAsync(cancellationToken);

        return newEmployee.Id; // TODO: Check
    }
}