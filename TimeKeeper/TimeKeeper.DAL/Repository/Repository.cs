using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected TimeKeeperContext context;
        protected DbSet<T> dbSet;

        public Repository(TimeKeeperContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return dbSet;
        }

        public void Delete (T entity)
        {
            dbSet.Remove(entity);
        }

        public List<T> Get(Func<T, bool> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get (int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity, int id)
        {
            T old = Get(id);
            if (old != null)
            {
                context.Entry(old).CurrentValues.SetValues(entity);
            }
        }
    }
}
