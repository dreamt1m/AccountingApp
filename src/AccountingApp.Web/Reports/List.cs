using AccountingApp.UseCases.Employees.Commands.Delete;
using AccountingApp.UseCases.Reports.Queries.List;
using AccountingApp.Web.Employees;

namespace AccountingApp.Web.Reports;

public class List : Endpoint<ListReportsRequest, ListReportsResponse>
{
    private readonly IMediator _mediator;

    public List(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(ListReportsRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new ListReportsRequest
            {
                EmployeeId = Guid.Empty,
                Date = DateTime.Today
            };
        });
    }

    public override async Task HandleAsync(ListReportsRequest request, CancellationToken cancellationToken)
    {
        var command = new ListReportsQuery(request.EmployeeId, DateOnly.FromDateTime(request.Date));

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            Response = new ListReportsResponse()
            {
                Reports = result.Value.Adapt<IEnumerable<ReportRecord>>().ToList(),
            };
        }
    }
}