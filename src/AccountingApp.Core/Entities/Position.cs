using AccountingApp.Core.Common;

namespace AccountingApp.Core.Entities;

public class Position : BaseEntity
{
    public string Name { get; set; }

    public double RatePerHour { get; set; }

    public double OvertimeMultiplier { get; set; }

    public double OvertimeRatePerHour => RatePerHour * OvertimeMultiplier;

    public ushort WorkingHoursPerMonth { get; set; }

    public FormOfRemuneration FormOfRemuneration { get; set; }

    public double CalculateSalary(int workedHours)
    {
        var salaryBeforeTaxes = FormOfRemuneration switch
        {
            FormOfRemuneration.Fixed => GetFixedSalary(),
            FormOfRemuneration.Hourly => GetHourlySalary(workedHours, RatePerHour),
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