namespace AccountingApp.Web.Employees;

public class ListEmployeesResponse
{
    public List<EmployeeRecord> Employees { get; set; } = new();
}