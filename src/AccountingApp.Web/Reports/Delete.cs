using AccountingApp.UseCases.Employees.Commands.Delete;
using AccountingApp.UseCases.Reports.Commands.Delete;
using AccountingApp.Web.Employees;

namespace AccountingApp.Web.Reports;

public class Delete : Endpoint<DeleteReportRequest>
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(DeleteReportRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new DeleteReportRequest
            {
                EmployeeId = Guid.Empty,
                ReportId = Guid.Empty,
            };
        });
    }

    public override async Task HandleAsync(DeleteReportRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteReportCommand(request.EmployeeId, request.ReportId);

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