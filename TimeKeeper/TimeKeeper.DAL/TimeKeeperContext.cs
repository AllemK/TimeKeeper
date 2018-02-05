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

namespace TimeKeeper.DAL
{
    public class TimeKeeperContext : DbContext
    {
        public TimeKeeperContext() : base("TimeKeeper")
        {

        }

        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Entities.Task> Task { get; set; }
        public DbSet<Team> Team { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Calendar>().Map<Calendar>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Customer>().Map<Customer>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Employee>().Map<Employee>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Member>().Map<Member>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Project>().Map<Project>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Role>().Map<Role>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Entities.Task>().Map<Entities.Task>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Team>().Map<Team>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
        }


        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted)) SoftDelete(entry);
            return base.SaveChanges();
        }

        private void SoftDelete(DbEntityEntry entry)
        {
            Type entryEntityType = entry.Entity.GetType();
            string tableName = GetTableName(entryEntityType);
            string sql = string.Format("UPDATE {0} SET Deleted = 1 WHERE id = @id", tableName);
            Database.ExecuteSqlCommand(sql, new SqlParameter("@id", entry.OriginalValues["Id"]));
            entry.State = EntityState.Unchanged;
        }

        private static Dictionary<Type, EntitySetBase> _mappingCache = new Dictionary<Type, EntitySetBase>();

        private string GetTableName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);
            return string.Format("[{0}].[{1}]", es.MetadataProperties["Schema"].Value, es.MetadataProperties["Table"].Value);
        }

        private EntitySetBase GetEntitySet(Type type)
        {
            if (!_mappingCache.ContainsKey(type))
            {
                ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;
                string typeName = ObjectContext.GetObjectType(type).Name;
                var es = octx.MetadataWorkspace.GetItemCollection(DataSpace.SSpace).GetItems<EntityContainer>().SelectMany(c => c.BaseEntitySets.Where(e => e.Name == typeName)).FirstOrDefault();
                if (es == null) throw new ArgumentException("Entity type not found in GetTableName", typeName);
                _mappingCache.Add(type, es);
            }
            return _mappingCache[type];
        }
    }
}
