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
    /// Interaction logic for AddLogin.xaml
    /// </summary>
    public partial class AddLogin : Window
    {
        private Db db = new Db();

        public AddLogin()
        {
            InitializeComponent();
        }

        private void SaveNewLogin(object sender, RoutedEventArgs e)
        {
            db.AddLogin(websiteBox.Text, usernameBox.Text, emailBox.Text);
            this.Close();
        }
    }
}
