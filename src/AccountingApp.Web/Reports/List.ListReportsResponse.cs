namespace AccountingApp.Web.Reports;

public class ListReportsResponse
{
    public List<ReportRecord> Reports { get; set; } = new();
}