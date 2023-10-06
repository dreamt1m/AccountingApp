using AccountingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.UseCases.Reports.Queries.Get;

public class GetReportQueryHandler : IQueryHandler<GetReportQuery, Result<ReportDto>>
{
    private readonly AccountingDbContext _context;

    public GetReportQueryHandler(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ReportDto>> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _context.Reports.FirstOrDefaultAsync(e => e.Id == request.ReportId, cancellationToken);

        if (report is null)
        {
            return Result.NotFound("Report not found");
        }

        return Result.Success(report.Adapt<ReportDto>());
    }
}