using DemoEkzam.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DemoEkzam
{
    /// <summary>
    /// Логика взаимодействия для ClientRecord.xaml
    /// </summary>
    public partial class ClientRecord : Window
    {
        Service service;
        public ClientRecord(Service service)
        {
            InitializeComponent();
            this.service = service;
            tbService.Text = service.Title;
            tbDuration.Text = (service.DurationInSeconds / 60).ToString();
            List<Client> clients = DataBase.connection.Client.ToList();
            cbClient.ItemsSource = clients;
            cbClient.SelectedValuePath = "ID";
            cbClient.DisplayMemberPath = "Fio";
        }

        bool Check()
        {
            if (cbClient.SelectedIndex != -1)
            {
                if (dtDateService.Text != "")
                {
                    if (Regex.IsMatch(tbTime.Text, @"^[0-2]\d:[0-5]\d$"))
                    {
                        try
                        {
                            DateTime dt = Convert.ToDateTime(tbTime.Text);
                            return true;
                        }
                        catch
                        {
                            MessageBox.Show("Введите время корректно!", "Запись клиента на услугу", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Введите время корректно!", "Запись клиента на услугу", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите дату оказания услуги!", "Запись клиента на услугу", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                
            }
            else
            {
                MessageBox.Show("Выберите клиента из списка!", "Запись клиента на услугу", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                DateTime dt = Convert.ToDateTime(dtDateService.SelectedDate);
                DateTime time = Convert.ToDateTime(tbTime.Text);
                dt = dt.AddHours(time.Hour);
                dt = dt.AddMinutes(time.Minute);
                ClientService client = new ClientService()
                {
                    ClientID = (int)cbClient.SelectedValue,
                    ServiceID = service.ID,
                    StartTime = dt
                };
                DataBase.connection.ClientService.Add(client);
                DataBase.connection.SaveChanges();
                MessageBox.Show("Клиент успешно записан на услугу", "Запись клиента на услугу", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void tbTimeEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tbTime.Text, @"^[0-2]\d:[0-5]\d$"))
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(tbTime.Text);
                    dt = dt.AddMinutes(service.DurationInSeconds / 60);
                    tbTimeEnd.Text = dt.ToString("HH:mm");
                }
                catch
                {
                    tbTimeEnd.Text = "Введите время корректно!";
                }

            }
            else
            {
                tbTimeEnd.Text = "Введите время корректно!";
            }
        }
    }
}
