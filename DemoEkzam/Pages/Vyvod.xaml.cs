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
            cbFiltr.SelectedIndex = 0;
        }
        static List<Service> list = DataBase.connection.Service.ToList();
        List<Service> list1 = list;

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0: 
                    list1 = list1.OrderBy(s => s.Costnew).ToList();
                    lstView.ItemsSource = list1;
                    break;
                case 1:
                    list1 = list1.OrderByDescending(s => s.Costnew).ToList();
                    lstView.ItemsSource = list1;
                    break;
            }
        }

        private void cbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFiltr.SelectedIndex)
            {
                case 0:
                    list1 = list;  
                    break;
                case 1:
                    list1 = list.Where(s=> s.Discount>=0 && s.Discount<0.05).ToList();
                    break;
                case 2:
                    list1 = list.Where(s => s.Discount >= 0.05 && s.Discount < 0.15).ToList();
                    break;
                case 3:
                    list1 = list.Where(s => s.Discount >= 0.15 && s.Discount < 0.30).ToList();
                    break;
                case 4:
                    list1 = list.Where(s => s.Discount >= 0.30 && s.Discount < 0.70).ToList();
                    break;
                case 5:
                    list1 = list.Where(s => s.Discount >= 0.70 && s.Discount < 1).ToList();
                    break;
            }
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    list1 = list1.OrderBy(s => s.Costnew).ToList();
                    break;
                case 1:
                    list1 = list1.OrderByDescending(s => s.Costnew).ToList();
                    break;
            }
            lstView.ItemsSource = list1;
        }
    }
}
