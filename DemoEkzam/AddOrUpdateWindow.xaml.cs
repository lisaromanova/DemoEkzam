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
using DemoEkzam.Classes;

namespace DemoEkzam
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateWindow.xaml
    /// </summary>
    public partial class AddOrUpdateWindow : Window
    {
        Service service;
        public AddOrUpdateWindow()
        {
            InitializeComponent();
            gbId.Visibility = Visibility.Collapsed;
            nameWindow.Title = "Добавление услуги";
            tbHeader.Text = "Добавление услуги";
            btnAddUpdate.Content = "Добавить услугу";
            gbId.Visibility = Visibility.Collapsed;
        }
        public AddOrUpdateWindow(Service service)
        {
            InitializeComponent();
            this.service = service;
            gbId.Visibility = Visibility.Visible;
            nameWindow.Title = "Редактирование услуги";
            tbHeader.Text = "Редактирование услуги";
            txtId.Text = service.ID.ToString();
            txtName.Text = service.Title;
            txtDescription.Text = service.Description;
            txtCost.Text = service.Cost.ToString();
            txtDuration.Text = service.DurationInSeconds.ToString();
            txtDiscount.Text = service.Discount.ToString();
            imageService.Source = new BitmapImage(new Uri(service.MainImagePath, UriKind.Relative));
            btnAddUpdate.Content = "Изменить услугу";
        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (service == null)
            {
                service = new Service();
                DataBase.connection.Service.Add(service);
            }
            service.Title = txtName.Text;
            service.Cost = Convert.ToDecimal(txtCost.Text);
            service.DurationInSeconds = Convert.ToInt32(txtDuration.Text);
            service.Description = txtDescription.Text;
            service.Discount = Convert.ToDouble(txtDiscount.Text);
            DataBase.connection.SaveChanges();
            MessageBox.Show("Услуга успешно добавлена!", "Добавление услуги", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
