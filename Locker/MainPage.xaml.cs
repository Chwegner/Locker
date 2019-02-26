using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Locker
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        string search;
        ArrayList list;
        Db db = new Db();

        public MainPage()
        {
            InitializeComponent();
            start();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            list.Clear();
            websiteTable.Items.Clear();
            search = searchText.Text;
            start(search);
        }

        public void start(string search = "")
        {
            list = db.SearchResults(search);

            foreach (WebsiteInfo item in list)
            {
                websiteTable.Items.Add(item);
            }
        }

        public class WebsiteInfo
        {
            public string Website { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string ChangeDate { get; set; }
        }
    }
}