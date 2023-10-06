using AccountingApp.UseCases.Employees.Queries.List;
using AccountingApp.UseCases.Positions.Queries.List;
using AccountingApp.Web.Positions;

namespace AccountingApp.Web.Employees;

public class List : EndpointWithoutRequest<ListEmployeesResponse>
{
    private readonly IMediator _mediator;

    public List(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/Employees");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var query = new ListEmployeesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            Response = new ListEmployeesResponse
            {
                Employees = result.Value.Adapt<IEnumerable<EmployeeRecord>>().ToList(),
            };
        }
    }
}