using AccountingApp.UseCases.Employees.Queries.ListAverageMonthSalaryPerYear;

namespace AccountingApp.Web.Employees;

public class
    ListAverageMonthSalaryPerYear : Endpoint<ListAverageMonthSalaryPerYearRequest,
        List<AverageMonthSalaryPerYearRecord>>
{
    private readonly IMediator _mediator;

    public ListAverageMonthSalaryPerYear(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(ListAverageMonthSalaryPerYearRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new ListAverageMonthSalaryPerYearRequest()
            {
                Date = DateTime.Today
            };
        });
    }

    public override async Task HandleAsync(ListAverageMonthSalaryPerYearRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ListAverageMonthSalaryPerYearQuery(DateOnly.FromDateTime(request.Date));

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            Response = result.Value.Adapt<IEnumerable<AverageMonthSalaryPerYearRecord>>().ToList();
        }
    }
}