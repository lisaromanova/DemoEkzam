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
            cbFiltr.SelectedIndex = 0;
        }

        private void cbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        void Filtr()
        {
            List<Service> list = DataBase.connection.Service.ToList();
            switch (cbFiltr.SelectedIndex)
            {
                case 1:
                    list = list.Where(s => s.Discount >= 0 && s.Discount < 0.05).ToList();
                    break;
                case 2:
                    list = list.Where(s => s.Discount >= 0.05 && s.Discount < 0.15).ToList();
                    break;
                case 3:
                    list = list.Where(s => s.Discount >= 0.15 && s.Discount < 0.30).ToList();
                    break;
                case 4:
                    list = list.Where(s => s.Discount >= 0.30 && s.Discount < 0.70).ToList();
                    break;
                case 5:
                    list = list.Where(s => s.Discount >= 0.70 && s.Discount < 1).ToList();
                    break;
            }
            if(cbSort.SelectedIndex != -1)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 0:
                        list = list.OrderBy(s => s.Costnew).ToList();
                        break;
                    case 1:
                        list = list.OrderByDescending(s => s.Costnew).ToList();
                        break;
                    
                }
            }
            if (string.IsNullOrWhiteSpace(txtNameService.Text))
            {
                list = list.Where(s => s.Title.Contains(txtNameService.Text)).ToList();
            }
            lstView.ItemsSource = list;
            tbCountService.Text = list.Count.ToString() + " из " + DataBase.connection.Service.ToList().Count.ToString();
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
                MessageBox.Show("Запись успешно удалена!", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameLoad.frmLoad.Navigate(new Vyvod());
            }
        }

        private void txtNameService_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = (int)btn.Tag;
            Service service = DataBase.connection.Service.FirstOrDefault(x => x.ID == index);
            AddOrUpdateWindow window = new AddOrUpdateWindow(service);
            window.ShowDialog();
            FrameLoad.frmLoad.Navigate(new Vyvod());
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateWindow window = new AddOrUpdateWindow();
            window.ShowDialog();
            FrameLoad.frmLoad.Navigate(new Vyvod());
        }

        private void btnServiceClient_Click(object sender, RoutedEventArgs e)
        {
            if(lstView.SelectedItem !=null)
            {
                Service service = (Service)lstView.SelectedItem;
                ClientRecord client = new ClientRecord(service);
                client.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной услуги!", "Запись клиента", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.ShowDialog();
        }
    }
}
