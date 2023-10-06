using AccountingApp.UseCases.Employees.Queries.ListMonthStatistics;
using AccountingApp.UseCases.Reports.Queries.List;
using AccountingApp.Web.Reports;

namespace AccountingApp.Web.Employees;

public class ListMonthStatistics : Endpoint<ListMonthStatisticsEmployeesRequest, ListMonthStatisticsEmployeesResponse>
{
    private readonly IMediator _mediator;

    public ListMonthStatistics(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(ListMonthStatisticsEmployeesRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new ListMonthStatisticsEmployeesRequest
            {
                Date = DateTime.Today
            };
        });
    }

    public override async Task HandleAsync(ListMonthStatisticsEmployeesRequest request, CancellationToken cancellationToken)
    {
        var command = new ListEmployeeMonthStatisticsQuery(DateOnly.FromDateTime(request.Date));

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            Response = new ListMonthStatisticsEmployeesResponse()
            {
                Employees = result.Value.Adapt<IEnumerable<EmployeeMonthStatisticsRecord>>().ToList(),
            };
        }
    }
}