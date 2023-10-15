using AccountingApp.Core.Entities;
using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Bonuses.Commands.Create;

public class CreateBonusCommandHandler : ICommandHandler<CreateBonusCommand, Result<Guid>>
{
    private readonly AccountingDbContext _context;

    public CreateBonusCommandHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateBonusCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return Result.NotFound("Employee not found");
        }

        if (!Enum.TryParse(request.BonusType, true, out BonusType bonusType))
            return Result.Error($"Bonus type {request.BonusType} not found");

        var newBonus = bonusType switch
        {
            BonusType.FixedPayment => Bonus.CreateFixedBonus(request.Title, request.Date, request.Value),
            BonusType.PercentageOfSalary => Bonus.CreatePercentageOfSalaryBonus(request.Title, request.Date,
                request.Value),
            _ => throw new InvalidOperationException("Unknown form of remuneration.")
        };

        employee.AddBonus(newBonus);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(newBonus.Id);
    }
}