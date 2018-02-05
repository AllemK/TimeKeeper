using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Repository
{
    public interface IRepository<T, I>
    {
        IQueryable<T> Get();
        List<T> Get(Func<T, bool> where);
        T Get(I id);
        void Insert(T entity);
        void Update(T entity, I id);
        void Delete(T entity);
    }
}
