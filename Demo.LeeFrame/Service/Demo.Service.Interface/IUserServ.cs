using System;
using System.Collections.Generic;
using Demo.Model;

namespace Demo.Service.Interface
{
    public interface IUserServ<TModel>
    {
        User GetOne(Guid id);
        bool Insert(User user);
        bool Delete(User user);
        bool Update(User user);

        List<TModel> GetPaged<TKey>(Func<TModel, bool> funcWhere, Func<TModel, TKey> funcOrderBy, int pageIndex,
            int pageSize, bool asc = true);
    }
}