﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T item);
        T FindById(int? id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Delete(T item);
        void Edit(T item);
        void SaveChanges();
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}