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
                // database does not exsists , great :)
            }
            finally
            {
                base.InitializeDatabase(context);

                using (UnitOfWork unit = new UnitOfWork())
                {
                    //addRoles(unit);
                    //addTeams(unit);

                    AddCustomer(unit);
                }
            }

            
        }

        private void addTeams(UnitOfWork unit)
        {
            throw new NotImplementedException();
        }

        private void addRoles(UnitOfWork unit)
        {
            //unit.Roles.Insert(new Entities.Role()
            //{
            //    Id = "SD",
            //    Name = "Software Developer",
            //    Type = RoleType.TeamRole,
            //    HourlyRate = 30,
            //    MonthlyRate = 4500
            //});
        }


        private void AddCustomer(UnitOfWork unit)
        {
            unit.Customers.Insert(new Customer()
            {


            });
        }
    }
}

//Database.SetInitializer<TimeContext>(new CreateDatabaseIfNotExists<TimeContext>());
//Database.SetInitializer<TimeContext>(new DropCreateDatabaseIfModelChanges<TimeContext>());
//base.Database.ExecuteSqlCommand("USE master; ALTER DATABASE Testera SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
//Database.SetInitializer<TimeContext>(new DropCreateDatabaseAlways<TimeContext>());
