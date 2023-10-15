using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AccountingApp.Core.Entities;
using AccountingApp.Web.Employees;
using RestSharp;

namespace AccountingApp.Client.Views
{
    /// <summary>
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        public const string URL = "http://localhost:5269";
        public ObservableCollection<EmployeeRecord?> Employees { get; set; } = new();

        public EmployeesWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected override async void OnActivated(EventArgs e)
        {
            await Refresh();
        }

        async Task Refresh()
        {
            var client = new RestClient(URL);
            var employees = await client.GetAsync<ListEmployeesResponse>(new RestRequest("/Employees"));

            Employees.Clear();

            foreach (var employee in employees!.Employees)
            {
                Employees.Add(employee);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new CreateEmployeeWindow();
            window.ShowDialog();
        }

        private async void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            var employee = (EmployeeRecord)(sender as Button).CommandParameter;

            var client = new RestClient(URL);
            await client.DeleteAsync(new RestRequest($"/Employees/{employee.Id}"));
            await Refresh();
        }

        private async void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            var employee = (EmployeeRecord)(sender as Button).CommandParameter;

            var window = new CreateEmployeeWindow(employee);
            window.ShowDialog();
        }

        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            var employee = (EmployeeRecord)(sender as Button).CommandParameter;

            var window = new EmployeeDetailsWindow(employee);
            window.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var window = new ReportsWindow();
            window.ShowDialog();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ChartWindow();
            window.ShowDialog();
        }

        private void ButtonBase_OnClick9(object sender, RoutedEventArgs e)
        {
            var window = new PositionsWindow();
            window.ShowDialog();
        }
    }
}
