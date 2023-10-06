namespace AccountingApp.Web.Reports
{
    public class CreateReportResponse
    {
        public CreateReportResponse(Guid reportId)
        {
            ReportId = reportId;
        }
        public Guid ReportId { get; set; }
    }
}
