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
using System.Windows.Shapes;

namespace Locker
{
    /// <summary>
    /// Interaktionslogik für UsernameChange.xaml
    /// </summary>
    public partial class UsernameChange : Window
    {
        long websiteID;
        Db db = new Db();

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
