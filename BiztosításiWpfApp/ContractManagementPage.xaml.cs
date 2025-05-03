using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using BiztositasKezelo;

namespace OpenPage
{   
    public partial class ContractManagementPage : Page
    {
        
        BiztositoDbContext _context = new BiztositoDbContext();
        public ContractManagementPage()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            var eredmeny = _context.Szerzodes
               .Join(_context.Felhasznalo, szerzodes => szerzodes.FelhId, felhasznalo => felhasznalo.FelhasznaloId,
                     (szerzodes, felhasznalo) => new { Szerzodes = szerzodes, Felhasznalo = felhasznalo })
               .Join(_context.Biztosito, temp => temp.Szerzodes.BiztId, biztosito => biztosito.BiztositoId,
                     (temp, biztosito) => new
                     {
                         temp.Szerzodes, // Megõrizzük a teljes Szerzodes objektumot
                         temp.Felhasznalo, // Megõrizzük a teljes Felhasznalo objektumot
                         Biztosito = biztosito // Biztosito objektumot is tároljuk
                     })
               .Where(temp => temp.Felhasznalo.FelhasznaloId == GlobalData.currentUserId) // Szûrés az aktuális felhasználóra
               .Select(temp => new
               {
                   SzerzodesId = temp.Szerzodes.SzerzodesId,
                   BiztositoNev = temp.Biztosito.Nev,
                   BiztositoTipus = temp.Biztosito.Tipus,
                   Osszeg = temp.Szerzodes.Osszeg,
                   Honap = temp.Szerzodes.Honap,
                   Datum = temp.Szerzodes.Datum
               })
               .ToList();
            myDataGrid.ItemsSource = eredmeny.ToList();
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
   
        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = myDataGrid.SelectedItem;
            int contract_Id = selectedItem?.SzerzodesId ?? 0;
            if (selectedItem != null)
            {
                var deleteDamEvent = _context.Karesemeny.Where(m => m.SzerzId == contract_Id).ToList();
                for (int i = 0; i < deleteDamEvent.Count; i++)
                {
                    _context.Karesemeny.Remove(deleteDamEvent[i]);
                    _context.SaveChanges();
                }
                var deleteContract = _context.Szerzodes.Where(m => m.SzerzodesId == contract_Id).Single();
                _context.Szerzodes.Remove(deleteContract);
                _context.SaveChanges();
                Load();
            }
        }
    }
} 