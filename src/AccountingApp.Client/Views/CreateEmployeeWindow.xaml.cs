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
using AccountingApp.Web.Employees;
using AccountingApp.Web.Positions;
using RestSharp;

namespace AccountingApp.Client.Views
{
    /// <summary>
    /// Interaction logic for CreateEmployeeWindow.xaml
    /// </summary>
    public partial class CreateEmployeeWindow : Window
    {
        private readonly EmployeeRecord? _employeeRecord = null;

        public CreateEmployeeWindow()
        {
            InitializeComponent();
        }

        public CreateEmployeeWindow(EmployeeRecord employeeRecord) : this()
        {
            _employeeRecord = employeeRecord;
        }

        private async void CreateEmployeeWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);
            var positions = await client.GetAsync<ListPositionsResponse>(new RestRequest("/Positions"));
            PositionComboBox.ItemsSource = positions!.Positions;

            if (_employeeRecord is not null)
            {
                NameTextBox.Text = _employeeRecord.Name;
                PositionComboBox.SelectedItem = _employeeRecord.Position;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);


            if (_employeeRecord is not null)
            {
                var request1 = new UpdateEmployeeRequest
                {
                    EmployeeId = _employeeRecord.Id,
                    Name = NameTextBox.Text,
                    PositionId = ((PositionRecord)PositionComboBox.SelectedItem).Id,
                };

                await client.PutAsync(new RestRequest($"/Employees/{_employeeRecord.Id}").AddJsonBody(request1));

                Close();

                return;
            }

            var request = new CreateEmployeeRequest
            {
                Name = NameTextBox.Text,
                PositionId = ((PositionRecord)PositionComboBox.SelectedItem).Id,
            };

            await client.PostAsync(new RestRequest("/Employees").AddJsonBody(request));

            Close();
        }
    }
}
