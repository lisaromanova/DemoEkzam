using DemoEkzam.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoEkzam.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            
            DateTime tomorrow = DateTime.Today.AddDays(2);
            List<ClientService> list = DataBase.connection.ClientService.Where(x => x.StartTime >= DateTime.Now && x.StartTime <= tomorrow).OrderBy(x=>x.StartTime).ToList();
            lstView.ItemsSource = list;
            if (list.Count==0)
            {
                tbEmpty.Visibility = Visibility.Visible;
                lstView.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbEmpty.Visibility = Visibility.Collapsed;
                lstView.Visibility = Visibility.Visible;
            }
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(RefreshPage);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        void RefreshPage(object sender, EventArgs e)
        {
            FrameLoad.frmAdmin.Navigate(new AdminPage());
        }
    }
}
