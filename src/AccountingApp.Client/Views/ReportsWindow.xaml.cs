using AccountingApp.Core.Entities;
using AccountingApp.Web.Employees;
using RestSharp;
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

namespace AccountingApp.Client.Views
{
    /// <summary>
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : Window
    {
        public ReportsWindow()
        {
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Now;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);
            if (DatePicker.SelectedDate == null) return;

            var employees = await client.GetAsync<ListMonthStatisticsEmployeesResponse>(
                new RestRequest($"/Employees/EmployeeMonthStatistics/{DatePicker.SelectedDate.Value:yyyy-MM-dd}"));

            EmplGrid.ItemsSource = employees.Employees;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);
            if (DatePicker.SelectedDate == null) return;

            var employees = await client.GetAsync<ListMonthStatisticsEmployeesResponse>(
                new RestRequest($"/Employees/EmployeeMonthStatistics/{DatePicker.SelectedDate.Value:yyyy-MM-dd}"));

            EmplGrid.ItemsSource = employees.Employees.Where(e => e.OvertimeHours > 0);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);
            if (DatePicker.SelectedDate == null) return;

            var employees = await client.GetAsync<ListMonthStatisticsEmployeesResponse>(
                new RestRequest($"/Employees/EmployeeMonthStatistics/{DatePicker.SelectedDate.Value:yyyy-MM-dd}"));

            EmplGrid.ItemsSource = employees.Employees.Where(e => e.Salary < double.Parse(SalaryFilter.Text));
        }
    }
}
