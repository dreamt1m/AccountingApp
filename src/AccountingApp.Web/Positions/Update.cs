using AccountingApp.UseCases.Positions.Commands.Update;
using Ardalis.Result;
using FastEndpoints;
using Mapster;
using MediatR;

namespace AccountingApp.Web.Positions;

public class Update : Endpoint<UpdatePositionRequest, UpdatePositionResponse>
{
    private readonly IMediator _mediator;

    public Update(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put(UpdatePositionRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new UpdatePositionRequest
            {
                PositionId = Guid.Empty,
                Name = "Driver",
                RatePerHour = 10,
                OvertimeMultiplier = 2,
                WorkingHoursPerMonth = 160,
                FormOfRemuneration = "Fixed"
            };
        });
    }

    public override async Task HandleAsync(
        UpdatePositionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePositionCommand(
            request.PositionId,
            request.Name!,
            request.RatePerHour!.Value,
            request.OvertimeMultiplier!.Value,
            request.WorkingHoursPerMonth!.Value,
            request.FormOfRemuneration!);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            var responseDto = result.Value.Adapt<PositionRecord>();
            Response = new UpdatePositionResponse(responseDto);
        }
    }
}