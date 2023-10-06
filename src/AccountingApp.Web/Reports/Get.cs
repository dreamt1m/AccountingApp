using AccountingApp.UseCases.Employees.Queries.Get;
using AccountingApp.UseCases.Reports.Queries.Get;
using AccountingApp.Web.Employees;

namespace AccountingApp.Web.Reports;

public class Get : Endpoint<GetReportRequest, ReportRecord>
{
    private readonly IMediator _mediator;
        
    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(GetReportRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetReportRequest request,
        CancellationToken cancellationToken)
    {
        var command = new GetReportQuery(request.ReportId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = result.Value.Adapt<ReportRecord>();
        }
    }
}