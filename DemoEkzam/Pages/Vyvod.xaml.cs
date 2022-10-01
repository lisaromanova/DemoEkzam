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

namespace DemoEkzam.Pages
{
    /// <summary>
    /// Логика взаимодействия для Vyvod.xaml
    /// </summary>
    public partial class Vyvod : Page
    {
        public Vyvod()
        {
            InitializeComponent();
            cbSort.SelectedIndex = 0;
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    List<Service> listSort = DataBase.connection.Service.ToList();
                    listSort = listSort.OrderBy(s => s.Costnew).ToList();
                    lstView.ItemsSource = listSort;
                    break;
                case 1:
                    List<Service> listSortDesc = DataBase.connection.Service.OrderByDescending(s => s.Cost).ToList();
                    listSortDesc = listSortDesc.OrderByDescending(s => s.Costnew).ToList();
                    lstView.ItemsSource = listSortDesc;
                    break;
            }
        }
    }
}
