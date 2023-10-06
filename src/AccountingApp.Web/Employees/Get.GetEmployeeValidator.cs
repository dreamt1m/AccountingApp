using FluentValidation;

namespace AccountingApp.Web.Employees;

public class GetEmployeeValidator : Validator<GetEmployeeRequest>
{
    public GetEmployeeValidator()
    {
        RuleFor(e => e.EmployeeId)
            .NotEmpty();
    }
}