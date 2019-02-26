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
        MainPage main = new MainPage();
        object selectedItem;

        public UsernameChange()
        {
            InitializeComponent();            
        }

        private void TextBox_Initialized(object sender, EventArgs e)
        {
           
        }
    }
}
