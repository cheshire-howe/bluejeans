using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Data
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        T GetById(int id, params Expression<Func<T, object>>[] includeExpressions);
        T GetByIdRecursive(int id, params Expression<Func<T, object>>[] includeExpressions);
        void Add(T entity);
        void Update(T entity, params Expression<Func<T, object>>[] includeExpressions);
        void Delete(T entity);
        void Delete(int id);
    }
}
