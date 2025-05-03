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

namespace OpenPage
{
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

            var user = _context.Felhasznalo.FirstOrDefault(u => u.FelhNev == username);
            if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(password, user.Jelszo))
            {
                MessageBox.Show("Sikeres bejelentkezés!", "Üdvözlet", MessageBoxButton.OK, MessageBoxImage.Information);
                GlobalData.currentUserId = user.FelhasznaloId;
                if (user.Jogosultsag == 1) GlobalData.isAdmin = true;
                else GlobalData.isAdmin = false;

                if(GlobalData.isAdmin == false) this.Content = new HomePage();
                else this.Content = new a_HomePage();
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


