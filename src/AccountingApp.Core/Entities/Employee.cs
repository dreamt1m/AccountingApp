using System.ComponentModel.DataAnnotations;
using AccountingApp.BuildingBlocks.Models;

namespace AccountingApp.Core.Entities
{
    public class Employee : EntityBase
    {
        private Employee() { }

        public Employee(string name, Position position)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public string Name { get; private set; }

        public Position Position { get; private set; } 

        public IEnumerable<Bonus> Bonuses { get; private set; } = new List<Bonus>();

        public IEnumerable<Report> Reports { get; private set; } = new List<Report>();

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
