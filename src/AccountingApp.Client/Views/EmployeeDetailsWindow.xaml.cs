using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AccountingApp.Core.Entities;
using AccountingApp.Web.Bonuses;
using AccountingApp.Web.Employees;
using AccountingApp.Web.Positions;
using AccountingApp.Web.Reports;
using RestSharp;

namespace AccountingApp.Client.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDetailsWindow.xaml
    /// </summary>
    public partial class EmployeeDetailsWindow : Window
    {
        private readonly EmployeeRecord? _employeeRecord;

        public EmployeeDetailsWindow()
        {
            InitializeComponent();
            ReportsDatePicker.SelectedDate = DateTime.Now;
        }

        public EmployeeDetailsWindow(EmployeeRecord employeeRecord) : this()
        {
            _employeeRecord = employeeRecord;
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);

            var request = new CreateBonusRequest
            {
                Title = BonusName.Text,
                Date = DateOnly.FromDateTime(BonusDate.SelectedDate!.Value),
                Value = double.Parse(BonusValue.Text),
                BonusType = BonusType.Text,
                EmployeeId = _employeeRecord!.Id
            };

            await client.PostAsync(new RestRequest("/Bonuses").AddJsonBody(request));

            await RefreshBonuses();
        }

        async Task RefreshBonuses()
        {
            var client = new RestClient(EmployeesWindow.URL);
            var bonuses = await client.GetAsync<ListBonusesResponse>(new RestRequest($"/Bonuses/{_employeeRecord.Id}"));
            BonusesGrid.ItemsSource = bonuses.Bonuses;
        }

        private async void EmployeeDetailsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await RefreshBonuses();
        }

        private async void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            var bonus = (BonusRecord)(sender as Button).CommandParameter;

            var client = new RestClient(EmployeesWindow.URL);
            await client.DeleteAsync(new RestRequest($"/Bonuses/{_employeeRecord.Id}/{bonus.Id}"));
            await RefreshBonuses();
        }

        private async void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);

            var request = new CreateReportRequest
            {
                HoursWorked = ushort.Parse(ReportHours.Text),
                Date = DateOnly.FromDateTime(ReportDate.SelectedDate!.Value),
                EmployeeId = _employeeRecord!.Id
            };

            await client.PostAsync(new RestRequest("/Reports").AddJsonBody(request));
            await RefreshReports();
        }

        private async void ButtonBase_OnClick4(object sender, RoutedEventArgs e)
        {
            var report = (ReportRecord)(sender as Button).CommandParameter;

            var client = new RestClient(EmployeesWindow.URL);
            await client.DeleteAsync(new RestRequest($"/Reports/{_employeeRecord.Id}/{report.Id}"));
            await RefreshReports();
        }

        private async Task RefreshReports()
        {
            var client = new RestClient(EmployeesWindow.URL);
            if (ReportsDatePicker.SelectedDate is null)
            {
                return;
            }
            var reports = await client.GetAsync<ListReportsResponse>(new RestRequest($"/Reports/{_employeeRecord.Id}/{ReportsDatePicker.SelectedDate.Value:yyyy-MM-dd}"));
            ReportsGrid.ItemsSource = reports.Reports;
        }

        private async void ReportsDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (_employeeRecord != null)
                await RefreshReports();
        }
    }
}
