using System;
using System.Windows;
using System.Windows.Controls;
using BiztositasKezelo.Context_classes;

namespace OpenPage
{

    public partial class HomePage : Page
    {
        BiztositoDbContext _context = new BiztositoDbContext();
        public HomePage()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            var query = from f in _context.Felhasznalo
                        join s in _context.Szemely on f.FelhasznaloId equals s.FelhId
                        where f.FelhasznaloId == GlobalData.currentUserId
                        select new { f, s };

            var veznev = query.Select(x => x.s.Veznev).FirstOrDefault();
            var utonev = query.Select(x => x.s.Utonev).FirstOrDefault();
            tbWelcome.Text = "Üdvözöljük " + veznev + " " + utonev + "!";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new HomePage();
        }

        private void btncontractmanagement_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new ContractManagementPage();
        }

        private void btnlifeinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new LifeInsurance();
        }

        private void btncarinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new CarInsurance();
        }

        private void btnhouseinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new HomeInsurance();
        }

        private void btntravelinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new TravelInsurance();
        }

        private void btncontracts_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new ContractManagementPage();
        }

        private void btndamageevents_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new DamageEventsPage();
        }

        private void btnsettings_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new SettingsPage();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.currentUserId = 0;
            GlobalData.isAdmin = false;

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

      

    }
}
