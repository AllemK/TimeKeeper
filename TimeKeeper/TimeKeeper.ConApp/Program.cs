using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.ConApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                var employees = unit.Employees.Get().ToList();
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName}, {employee.LastName} is {employee.Status}");
                }
                unit.Employees.Delete(employees.ElementAt((employees.Count - 2)));
                unit.Save();
            }
            Console.ReadKey();
        }
    }
}
