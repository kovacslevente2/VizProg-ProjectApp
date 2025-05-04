using System.Windows;
using System.Windows.Controls;
using BiztositasKezelo.Context_classes;
using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OpenPage
{
    public partial class SettingsPage : Page
    {
        BiztositoDbContext _context = new BiztositoDbContext();
        public SettingsPage()
        {
            InitializeComponent();
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

        private void btnhouseinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new HomeInsurance();
        }

        private void btncarinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new CarInsurance();
        }

        private void btntravelinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new TravelInsurance();
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
            Application.Current.Shutdown();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) && string.IsNullOrWhiteSpace(txtFirstName.Text) && string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Nem változtatott egyik adaton sem!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (var context = new BiztositoDbContext())
            {
                var query = (from f in context.Felhasznalo
                             join s in context.Szemely on f.FelhasznaloId equals s.FelhId
                             where f.FelhasznaloId == GlobalData.currentUserId
                             select new { f, s }).FirstOrDefault();



                if (!string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    query.s.Veznev = txtLastName.Text;
                }
                if (!string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    query.s.Utonev = txtFirstName.Text;
                }
                if (!string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    query.s.Varos = txtAddress.Text;
                }

                context.SaveChanges();
                MessageBox.Show("A változtatás/változtatások mentése sikeres!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                Logger.Log("Személyes adatok változtatása, ID: " + GlobalData.currentUserId);
                txtLastName.Text="";
                txtFirstName.Text="";
                txtAddress.Text="";
            }
        }


    }
} 