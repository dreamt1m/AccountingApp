using AccountingApp.UseCases.Employees.Commands.Delete;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace AccountingApp.Web.Employees;

public class Delete : Endpoint<DeleteEmployeeRequest>
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(DeleteEmployeeRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new DeleteEmployeeRequest
            {
                EmployeeId = Guid.Empty,
            };
        });
    }

    public override async Task HandleAsync(DeleteEmployeeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEmployeeCommand(request.EmployeeId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status is ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
        }

        if (result.IsSuccess)
        {
            await SendOkAsync(cancellationToken);
        }
    }
}