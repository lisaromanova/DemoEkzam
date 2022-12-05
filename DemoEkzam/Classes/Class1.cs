using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using DemoEkzam.Classes;

namespace DemoEkzam
{
    public partial class Service
    {
        public string myString
        {
            get
            {
                string str = Costnew.ToString();
                str += " рублей за ";
                int min = DurationInSeconds / 60;
                str += min.ToString();
                str += " минут";
                return str;
            }
        }
        public decimal Costnew
        {
            get
            {
                decimal c = Math.Round(Cost, 0);
                c *= Convert.ToDecimal(1 - Discount);
                c = Math.Round(c, 0);
                return c;
            }
        }
        public Visibility AdminButtons
        {
            get
            {
                return Globals.AdminButton;
            }

        }
        public string PriceVisibility
        {
            get
            {
                if (Discount == 0)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string PriceDisc
        {
            get
            {
                return Math.Round(Cost, 0).ToString();
            }
        }
        public string Sk
        {
            get
            {
                return "* скидка " + (Discount * 100).ToString() + "%";
            }
        }
        public string ColorBack
        {
            get
            {
                if (Discount == 0)
                {
                    return "White";
                }
                else
                {
                    return "#FFE7FABF";
                }
            }
        }
        public BitmapImage ImageNew
        {
            get
            {
                return new BitmapImage(new Uri(Environment.CurrentDirectory + MainImagePath, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
