using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;
using TimeKeeper.DAL.Helper;
using TimeKeeper.DAL;

namespace TimeKeeper.ConApp
{
    class Program
    {
        static void Main(string[] args)
        {
            double time;
            //int count, result;
            using (TimeKeeperContext context = new TimeKeeperContext())
            {
                Console.WriteLine("Enter team name to search: ");
                string teamName = Console.ReadLine();
                //count = 0;
                //result = 0;
                DateTime srcStart = DateTime.Now;
                //var details = context.Details
                //    .Where(x => x.Hours == taskTime)
                //    .Select(x => new
                //    {
                //        DetailId = x.Id,
                //        ProjectName = x.Project.Name,
                //        Desc = x.Description
                //    });

                var teams = context.Teams
                    .Where(x => x.Name.Contains(teamName))
                    .SelectMany(x=>x.Engagements);
                foreach (var team in teams)
                {
                    //count++;
                    //result++;
                    Console.WriteLine($"\nTeam {team.Name} ");
                    var engs = team.Engagements;
                    foreach (var eng in engs)
                    {
                        Console.WriteLine($"{eng.Role.Id}: {eng.Employee.FirstName} | {eng.Hours}");
                    }
                }
                time = Math.Round((DateTime.Now - srcStart).TotalSeconds, 3);
            }
            Console.WriteLine($"\n-----------------------------");
            //Console.WriteLine($"\n{count} records retrieved.");
            //Console.WriteLine($"\n{result} records found.");
            Console.WriteLine($"\ntook {time} to get it done.");
            Console.Write($"\n--- press any key ---");
            Console.ReadKey();
        }
    }
}
