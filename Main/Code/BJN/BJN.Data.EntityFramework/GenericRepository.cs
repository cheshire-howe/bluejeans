using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Data.EntityFramework
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeExpression in includeExpressions)
                query = query.Include(includeExpression);
            return query;
        }

        public virtual T GetById(int id, params Expression<Func<T, object>>[] includeExpressions)
        {
            var entitySet = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<T>().EntitySet;
            string keyName = entitySet.ElementType.KeyMembers.Select(k => k.Name).Single();

            IQueryable<T> query = DbSet;
            query = includeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            return query.Where(keyName + "= @0", id).FirstOrDefault();
        }

        public T GetByIdRecursive(int id, params Expression<Func<T, object>>[] includeExpressions)
        {
            var entitySet = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<T>().EntitySet;
            string keyName = entitySet.ElementType.KeyMembers.Select(k => k.Name).Single();

            IQueryable<T> query = DbSet;
            query = includeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            T first = null;
            // Force the loading of all entities in the graph to fully populate any recursive relationships
            foreach (var o in query)
            {
                if (first == null)
                    first = o;
                // don't need to do anything else here, because we're just trying 
                // to enumerate everything so it gets pulled into the context
            }

            return query.Where(keyName + "= @0", id).FirstOrDefault();
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity, params Expression<Func<T, object>>[] includeExpressions)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                var newValues = new Dictionary<Expression<Func<T, object>>, object>();
                var propNames = new Dictionary<Expression<Func<T, object>>, string>();

                // Update related entities
                foreach (var includeExpression in includeExpressions)
                {
                    newValues.Add(includeExpression, includeExpression.Compile()(entity));
                    propNames.Add(includeExpression, ((includeExpression.Body as MemberExpression).Member).Name);
                    entity.GetType().GetProperty(propNames[includeExpression]).SetValue(entity, null);
                }
                DbSet.Attach(entity);

                foreach (var includeExpression in includeExpressions)
                {
                    if (entity.GetType().GetProperty(propNames[includeExpression]).PropertyType.GetInterface("IEnumerable") != null)
                        DbContext.Entry(entity).Collection(propNames[includeExpression]).Load();
                    else
                        DbContext.Entry(entity).Reference(propNames[includeExpression]).Load();
                    entity.GetType().GetProperty(propNames[includeExpression]).SetValue(entity, newValues[includeExpression]);
                }
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }
    }
}
