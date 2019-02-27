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
    public partial class WebsiteChange : Window
    {
        long websiteID;
        Db db = new Db();

        public WebsiteChange(string website, long id)
        {
            InitializeComponent();

            websiteBox.Text = website;
            websiteID = id;
        }

        private void SaveWebsite(object sender, RoutedEventArgs e)
        {
            string newWebsite = websiteBox.Text;
            db.ChangeWebsite(newWebsite, websiteID);
            this.Close();
        }
    }
}
