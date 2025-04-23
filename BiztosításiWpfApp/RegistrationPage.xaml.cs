using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace OpenPage
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string _username = txtNewUser.Text;
            string _email = txtEmail.Text;
            string _password = txtNewPassword.Password;
            string _confirmPassword = txtConfirmPassword.Password;

            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_email) ||
                string.IsNullOrWhiteSpace(_password) || string.IsNullOrWhiteSpace(_confirmPassword))
            {
                MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_password != _confirmPassword)
            {
                MessageBox.Show("A jelszavak nem egyeznek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //
            MessageBox.Show("Sikeres regisztráció!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);


           
        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();

            Window.GetWindow(this)?.Close();
        }
    }
}