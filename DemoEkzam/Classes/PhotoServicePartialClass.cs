using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DemoEkzam
{
    public partial class ServicePhoto
    {
        public BitmapImage NewPhoto
        {
            get
            {
                return new BitmapImage(new Uri(Environment.CurrentDirectory + PhotoPath, UriKind.Absolute));
            }
        }
    }
}
