using AccountingApp.UseCases.Positions.Commands.Delete;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace AccountingApp.Web.Positions
{
    public class Delete : Endpoint<DeletePositionRequest>
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete(DeletePositionRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new DeletePositionRequest
                {
                    PositionId = Guid.Empty,
                };
            });
        }

        public override async Task HandleAsync(DeletePositionRequest req, CancellationToken ct)
        {
            var command = new DeletePositionCommand(req.PositionId);

            var result = await _mediator.Send(command, ct);

            if (result.Status is ResultStatus.NotFound)
            {
                await SendNotFoundAsync(ct);
            }

            if (result.IsSuccess)
            {
                await SendOkAsync(ct);
            }
        }
    }
}