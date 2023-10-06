namespace AccountingApp.Web.Employees;

public class GetEmployeeRequest
{
    public const string Route = "/Employees/{EmployeeId:Guid}";

    public Guid EmployeeId { get; set; }
}