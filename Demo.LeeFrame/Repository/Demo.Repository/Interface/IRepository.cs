using System.Collections.Generic;

namespace Demo.Repository.Interface
{
    public interface IRepository<T> where T:class
    {
        bool Insert(T t);
        bool Update(T t);
        bool Delete(T t);
        T GetOne();
        List<T> GetPaged(int pageIndex, int pageSize, string orderBy);
    }
}