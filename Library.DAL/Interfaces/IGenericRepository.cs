using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T item);
        IEnumerable<T> Get();
        void Delete(T item);
        void Edit(T item);
        void SaveChanges();
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
    }
}