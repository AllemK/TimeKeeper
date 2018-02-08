using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;
using TimeKeeper.DAL.Helper;

namespace TimeKeeper.ConApp
{
    class Program
    {
        public class Rezultat
        {
            public DAL.Entities.Employee Emp { get; set; }
            public decimal TotalHours { get; set; }
            public decimal AverageHours { get; set; }
        }
        static void Main(string[] args)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                Console.WriteLine("Welcome to Dakota's demo show (simple console app)");
                //while (true)
                //{
                //    Console.WriteLine("Type quit to quit :)");
                //    Console.Write("Please enter employee first name: ");
                //    string firstName = Console.ReadLine();
                //    if (firstName == "quit")
                //    {
                //        break;
                //    }
                //    var employees = unit.Employees.Get(x => x.FirstName == firstName);

                //    if (employees == null)
                //    {
                //        Console.WriteLine("Employee not found!");
                //    }
                //    else
                //    {
                //        foreach (var e in employees)
                //        {
                //            Console.WriteLine($"Employee: {e.FirstName} {e.LastName}");
                //        }
                //    }

                //}
                var e = unit.Employees.Get(1);
                e.Email = "srle@srle.srle";
                e.FirstName = "Srle";
                e.LastName = "Srle";
                e.Phone = "060 0303 213";
                e.Salary = 25m;
                e.Status = EmployeeStatus.Active;
                unit.Employees.Update(e, 1);
                unit.Save();
                Console.WriteLine("Update first employee to be Srle! :*");
                e.WriteEmployee();
            }
            Console.Write("--- press any key ---");
            Console.ReadKey();
        }
    }
}
