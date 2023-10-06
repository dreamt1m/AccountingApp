using AccountingApp.Infrastructure.Data.Configurations;
using FastEndpoints;
using FluentValidation;

namespace AccountingApp.Web.Employees;

public class UpdateEmployeeValidator : Validator<UpdateEmployeeRequest>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty();

        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(2)
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);

        RuleFor(e => e.PositionId)
            .NotEmpty();
    }
}