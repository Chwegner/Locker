using System.Windows;


namespace Locker
{
    /// <summary>
    /// Interaktionslogik für UsernameChange.xaml
    /// </summary>
    public partial class UsernameChange : Window
    {
        private long websiteID;
        private Db db = new Db();

        public UsernameChange(string username, long id)
        {
            InitializeComponent();

            usernameBox.Text = username;
            websiteID = id;
        }

        private void SaveUsername(object sender, RoutedEventArgs e)
        {
            string newUsername = usernameBox.Text;
            db.ChangeUsername(newUsername, websiteID);
            this.Close();
        }
    }
}
