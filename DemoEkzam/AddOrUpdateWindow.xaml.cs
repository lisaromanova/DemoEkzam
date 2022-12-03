using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
using DemoEkzam.Classes;
using Microsoft.Win32;

namespace DemoEkzam
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateWindow.xaml
    /// </summary>
    public partial class AddOrUpdateWindow : Window
    {
        string s;
        Service service;
        string directory;

        public AddOrUpdateWindow()
        {
            InitializeComponent();
            gbId.Visibility = Visibility.Collapsed;
            s = "Добавление услуги";
            nameWindow.Title = s;
            btnAddUpdate.Content = "Добавить услугу";
            gbId.Visibility = Visibility.Collapsed;
            DirectoryPhoto();
        }
        public AddOrUpdateWindow(Service service)
        {
            InitializeComponent();
            this.service = service;
            gbId.Visibility = Visibility.Visible;
            s = "Редактирование услуги";
            nameWindow.Title = s;
            txtId.Text = service.ID.ToString();
            txtName.Text = service.Title;
            txtDescription.Text = service.Description;
            txtCost.Text = service.Cost.ToString();
            txtDuration.Text = (service.DurationInSeconds/60).ToString();
            txtDiscount.Text = service.Discount.ToString();
            imageService.Source = new BitmapImage(new Uri(service.MainImagePath, UriKind.Relative));
            btnAddUpdate.Content = "Изменить услугу";
            List<ServicePhoto> list = DataBase.connection.ServicePhoto.Where(x=> x.ServiceID==service.ID).ToList();
            lstPhotos.ItemsSource = list;
            DirectoryPhoto();
        }
        void DirectoryPhoto()
        {
            directory = Environment.CurrentDirectory;
            string[] arrayDirectiry = directory.Split('\\');
            directory = "";
            for (int i = 0; i < arrayDirectiry.Length - 2; i++)
            {
                directory += arrayDirectiry[i] + "\\";
            }
            directory += "Услуги школы";
        }
        private bool Check()
        {
            if(Regex.IsMatch(txtName.Text, "^[A-Я][а-я ]+$"))
            {
                if(Regex.IsMatch(txtCost.Text, @"^[0-9]*[.,]?[0-9]+$"))
                {
                    if (Regex.IsMatch(txtDuration.Text, @"^[0-9]+$"))
                    {
                        if(Convert.ToInt32(txtDuration.Text) <= 240)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Длительность услуги не может быть больше 4 часов!", s, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите длительность услуги корректно!", s, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите стоимость услуги корректно!", s, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите название услуги корректно!", s, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        private bool CheckService()
        {
            Service service;
            if (this.service != null)
            {
                service = DataBase.connection.Service.FirstOrDefault(x => x.Title == txtName.Text && x.ID != this.service.ID);
            }
            else
            {
                service = DataBase.connection.Service.FirstOrDefault(x => x.Title == txtName.Text);
            }
            if(service== null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Услуга с таким названием уже существует!", s, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        string path="";
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                if (CheckService())
                {
                    if (service == null)
                    {

                        service = new Service();
                        DataBase.connection.Service.Add(service);
                    }
                }
                service.Title = txtName.Text;
                service.Cost = Convert.ToDecimal(txtCost.Text);
                service.DurationInSeconds = Convert.ToInt32(txtDuration.Text)*60;
                service.Description = txtDescription.Text;
                if (txtDiscount.Text == "")
                {
                    service.Discount = 0;
                }
                else
                {
                    if (Regex.IsMatch(txtDiscount.Text, "^\\d+$"))
                    {
                        service.Discount = Convert.ToInt32(txtDiscount.Text)/100;
                    }
                    else
                    {
                        MessageBox.Show("Введите скидку корректно!", s, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if (path == "")
                {
                    service.MainImagePath = null;
                }
                else
                {
                    service.MainImagePath = path;
                }
                DataBase.connection.SaveChanges();
                MessageBox.Show("Услуга успешно добавлена!", "Добавление услуги", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void btnPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string directory = Environment.CurrentDirectory;
            directory += "\\Услуги школы";
            if ((bool)openFile.ShowDialog())
            {
                path = openFile.FileName;//нашли файл по пути
                if (!path.Contains(directory))//если в пути нет папки с файлами
                {
                    string NameFile = directory + '\\' + openFile.SafeFileName;//путь к папке + название фото
                    if (File.Exists(NameFile))
                    {
                        string[] fileName = openFile.SafeFileName.Split('.');
                        fileName[fileName.Length - 2] = fileName[fileName.Length - 2] + "(1)";
                        NameFile = directory + '\\';
                        foreach(string s in fileName)
                        {
                            NameFile += s+'.';
                        }
                        NameFile = NameFile.Substring(0, NameFile.Length - 1);
                    }
                    File.Copy(path, NameFile, false);//копируем файл
                    path = NameFile;//присваиваем путь
                }
                MessageBox.Show(path);
                string[] arrayPath = path.Split('\\');
                string pathApp = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];
                MessageBox.Show(path);
                imageService.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                service.MainImagePath = pathApp;
                DataBase.connection.SaveChanges();
                MessageBox.Show("Фото успешно изменено!", s, MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnAddPhotos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
