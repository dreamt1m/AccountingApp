using AccountingApp.BuildingBlocks.Models;

namespace AccountingApp.Core.Entities
{
    public class Employee : EntityBase
    {
        private Employee() { }

        public Employee(string name, Position position)
        {
            UpdateName(name);
            UpdatePosition(position);
        }

        public string Name { get; private set; }

        public Position Position { get; private set; } 

        public List<Bonus> Bonuses { get; private set; } = new();

        public List<Report> Reports { get; private set; } = new();

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void UpdatePosition(Position position)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public void AddReport(Report report)
        {
            Reports.Add(report);
        }

        public void RemoveReport(Report report)
        {
            Reports.Remove(report); // TODO: check
        }

        public void AddBonus(Bonus bonus)
        {
            Bonuses.Add(bonus);
        }

        public void RemoveBonus(Bonus bonus)
        {
            Bonuses.Remove(bonus);
        }

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
