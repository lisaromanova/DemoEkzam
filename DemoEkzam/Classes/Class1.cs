using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoEkzam.Classes;

namespace DemoEkzam
{
    public partial class Service
    {
        public string myString
        {
            get
            {
                decimal c = Math.Round(Cost, 0);
                string str = c.ToString();
                str += " рублей за ";
                int min = DurationInSeconds / 60;
                str += min.ToString();
                str += " минут";
                return str;

            }
        }
        public string AdminButtons
        {
            get
            {
                return Globals.AdminButton;
            }
            
        }

    }
}
