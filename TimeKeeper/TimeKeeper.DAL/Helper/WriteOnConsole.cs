using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.DAL.Helper
{
    public static class WriteOnConsole
    {
        public static void WriteEmployee(this Employee e)
        {
            Console.WriteLine($"{e.FirstName} {e.LastName}, {e.Status}");
        }
    }
}
