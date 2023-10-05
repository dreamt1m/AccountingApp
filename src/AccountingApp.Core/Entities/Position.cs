using AccountingApp.BuildingBlocks.Models;

namespace AccountingApp.Core.Entities;

public class Position : EntityBase
{
    private Position() { }

    public Position(string name, double ratePerHour, double overtimeMultiplier, ushort workingHoursPerMonth,
        FormOfRemuneration formOfRemuneration)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Name can not be empty.");

        if (ratePerHour <= 0)
            throw new ArgumentOutOfRangeException(nameof(ratePerHour), ratePerHour, $"Rate per hour can't be zero or negative. Actual value is {ratePerHour}.");

        if (overtimeMultiplier < 1)
            throw new ArgumentOutOfRangeException(nameof(overtimeMultiplier), overtimeMultiplier, $"Overtime multiplier must be more than 1 or equal. Actual value is {overtimeMultiplier}");

        if (Enum.IsDefined(formOfRemuneration) && formOfRemuneration is FormOfRemuneration.Unspecified)
            throw new ArgumentException("Remuneration for position must be set to valid value.",
                nameof(formOfRemuneration));

        Name = name;
        RatePerHour = ratePerHour;
        OvertimeMultiplier = overtimeMultiplier;
        WorkingHoursPerMonth = workingHoursPerMonth;
        FormOfRemuneration = formOfRemuneration;
    }

    public static Position CreateFixedPosition(string name, double ratePerHour, double overtimeMultiplier,
        ushort workingHoursPerMonth)
        => new(name, ratePerHour, overtimeMultiplier, workingHoursPerMonth, FormOfRemuneration.Fixed);

    public static Position CreateHourlyPosition(string name, double ratePerHour)
        => new(name, ratePerHour, 1, 0, FormOfRemuneration.Hourly);

    public string Name { get; private set; }

    public double RatePerHour { get; private set; }

    public double OvertimeMultiplier { get; private set; }

    public double OvertimeRatePerHour => RatePerHour * OvertimeMultiplier;

    public ushort WorkingHoursPerMonth { get; private set; }

    public FormOfRemuneration FormOfRemuneration { get; private set; }

    public double CalculateSalary(int workedHours)
    {
        var salaryBeforeTaxes = FormOfRemuneration switch
        {
            FormOfRemuneration.Fixed => GetFixedSalary(),
            FormOfRemuneration.Hourly => GetHourlySalary(workedHours, RatePerHour),
            _ => throw new InvalidOperationException("Form of remuneration is corrupted")
        };

        return salaryBeforeTaxes;

        static double GetHourlySalary(int workedHours, double rate)
        {
            return workedHours * rate;
        }

        double GetFixedSalary()
        {
            var difference = workedHours - WorkingHoursPerMonth;

            var salary = difference switch
            {
                <= 0 => GetHourlySalary(workedHours, RatePerHour), // no overtimes
                > 0 => GetHourlySalary(WorkingHoursPerMonth, RatePerHour)
                       + GetHourlySalary(difference, OvertimeRatePerHour) // overtimes
            };

            return salary;
        }
    }
}

public enum FormOfRemuneration
{
    Unspecified = 0,
    Hourly,
    Fixed
}