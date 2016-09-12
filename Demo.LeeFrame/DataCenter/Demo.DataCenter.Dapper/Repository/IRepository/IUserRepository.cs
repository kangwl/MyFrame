using System;
using Demo.Model;

namespace Demo.DataCenter.Dapper.Repository.IRepository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        bool Delete(User t);
        User GetOne(Guid ID, User t);
    }
}