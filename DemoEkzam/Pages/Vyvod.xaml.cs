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
        int index = 0;
        int indexAll = list.Count;

        private void cbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTitle(list);
            Filtr(list1);
            Sort();
        }

        void Filtr(List<Service> L)
        {
            switch (cbFiltr.SelectedIndex)
            {
                case 0:
                    list1 = list;
                    break;
                case 1:
                    list1 = L.Where(s => s.Discount >= 0 && s.Discount < 0.05).ToList();
                    break;
                case 2:
                    list1 = L.Where(s => s.Discount >= 0.05 && s.Discount < 0.15).ToList();
                    break;
                case 3:
                    list1 = L.Where(s => s.Discount >= 0.15 && s.Discount < 0.30).ToList();
                    break;
                case 4:
                    list1 = L.Where(s => s.Discount >= 0.30 && s.Discount < 0.70).ToList();
                    break;
                case 5:
                    list1 = L.Where(s => s.Discount >= 0.70 && s.Discount < 1).ToList();
                    break;
            }
        }
        void Sort()
        {
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
            index = list1.Count;
            tbCountService.Text = index.ToString() + " из " + indexAll.ToString();
        }

        void SearchTitle(List<Service> L)
        {
            list1 = L.Where(x => x.Title.Contains(txtNameService.Text)||x.Description.Contains(txtNameService.Text)).ToList();
        }

        bool CheckZapis(int id)
        {
            ClientService lst = DataBase.connection.ClientService.FirstOrDefault(x => x.ServiceID == id);
            if (lst != null)
            {
                MessageBox.Show("Невозможно удалить запись!", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btnDelete = sender as Button;
            int id = (int)btnDelete.Tag;
            if (CheckZapis(id))
            {
                Service service = DataBase.connection.Service.FirstOrDefault(x => x.ID == id);
                DataBase.connection.Service.Remove(service);
                DataBase.connection.SaveChanges();
                list = DataBase.connection.Service.ToList();
                SearchTitle(list);
                Filtr(list1);
                Sort();
                MessageBox.Show("Запись успешно удалена!", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtNameService_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr(list);
            SearchTitle(list1);
            Sort();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateWindow window = new AddOrUpdateWindow();
            if (window.IsActive)
            {
                MessageBox.Show("ervdev");
            }
            else
            {
                window.Show();
            }
        }
    }
}
