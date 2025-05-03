using System.Data;
using System.Windows;
using System.Windows.Controls;
using BiztositasKezelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace OpenPage
{
    public partial class DamageEventsPage : Page
    {
        BiztositoDbContext _context = new BiztositoDbContext();
        public DamageEventsPage()
        {
            InitializeComponent();


            List<string> types;
            types = _context.Szerzodes
                .Join(_context.Felhasznalo, szerzodes => szerzodes.FelhId, felhasznalo => felhasznalo.FelhasznaloId,
                      (szerzodes, felhasznalo) => new { Szerzodes = szerzodes, Felhasznalo = felhasznalo })
                .Join(_context.Biztosito, temp => temp.Szerzodes.BiztId, biztosito => biztosito.BiztositoId,
                      (temp, biztosito) => biztosito.Tipus)
                .ToList();

            List<string> names;
            names = _context.Szerzodes
                .Join(_context.Felhasznalo, szerzodes => szerzodes.FelhId, felhasznalo => felhasznalo.FelhasznaloId,
                      (szerzodes, felhasznalo) => new { Szerzodes = szerzodes, Felhasznalo = felhasznalo })
                .Join(_context.Biztosito, temp => temp.Szerzodes.BiztId, biztosito => biztosito.BiztositoId,
                      (temp, biztosito) => biztosito.Nev)
                .ToList();

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i] == "elet") cbtype.Items.Add(names[i] + "-Élet biztosítás");
                if (types[i] == "auto") cbtype.Items.Add(names[i] + "-Autó biztosítás");
                if (types[i] == "lakas") cbtype.Items.Add(names[i] + "-Lakás biztosítás");
                if (types[i] == "utas") cbtype.Items.Add(names[i] + "-Utas biztosítás");
            }
         

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string selectedDamageType = selectedItem.Content.ToString();
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string cbtext = cbtype.Text;
            string[] strings = cbtext.Split('-');
            var InsId = _context.Biztosito
                .Where(b => b.Nev == strings[0])
                .Select(b => b.BiztositoId)
                .FirstOrDefault();

            var Cont = _context.Szerzodes
                .Where(s => s.FelhId == GlobalData.currentUserId && s.BiztId == InsId)
                .FirstOrDefault();

            //Console.WriteLine(Cont.Count);

            var dam = new Karesemeny
            {
                Megnevezes = tbText.Text,
                Szerz = Cont
            };
            _context.Karesemeny.Add(dam);
            _context.SaveChanges();
            MessageBox.Show("Sikeres káresemény hozzáadás!", "Sikeres művelet", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
} 