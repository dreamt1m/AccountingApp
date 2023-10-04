using AccountingApp.Core.Common;

namespace AccountingApp.Core.Entities;

public class Bonus : BaseEntity
{
    public string Title { get; set; }

    public DateOnly Date { get; set; }

    public double Value { get; set; }

    public BonusType BonusType { get; set; }

    public Employee Employee { get; set; }

    public double CalculateBonus(double salary)
    {
        var salaryWithBonus = BonusType switch
        {
            BonusType.FixedPayment => Value,
            BonusType.PercentageOfSalary => salary * Value / 100,
        };

        return salaryWithBonus;
    }
}

public enum BonusType
{
    Unspecified = 0,
    PercentageOfSalary,
    FixedPayment
}