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
    /// Interaction logic for VerifyDeletion.xaml
    /// </summary>
    public partial class VerifyDeletion : Window
    {
        private long id;
        private Db db = new Db();

        public VerifyDeletion(long id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void CancelDeletion(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Deletion(object sender, RoutedEventArgs e)
        {
            db.DeleteLogin(id);
            this.Close();
        }

    }

    
}
