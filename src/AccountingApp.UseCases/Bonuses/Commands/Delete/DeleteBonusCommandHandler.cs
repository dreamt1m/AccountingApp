using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Bonuses.Commands.Delete;

public class DeleteBonusCommandHandler : ICommandHandler<DeleteBonusCommand, Result>
{
    private readonly AccountingDbContext _context;

    public DeleteBonusCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteBonusCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.Bonuses)
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return Result.NotFound("Employee not found");
        }

        var bonus = employee.Bonuses.FirstOrDefault(e => e.Id == request.BonusId);

        if (bonus == null)
        {
            return Result.Error("Bonus not found");
        }

        employee.RemoveBonus(bonus);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}