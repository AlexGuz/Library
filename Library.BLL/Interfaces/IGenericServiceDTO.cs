using System.Collections.Generic;

namespace Library.BLL.Interfaces
{
    public interface IGenericServiceDTO<T> where T : class
    {
        void Create(T item);
        void Delete(T item);
        void Edit(T item);
        IEnumerable<T> Get();
        T GetWithInclude(int? item);
        void SaveToFile(string connectionString);
    }
}