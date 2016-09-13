using System;
using System.Collections.Generic;
using Demo.Common.DB;
using Demo.Common.DB.Operate;
using Demo.Model;

namespace Demo.DataCenter.Dapper.Repository.IRepository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        bool Delete(DeleteEntity<User> deleteEntity);
        User GetOne(List<WhereItem> whereItems, User t);
        List<User> Paged(int pageIndex, int pageSize, List<WhereItem> whereItems, OrderByItem orderByItem,User t);
        void InsertBath(List<User> users);
    }
}