using System.IO;
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

namespace OpenPage
{
    public class Logger
    {
        private static string logFilePath = "C://Users/istva/source/repos/VizProg-ProjectApp/BiztositasKezelo-FinalVersion/log.txt";
        public static void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }

    

    public static class GlobalData
    {
        public static int currentUserId { get; set; }
        public static bool isAdmin { get; set; }
    }

    public partial class MainWindow : Window
    {
        
        BiztositoDbContext _context = new BiztositoDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }
  

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) ||
              string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = _context.Felhasznalo.FirstOrDefault(u => u.FelhNev == username);
            if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(password, user.Jelszo))
            {
                MessageBox.Show("Sikeres bejelentkezés!", "Üdvözlet", MessageBoxButton.OK, MessageBoxImage.Information);
                GlobalData.currentUserId = user.FelhasznaloId;
                Logger.Log("Bejelentkezés, ID: "+GlobalData.currentUserId);
                if (user.Jogosultsag == 1) GlobalData.isAdmin = true;
                else GlobalData.isAdmin = false;

                if(GlobalData.isAdmin == false) this.Content = new HomePage();
                else this.Content = new admin_HomePage();
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new RegistrationPage();
        }

   
    }
}


