using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DemoEkzam
{
    public partial class ClientService
    {
        public string Time
        {
            get
            {
                int minutes = (StartTime.Hour*60+StartTime.Minute)-(DateTime.Now.Hour*60+DateTime.Now.Minute);
                int hours = minutes / 60;
                minutes = minutes % 60;
                return hours.ToString()+" часа "+minutes.ToString()+" минут";
            }
        }

        public SolidColorBrush ColorTime
        {
            get
            {
                string[] time = Time.Split(' ');
                int i = Convert.ToInt32(time[0]);
                if (i == 0)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.Black;
                }
            }
        }
    }
}
