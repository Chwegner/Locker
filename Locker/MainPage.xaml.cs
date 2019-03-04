using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Locker
{
    /// <summary>
    /// MainPage Controller
    /// This page is loaded inside a frame("mainframe") of the window LockerMain
    /// </summary>
    public partial class MainPage : Page
    {
        private string search;
        private ArrayList list;
        private Db db = new Db();
        private string usernameCopy;


        public MainPage()
        {
            InitializeComponent();
            FillDatagrid();
        }

        // read changes in search box and reload Datagrid
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            search = searchText.Text;
            FillDatagrid(search);
        }

        // fill Datagrid from DB depending on search
        private void FillDatagrid(string search = "")
        {
            websiteTable.Items.Clear();

            list = db.SearchResults(search);

            foreach (WebsiteInfo item in list)
            {
                websiteTable.Items.Add(item);
            }
        }


        private void timerTick(object sender, EventArgs e)
        {
            // TODO 
            IDataObject data = Clipboard.GetDataObject();
            string datastring = data.GetFormats().ToString();

            if (datastring.Equals(usernameCopy))
            {
                Clipboard.Clear();
            }
        }


        private void ClipboardUsername(object sender, RoutedEventArgs e)
        {
            try
            {
                WebsiteInfo info = (WebsiteInfo)websiteTable.SelectedItem;

                long id = info.Id;
                string username = db.ReadUsername(id);
                usernameCopy = username;

                DataObject data = new DataObject(DataFormats.UnicodeText, (Object)username);
                Clipboard.SetDataObject(data, false);



                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(30);
                timer.Tick += new EventHandler(timerTick);
                timer.Start();

            }
            catch (Exception)
            {

                throw;
            }


        }

        // open window to change selected username
        private void ChangeUsername(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                WebsiteInfo info = (WebsiteInfo)websiteTable.SelectedItem;
                string username = info.Username;
                long id = info.Id;

                UsernameChange change = new UsernameChange(username, id);
                change.ShowDialog();
                FillDatagrid();
            }
            catch (NullReferenceException n)
            {
                MessageBox.Show("No Row Selected? \r\n\r\n" + n.Message);
            }

        }

        // open window to change selected website name
        private void ChangeWebsite(object sender, RoutedEventArgs e)
        {
            try
            {
                WebsiteInfo info = (WebsiteInfo)websiteTable.SelectedItem;
                string website = info.Website;
                long id = info.Id;

                WebsiteChange change = new WebsiteChange(website, id);
                change.ShowDialog();
                FillDatagrid();
            }
            catch (NullReferenceException n)
            {
                MessageBox.Show("No Row Selected? \r\n\r\n" + n.Message);
            }
        }

        // open window to change selected E-Mail Adress
        private void ChangeEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                WebsiteInfo info = (WebsiteInfo)websiteTable.SelectedItem;
                string email = info.Email;
                long id = info.Id;

                EmailChange change = new EmailChange(email, id);
                change.ShowDialog();
                FillDatagrid();
            }
            catch (NullReferenceException n)
            {
                MessageBox.Show("No Row Selected? \r\n\r\n" + n.Message);
            }
        }

        private void NewData(object sender, RoutedEventArgs e)
        {
            AddLogin add = new AddLogin();
            add.ShowDialog();
            FillDatagrid();
        }

        private void DeleteLogin(object sender, RoutedEventArgs e)
        {
            WebsiteInfo info = (WebsiteInfo)websiteTable.SelectedItem;
            long id = info.Id;

            VerifyDeletion delete = new VerifyDeletion(id);
            delete.ShowDialog();
            FillDatagrid();

        }

        /// <summary>
        /// Defining Content of Datagrid - Id only used, not shown
        /// </summary>
        public class WebsiteInfo
        {
            public long Id { get; set; }
            public string Website { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string ChangeDate { get; set; }
        }

    }
}