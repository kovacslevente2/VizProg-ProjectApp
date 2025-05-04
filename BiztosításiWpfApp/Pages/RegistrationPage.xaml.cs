using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BiztositasKezelo;
using BiztositasKezelo.Context_classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.ApplicationServices;
using static OpenPage.MainWindow;

namespace OpenPage
{
    public partial class RegistrationPage : Page
    {
        BiztositoDbContext _context = new BiztositoDbContext();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtNewUser.Text;
            string password = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;
            string hash_password = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 8);
            string lastName= txtLastName.Text;
            string firstName= txtFirstName.Text;
            string birthDate = dpBirthDate.Text;
            string city=txtCity.Text;

            var users = _context.Felhasznalo
                                .Where(i => i.FelhNev == username)
                                .Select(i => i.FelhNev)
                                .ToList();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(birthDate) ||
                string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(!users.IsNullOrEmpty()) {
                MessageBox.Show("A felhasználónév már foglalt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("A jelszavak nem egyeznek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string[] bd = birthDate.Split('.');
            DateOnly birthd = new DateOnly(int.Parse(bd[0]), int.Parse(bd[1]), int.Parse(bd[2]));

            var felh = new Felhasznalo
            {
                FelhNev = username,
                Jelszo = hash_password,
                Jogosultsag = 2
            };

            var szem = new Szemely
            {
                Veznev = lastName,
                Utonev = firstName,
                SzulDatum = birthd,
                Varos = city,
                Felh = felh
            };

            _context.Felhasznalo.Add(felh);
            _context.Szemely.Add(szem);
            _context.SaveChanges();

            MessageBox.Show("Sikeres regisztráció!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            Logger.Log("Regisztrálás, felhasználónév: "+username);
            txtNewUser.Text="";
            txtNewPassword.Password = "";
            txtConfirmPassword.Password="";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            dpBirthDate.Text = "";
            txtCity.Text = "";
        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();

            Window.GetWindow(this)?.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}