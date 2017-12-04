using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T item);
        T FindById(int? id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Delete(T item);
        void Edit(T item);
    }
}