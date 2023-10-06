using AccountingApp.Infrastructure.Data.Configurations;
using FastEndpoints;
using FluentValidation;

namespace AccountingApp.Web.Employees;

public class CreateEmployeeValidator : Validator<CreateEmployeeRequest>
{
    public CreateEmployeeValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(2)
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);

        RuleFor(e => e.PositionId)
            .NotEmpty();
    }
}