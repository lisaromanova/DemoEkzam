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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoEkzam.Classes;
using DemoEkzam.Pages;

namespace DemoEkzam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataBase.connection = new DBEntities();
            FrameLoad.frmLoad = frmMain;
            FrameLoad.frmLoad.Navigate(new Vyvod());
        }

        private void txtAdmin_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (txtAdmin.Text == "0000")
            {
                Service.AdminButtons = Visibility.Visible;
                FrameLoad.frmLoad.Navigate(new Vyvod());
            }
            else
            {
                Service.AdminButtons = Visibility.Hidden;
                FrameLoad.frmLoad.Navigate(new Vyvod());
            }
        }
    }
}
