using AccountingApp.UseCases.Bonuses.Queries.List;

namespace AccountingApp.Web.Bonuses;

public class List : Endpoint<ListBonusesRequest, ListBonusesResponse>
{
    private readonly IMediator _mediator;

    public List(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(ListBonusesRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new ListBonusesRequest
            {
                EmployeeId = Guid.Empty,
            };
        });
    }

    public override async Task HandleAsync(ListBonusesRequest request, CancellationToken cancellationToken)
    {
        var command = new ListBonusesQuery(request.EmployeeId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            Response = new ListBonusesResponse
            {
                Bonuses = result.Value.Adapt<IEnumerable<BonusRecord>>().ToList(),
            };
        }
    }
}