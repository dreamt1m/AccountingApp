using AccountingApp.UseCases.Positions.Queries.Get;
using Ardalis.Result;
using FastEndpoints;
using Mapster;
using MediatR;

namespace AccountingApp.Web.Positions;

public class Get : Endpoint<GetPositionRequest, PositionRecord>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(GetPositionRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetPositionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new GetPositionQuery(request.PositionId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = result.Value.Adapt<PositionRecord>();
        }
    }
}