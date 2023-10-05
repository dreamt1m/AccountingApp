using AccountingApp.UseCases.Positions.Queries.List;
using FastEndpoints;
using Mapster;
using MediatR;

namespace AccountingApp.Web.Positions;

public class List : EndpointWithoutRequest<ListPositionsResponse>
{
    private readonly IMediator _mediator;

    public List(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/Positions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new ListPositionsQuery();
        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = new ListPositionsResponse
            {
                Positions = result.Value.Adapt<IEnumerable<PositionRecord>>().ToList(),
            };
        }
    }
}