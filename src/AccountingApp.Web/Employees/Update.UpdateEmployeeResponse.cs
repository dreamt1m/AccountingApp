namespace AccountingApp.Web.Employees;

public class UpdateEmployeeResponse
{
    public UpdateEmployeeResponse(EmployeeRecord employee)
    {
        Employee = employee;
    }

    public EmployeeRecord Employee { get; set; }
}