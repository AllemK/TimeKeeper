using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.DAL
{
    internal class TimeKeeperDBInitializer<T> : DropCreateDatabaseAlways<TimeKeeperContext>
    {
        public override void InitializeDatabase(TimeKeeperContext context)
        {
            try
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    $"ALTER DATABASE {context.Database.Connection.Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            }
            catch
            {
                //database does not exist, excellent!
            }
            finally
            {
                base.InitializeDatabase(context);

                using (UnitOfWork unit = new UnitOfWork())
                {
                    addRoles(unit);
                    addTeams(unit);

                }
            }
        }

        void addRoles(UnitOfWork unit)
        {
            unit.Roles.Insert(new Role()
            {
                Id = "SD",
                Name = "Software Developer",
                Type = RoleType.TeamRole,
                HourlyRate = 30,
                MonthlyRate = 4500
            });
            unit.Roles.Insert(new Role()
            {
                Id = "UX",
                Name = "UI/UX Designer",
                Type = RoleType.TeamRole,
                HourlyRate = 45,
                MonthlyRate = 6500
            });
            unit.Save();
        }

        void addTeams(UnitOfWork unit)
        {
            unit.Teams.Insert(new Team()
            {
                Name = "Alpha",
                Id = "A",
                Image = "A",
                Description = "Alpha Team"
            });
            unit.Teams.Insert(new Team()
            {
                Name = "Bravo",
                Id = "B",
                Image = "B",
                Description = "Bravo Team"
            });
            unit.Save();
        }
    }
}

