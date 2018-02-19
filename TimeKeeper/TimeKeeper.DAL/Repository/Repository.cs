using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Repository
{
    public class Repository<T, I> : IRepository<T, I> where T : class
    {
        protected TimeKeeperContext timeKeeperContext;
        protected DbSet<T> dbSet;

        public Repository(TimeKeeperContext context)
        {
            timeKeeperContext = context;
            dbSet = context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            Utility.Log("REPOSITORY: all records retrieved", "INFO");
            return dbSet;
        }

        public List<T> Get(Func<T, bool> where)
        {
            Utility.Log("REPOSITORY: all records retrieved", "INFO");
            return dbSet.Where(where).ToList();
        }

        public T Get(I id)
        {
            Utility.Log($"REPOSITORY: record with id({id}) retrieved", "INFO");
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
            Utility.Log($"REPOSITORY: record inserted", "INFO");
        }

        public void Update(T entity, I id)
        {
            T old = Get(id);
            if (old != null)
            {
                timeKeeperContext.Entry(old).CurrentValues.SetValues(entity);
                Utility.Log("REPOSITORY: record updated", "INFO");
            }
            else
            {
                Utility.Log("REPOSITORY: record not found", "ERROR");
            }
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            Utility.Log("REPOSITORY: record deleted", "INFO");
        }
    }
}
