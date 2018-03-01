using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;
using TimeKeeper.Utility;

namespace TimeKeeper.DAL
{
    public class TimeKeeperContext : DbContext
    {
        public TimeKeeperContext() : base("name=TimeKeeper")
        {
            if (Database.Connection.Database == "Testera")
            {
                Database.SetInitializer(new TimeKeeperDBInitializer<TimeKeeperContext>());
            }
        }

        public DbSet<Day> Calendar { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Engagement> Engagements { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Day>().Map<Day>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Customer>().Map<Customer>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Employee>().Map<Employee>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Engagement>().Map<Engagement>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Project>().Map<Project>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Role>().Map<Role>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Detail>().Map<Entities.Detail>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Team>().Map<Team>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Properties().Where(x => x.GetCustomAttributes(false).OfType<Precision>().Any())
                .Configure(c => c.HasPrecision(c.ClrPropertyInfo.GetCustomAttributes(false).OfType<Precision>().First()
                .precision, c.ClrPropertyInfo.GetCustomAttributes(false).OfType<Precision>().First().scale));
        }


        public override int SaveChanges()
        {
            EntitySetBase setBase;
            string tableName, primaryKeyName;
            try
            {
                foreach (var entry in ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted))
                {
                    setBase = GetEntitySet(entry.Entity.GetType());
                    tableName = (string)setBase.MetadataProperties["Table"].Value;
                    primaryKeyName = setBase.ElementType.KeyMembers[0].Name;
                    Database.ExecuteSqlCommand($"UPDATE {tableName} SET Deleted=1 " +
                        $"WHERE {primaryKeyName}='{entry.OriginalValues[primaryKeyName]}'");
                    entry.State = EntityState.Detached;
                }
                return base.SaveChanges();
            }
            catch { return 0; }
        }

        private EntitySetBase GetEntitySet(Type type)
        {
            ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;
            string typeName = ObjectContext.GetObjectType(type).Name;
            var es = octx.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)
                         .GetItems<EntityContainer>()
                         .SelectMany(c => c.BaseEntitySets.Where(e => e.Name == typeName))
                         .FirstOrDefault();
            if (es == null) throw new ArgumentException("Entity type not found in GetTableName", typeName);
            return es;
        }
    }
}
