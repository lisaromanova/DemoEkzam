using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEkzam
{
    public partial class Client
    {
        public string Fio
        {
            get
            {
                return FirstName + LastName + Patronymic;
            }
        }
    }
}
