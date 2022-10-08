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

namespace DemoEkzam
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateWindow.xaml
    /// </summary>
    public partial class AddOrUpdateWindow : Window
    {
        public AddOrUpdateWindow()
        {
            InitializeComponent();
            gbId.Visibility = Visibility.Collapsed;
            nameWindow.Title = "Добавление услуги";
        }
        public AddOrUpdateWindow(Service service)
        {
            InitializeComponent();
            gbId.Visibility = Visibility.Visible;
            nameWindow.Title = "Редактирование услуги";
        }
    }
}
