using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Utility;

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
            Logger.Log("REPOSITORY: all records retrieved", "INFO");
            return dbSet;
        }

        public List<T> Get(Func<T, bool> where)
        {
            Logger.Log("REPOSITORY: all records retrieved", "INFO");
            return dbSet.Where(where).ToList();
        }

        public T Get(I id)
        {
            Logger.Log($"REPOSITORY: record with id({id}) retrieved", "INFO");
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
            Logger.Log($"REPOSITORY: record inserted", "INFO");
        }

        public void Update(T entity, I id)
        {
            T old = Get(id);
            if (old != null)
            {
                timeKeeperContext.Entry(old).CurrentValues.SetValues(entity);
                Logger.Log("REPOSITORY: record updated", "INFO");
            }
            else
            {
                Logger.Log("REPOSITORY: record not found", "ERROR");
            }
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            Logger.Log("REPOSITORY: record deleted", "INFO");
        }
    }
}
