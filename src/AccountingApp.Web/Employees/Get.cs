using AccountingApp.UseCases.Employees.Queries.Get;
using AccountingApp.UseCases.Positions.Queries.Get;
using AccountingApp.Web.Positions;

namespace AccountingApp.Web.Employees;

public class Get : Endpoint<GetEmployeeRequest, EmployeeRecord>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(GetEmployeeRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new GetEmployeeQuery(request.EmployeeId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = result.Value.Adapt<EmployeeRecord>();
        }
    }
}