using System;
using System.Windows;
using System.Windows.Controls;
using BiztositasKezelo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OpenPage
{
    
    public partial class LifeInsurance : Page
    {
        BiztositoDbContext _context = new BiztositoDbContext();
        public LifeInsurance()
        {
            InitializeComponent();
            List<string> insurers;
            insurers = _context.Biztosito
                                .Where(i => i.Tipus=="elet")
                                .Select(i => i.Nev)
                                .ToList();
            cbInsurer.ItemsSource = insurers;
            
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new HomePage();
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

        private void btncontractmanagement_Click(object sender, RoutedEventArgs e)
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
        }

        private void btnContract_Click(object sender, RoutedEventArgs e)
        {
            var user = _context.Felhasznalo.FirstOrDefault(u => u.FelhasznaloId == GlobalData.currentUserId);
            var insurer= _context.Biztosito.FirstOrDefault(u => u.Nev == cbInsurer.Text);
            string amount = tbAmount.Text;
            int month = int.Parse(tbMonth.Text);
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);

            var szerz = new Szerzodes
            {
                Osszeg = amount,
                Datum = dateNow,
                Honap = month,
                Bizt = insurer,
                Felh=user
            };
            _context.Szerzodes.Add(szerz);
            _context.SaveChanges();
            MessageBox.Show("Sikeres szerződés kötés!", "Sikeres művelet", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
