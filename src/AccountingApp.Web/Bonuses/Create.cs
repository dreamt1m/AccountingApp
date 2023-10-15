using AccountingApp.UseCases.Bonuses.Commands.Create;

namespace AccountingApp.Web.Bonuses;

public class Create : Endpoint<CreateBonusRequest, CreateBonusResponse>
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(CreateBonusRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CreateBonusRequest()
            {
                EmployeeId = Guid.Empty,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Value = 100,
                BonusType = "FixedPayment",
                Title = "Monthly bonus"
            };
        });
    }

    public override async Task HandleAsync(CreateBonusRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBonusCommand(
            request.EmployeeId,
            request.Title!,
            request.Date,
            request.Value!.Value,
            request.BonusType!);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.Status == ResultStatus.Error)
        {
            foreach (var error in result.Errors)
            {
                AddError(error);
            }
        }

        if (result.IsSuccess)
        {
            Response = new CreateBonusResponse(result.Value);
        }
    }
}