
using System.Windows;


namespace Locker
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Db db = new Db();
        LockerMain locker = new LockerMain();
        Crypt crypt = new Crypt();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton(object sender, RoutedEventArgs e)
        {
            if (db.VerifyPassword(username.Text, password.Password))
            {
                locker.Show();
                this.Close();
            }
            else
            {
                errorMsg.Content = "Login fehlgeschlagen!";
            }
        }
    }
}
