using AccountingApp.UseCases.Bonuses.Commands.Delete;

namespace AccountingApp.Web.Bonuses;

public class Delete : Endpoint<DeleteBonusRequest>
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(DeleteBonusRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new DeleteBonusRequest
            {
                EmployeeId = Guid.Empty,
                BonusId = Guid.Empty
            };
        });
    }

    public override async Task HandleAsync(DeleteBonusRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteBonusCommand(
            request.EmployeeId,
            request.BonusId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status is ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.Status is ResultStatus.Error)
        {
            foreach (var error in result.Errors)
            {
                AddError(error);
            }

            return;
        }

        if (result.IsSuccess)
        {
            await SendOkAsync(cancellationToken);
        }
    }
}