using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker
{
    class Dates
    {
        public string DateNow()
        {
            string now = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            return now;
        }
    }
}
