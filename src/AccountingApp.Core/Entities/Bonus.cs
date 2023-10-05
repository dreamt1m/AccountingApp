using AccountingApp.BuildingBlocks.Models;

namespace AccountingApp.Core.Entities;

public class Bonus : EntityBase
{
    private Bonus() {}

    public Bonus(string title, DateOnly date, double value, BonusType bonusType, Employee employee)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException(nameof(title), "Invalid bonus title");

        if (bonusType is BonusType.Unspecified)
            throw new ArgumentException("Need to select bonus type.",
                nameof(bonusType));

        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid bonus value.");

        Title = title;
        Date = date;
        Value = value;
        BonusType = bonusType;
        Employee = employee ?? throw new ArgumentNullException(nameof(employee));
    }

    public static Bonus CreateFixedBonus(string title, DateOnly date, double value, Employee employee) =>
        new(title, date, value, BonusType.FixedPayment, employee);

    public static Bonus CreatePercentageOfSalaryBonus(string title, DateOnly date, double value, Employee employee) =>
        new(title, date, value, BonusType.PercentageOfSalary, employee);

    public string Title { get; private set; }

    public DateOnly Date { get; private set; }

    public double Value { get; private set; }

    public BonusType BonusType { get; private set; }

    public Employee Employee { get; private set; }

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