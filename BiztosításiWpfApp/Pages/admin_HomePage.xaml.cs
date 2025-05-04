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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BiztositasKezelo.Context_classes;
using Microsoft.EntityFrameworkCore;
using OpenPage;  // MainWindow osztály névtere

namespace BiztositasKezelo
{

    public partial class admin_HomePage : Page
    {
        BiztositoDbContext _context = new BiztositoDbContext();
        public admin_HomePage()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            List<string> users;
            users = _context.Felhasznalo
                                .Where(i=>i.FelhNev!="admin")
                                .Select(i => i.FelhNev)
                                .ToList();
            cbUsers.ItemsSource = users;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var query = _context.Szerzodes
              .Join(_context.Felhasznalo, szerzodes => szerzodes.FelhId, felhasznalo => felhasznalo.FelhasznaloId,
                    (szerzodes, felhasznalo) => new { Szerzodes = szerzodes, Felhasznalo = felhasznalo })
              .Join(_context.Biztosito, temp => temp.Szerzodes.BiztId, biztosito => biztosito.BiztositoId,
                    (temp, biztosito) => new
                    {
                        temp.Szerzodes,
                        temp.Felhasznalo,
                        Biztosito = biztosito
                    })
              .Where(temp => temp.Felhasznalo.FelhNev == cbUsers.SelectedItem)
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
            myDataGrid.ItemsSource = query.ToList();
        }
    }
}
