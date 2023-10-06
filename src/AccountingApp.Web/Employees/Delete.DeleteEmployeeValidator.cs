using FastEndpoints;
using FluentValidation;

namespace AccountingApp.Web.Employees;

public class DeleteEmployeeValidator : Validator<DeleteEmployeeRequest>
{
    public DeleteEmployeeValidator()
    {
        RuleFor(e => e.EmployeeId)
            .NotEmpty();
    }
}