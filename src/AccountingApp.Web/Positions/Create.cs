using AccountingApp.UseCases.Positions.Commands.Create;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace AccountingApp.Web.Positions
{
    public class Create : Endpoint<CreatePositionRequest, CreatePositionResponse>
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post(CreatePositionRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new CreatePositionRequest
                {
                    Name = "Driver",
                    RatePerHour = 10,
                    WorkingHoursPerMonth = 160,
                    OvertimeMultiplier = 2,
                    FormOfRemuneration = "HOURLY"
                };
            });
        }

        public override async Task HandleAsync(CreatePositionRequest request, CancellationToken cancellationToken)
        {
            var command = new CreatePositionCommand(request.Name!,
                request.RatePerHour!.Value,
                request.OvertimeMultiplier!.Value,
                request.WorkingHoursPerMonth!.Value,
                request.FormOfRemuneration!);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                Response = new CreatePositionResponse(result.Value, request.Name!);
            }
        }
    }
}