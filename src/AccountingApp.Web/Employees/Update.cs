using AccountingApp.UseCases.Employees.Commands.Update;

namespace AccountingApp.Web.Employees;

public class Update : Endpoint<UpdateEmployeeRequest, UpdateEmployeeResponse>
{
    private readonly IMediator _mediator;

    public Update(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put(UpdateEmployeeRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new UpdateEmployeeRequest
            {
                Id = Guid.Empty,
                Name = "Tom",
                PositionId = Guid.Empty,
            };
        });
    }

    public override async Task HandleAsync(UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEmployeeCommand(request.Id, request.Name!, request.PositionId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.Status == ResultStatus.Error)
        {
            foreach (var error in result.Errors)
            {
                AddError(error);
            }
        }

        if (result.IsSuccess)
        {
            var responseDto = result.Value.Adapt<EmployeeRecord>();
            Response = new UpdateEmployeeResponse(responseDto);
        }
    }
}