using System;
using System.Windows;
using System.Windows.Controls;
using BiztositasKezelo;
using BiztositasKezelo.Context_classes;

namespace OpenPage
{
    public partial class HomeInsurance : Page
    {
        BiztositoDbContext _context = new BiztositoDbContext();
        public HomeInsurance()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            List<string> insurers;
            insurers = _context.Biztosito
                                .Where(i => i.Tipus == "lakas")
                                .Select(i => i.Nev)
                                .ToList();
            cbInsurer.ItemsSource = insurers;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new HomePage();
        }

        private void btnlifeinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new LifeInsurance();
        }

        private void btncarinsurance_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new CarInsurance();
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
            if (string.IsNullOrWhiteSpace(tbAmount.Text) ||
             string.IsNullOrWhiteSpace(tbMonth.Text))
            {
                MessageBox.Show("Kérjük, töltse ki az összes mezõt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int number;
            if (int.TryParse(tbAmount.Text, out number) && int.TryParse(tbMonth.Text, out number))
            {
                if (int.Parse(tbAmount.Text) < 5000)
                {
                    MessageBox.Show("Az összegnek minimum 5000 Ft-nak kell lennie!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (int.Parse(tbMonth.Text) < 3)
                {
                    MessageBox.Show("Az hónapok számának minimum 3-nak kell lennie!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = _context.Felhasznalo.FirstOrDefault(u => u.FelhasznaloId == GlobalData.currentUserId);
                var insurer = _context.Biztosito.FirstOrDefault(u => u.Nev == cbInsurer.Text);
                string amount = tbAmount.Text;
                int month = int.Parse(tbMonth.Text);
                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);

                var szerz = new Szerzodes
                {
                    Osszeg = amount,
                    Datum = dateNow,
                    Honap = month,
                    Bizt = insurer,
                    Felh = user
                };
                _context.Szerzodes.Add(szerz);
                _context.SaveChanges();
                MessageBox.Show("Sikeres szerzõdés kötés!", "Sikeres mûvelet", MessageBoxButton.OK, MessageBoxImage.Information);
                Logger.Log("Szerzõdés kötés (lakásbiztosítás), ID: " + GlobalData.currentUserId);
                tbAmount.Text = "";
                tbMonth.Text = "";
            }
            else
            {
                MessageBox.Show("A mezõkbe csak számértéket adhat meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
} 