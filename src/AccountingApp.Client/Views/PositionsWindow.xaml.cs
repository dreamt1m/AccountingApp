using AccountingApp.Web.Employees;
using AccountingApp.Web.Positions;
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
    /// Interaction logic for PositionsWindow.xaml
    /// </summary>
    public partial class PositionsWindow : Window
    {
        public PositionsWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            var position = (PositionRecord)(sender as Button).CommandParameter;

            var client = new RestClient(EmployeesWindow.URL);
            await client.DeleteAsync(new RestRequest($"/Positions/{position.Id}"));
            await Refresh();
        }

        private async void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            var position = (PositionRecord)(sender as Button).CommandParameter;

            var window = new CreatePositionWindow(position);
            window.Show();
            await Refresh();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        private async Task Refresh()
        {
            var client = new RestClient(EmployeesWindow.URL);
            var positions = await client.GetAsync<ListPositionsResponse>(new RestRequest("/Positions"));
            PositionsGreed.ItemsSource = positions!.Positions;
        }

        private async void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            var window = new CreatePositionWindow();
            window.Show();
            await Refresh();
        }

        private async void PositionsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        protected override async void OnActivated(EventArgs e)
        {
            await Refresh();
        }
    }
}
