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
    /// Interaction logic for EmailChange.xaml
    /// </summary>
    public partial class EmailChange : Window
    {
        long websiteID;
        Db db = new Db();

        public EmailChange(string email, long id)
        {
            InitializeComponent();

            emailBox.Text = email;
            websiteID = id;
        }

        private void SaveEmail(object sender, RoutedEventArgs e)
        {
            string newEmail = emailBox.Text;
            db.ChangeEmail(newEmail, websiteID);
            this.Close();
        }
    }
}
