using AccountingApp.Core.Common;

namespace AccountingApp.Core.Entities;

public class Report : BaseEntity
{
    public Employee Employee { get; set; }

    public DateOnly Date { get; set; }

    public ushort HoursWorked { get; set; }
}