using AccountingApp.Core.Common;

namespace AccountingApp.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        public Position Position { get; set; }

        public IList<Bonus> Bonuses { get; set; }

        public IList<Report> Reports { get; set; }

        public double GetSalary(DateOnly date)
        {
            var workedHours = GetWorkedHours(date);

            var salary = Position.CalculateSalary(workedHours);
            var salaryWithBonuses = AppendBonuses();

            var salaryAfterTaxes = SubtractTaxes(salaryWithBonuses);

            return salaryAfterTaxes;

            double AppendBonuses()
            {
                var bonuses = Bonuses.Where(e => e.Date.Month == date.Month && e.Date.Year == date.Year);

                var tempSalary = salary;

                foreach (var bonus in bonuses)
                {
                    tempSalary += bonus.CalculateBonus(salary);
                }

                return tempSalary;
            }

            static double SubtractTaxes(double salary) => salary * 0.88;
        }

        public ushort GetWorkedHours(DateOnly date)
        {
            var reports = Reports.Where(e => e.Date.Month == date.Month && e.Date.Year == date.Year);
            var totalHoursWorked = reports.Sum(e => e.HoursWorked);
            return (ushort)totalHoursWorked;
        }

        public ushort GetOvertimeHours(DateOnly date)
        {
            var workedTime = GetWorkedHours(date);
            var difference = workedTime - Position.WorkingHoursPerMonth;
            return (ushort)(difference > 0 ? difference : 0);
        }
    }
}
