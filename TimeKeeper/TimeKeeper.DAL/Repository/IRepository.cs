using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        List<T> Get(Func<T, bool> where);
        T Get(int id);

        void Insert(T entity);
        void Update(T entity, int id);
        void Delete(T entity);
    }
}
