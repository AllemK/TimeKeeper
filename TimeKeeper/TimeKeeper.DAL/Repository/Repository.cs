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
            return dbSet;
        }

        public List<T> Get(Func<T, bool> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(I id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity, I id)
        {
            T old = Get(id);
            if (old != null)
            {
                timeKeeperContext.Entry(old).CurrentValues.SetValues(entity);
            }
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
