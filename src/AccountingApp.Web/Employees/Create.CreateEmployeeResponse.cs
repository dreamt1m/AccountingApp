namespace AccountingApp.Web.Employees;

public class CreateEmployeeResponse
{
    public CreateEmployeeResponse(Guid employeeId)
    {
        EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
}