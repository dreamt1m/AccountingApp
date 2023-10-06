using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Employees.Commands.Update;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, Result<EmployeeDto>>
{
    private readonly AccountingDbContext _context;

    public UpdateEmployeeCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
        {
            return Result.NotFound("Employee not found.");
        }

        var position = await _context.Positions.FirstOrDefaultAsync(e => e.Id == request.PositionId, cancellationToken);

        if (position == null)
        {
            return Result.Error("Position not found.");
        }

        employee.UpdateName(request.Name);
        employee.UpdatePosition(position);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(employee.Adapt<EmployeeDto>());
    }
}