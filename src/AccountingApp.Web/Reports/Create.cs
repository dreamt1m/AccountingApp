using AccountingApp.UseCases.Employees.Commands.Create;
using AccountingApp.UseCases.Reports.Commands.Create;
using AccountingApp.Web.Employees;

namespace AccountingApp.Web.Reports;

public class Create : Endpoint<CreateReportRequest, CreateReportResponse>
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(CreateReportRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CreateReportRequest()
            {
                EmployeeId = Guid.Empty,
                Date = DateOnly.FromDateTime(DateTime.Today),
                HoursWorked = 8
            };
        });
    }

    public override async Task HandleAsync(CreateReportRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateReportCommand(
            request.EmployeeId,
            request.Date,
            request.HoursWorked);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new CreateReportResponse(result.Value);
        }
    }
}