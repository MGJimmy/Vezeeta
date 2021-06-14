using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using DAL;

namespace BL.Bases
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        #region Properties
        protected DbContext Context { get; set; }

        protected DbSet<T> DbSet { get; set; }
        #endregion

        #region Constructor
        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            Context = dbContext;
            DbSet = Context.Set<T>();
        }
        #endregion

        #region Get All Data Methods
        public virtual ICollection<T> GetAll() 
        {
            return DbSet.ToList();
        }

        public virtual ICollection<T> GetAllSorted<TKey>(Expression<Func<T, TKey>> sortingExpression)
        {
            return DbSet.OrderBy<T, TKey>(sortingExpression).ToList();
        }

        public virtual ICollection<T> GetWhere(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.ToList();
        }

        public virtual bool GetAny(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;
            bool result = false;
            if (filter != null)
            {
                result = query.Any(filter);
            }
            return result;
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return DbSet.FirstOrDefault(filter);
            }
            return null;
        }
        #endregion

        #region Get one record
        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual T GetById(long id)
        {
            return DbSet.Find(id);
        }
        #endregion

        #region CRUD Methods
        public virtual T Insert(T entity)
        {
            EntityEntry<T> dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
               DbSet.Add(entity);
            }
            return entity;
        }

        public virtual void InsertList(List<T> entityList)
        {
            DbSet.AddRange(entityList);
        }

        public virtual void Update(T entity)
        {
            EntityEntry<T> dbEntityEntry = Context.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }
        
        public virtual void UpdateList(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                Update(item);
            }
        }

        public virtual void Delete(T entity)
        {
            EntityEntry<T> dbEntityEntry = Context.Entry(entity);
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
        public virtual void DeleteList(IEnumerable<T> entityList)
        {
            foreach (T item in entityList)
            {
                Delete(item);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }
        #endregion

        #region pagination
        public virtual int CountEntity()
        {
            return DbSet.Count();
        }
        public virtual IEnumerable<T> GetPageRecords(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
        #endregion

    }
}
