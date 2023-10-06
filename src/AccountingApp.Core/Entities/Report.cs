using AccountingApp.BuildingBlocks.Models;

namespace AccountingApp.Core.Entities;

public class Report : EntityBase
{
    private Report() { }

    public Report(DateOnly date, ushort hoursWorked)
    {
        if (hoursWorked > 24)
            throw new ArgumentOutOfRangeException(nameof(hoursWorked), hoursWorked, "Invalid report hours amount.");

        Date = date;
        HoursWorked = hoursWorked;
    }

    public Employee Employee { get; private set; }

    public DateOnly Date { get; private set; }

    public ushort HoursWorked { get; private set; }
}