using System;
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
using AccountingApp.Web.Employees;
using AccountingApp.Web.Positions;
using RestSharp;

namespace AccountingApp.Client.Views
{
    /// <summary>
    /// Interaction logic for CreatePositionWindow.xaml
    /// </summary>
    public partial class CreatePositionWindow : Window
    {
        private readonly PositionRecord? _positionRecord;

        public CreatePositionWindow()
        {
            InitializeComponent();
        }

        public CreatePositionWindow(PositionRecord positionRecord) : this()
        {
            _positionRecord = positionRecord;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(EmployeesWindow.URL);

            if (_positionRecord != null)
            {
                var request1 = new UpdatePositionRequest()
                {
                    PositionId = _positionRecord.Id,
                    Name = PositionName.Text,
                    RatePerHour = double.Parse(PositionRatePerHour.Text),
                    OvertimeMultiplier = double.Parse(PositionOvertimeMultiplier.Text),
                    WorkingHoursPerMonth = ushort.Parse(PositionWorkingHoursPerMonth.Text),
                    FormOfRemuneration = PositionFormOfRemuneration.Text
                };

                await client.PutAsync(new RestRequest($"/Positions/{_positionRecord.Id}").AddJsonBody(request1));

                Close();

                return;
            }

            var request = new CreatePositionRequest()
            {
                Name = PositionName.Text,
                RatePerHour = double.Parse(PositionRatePerHour.Text),
                OvertimeMultiplier = double.Parse(PositionOvertimeMultiplier.Text),
                WorkingHoursPerMonth = ushort.Parse(PositionWorkingHoursPerMonth.Text),
                FormOfRemuneration = PositionFormOfRemuneration.Text
            };

            await client.PostAsync(new RestRequest("/Positions").AddJsonBody(request));

            Close();
        }

        private void CreatePositionWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_positionRecord is not null)
            {
                PositionName.Text = _positionRecord.Name;
                PositionRatePerHour.Text = _positionRecord.RatePerHour.ToString();
                PositionOvertimeMultiplier.Text = _positionRecord.OvertimeMultiplier.ToString();
                PositionWorkingHoursPerMonth.Text = _positionRecord.WorkingHoursPerMonth.ToString();
                PositionFormOfRemuneration.Text = _positionRecord.FormOfRemuneration;
            }
        }
    }
}
