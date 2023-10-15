using AccountingApp.Web.Employees;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public ChartWindow()
        {
            InitializeComponent();
        }

        private async void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);
            if (DatePicker.SelectedDate == null) return;

            var bars = await client.GetAsync<List<AverageMonthSalaryPerYearRecord>>(
                new RestRequest(
                    $"/Employees/AverageMonthSalaryPerYearRequest/{DatePicker.SelectedDate.Value:yyyy-MM-dd}"));

            SalaryChart.ItemsSource = bars;
        }
    }
}
