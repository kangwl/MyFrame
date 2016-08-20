using System;
using Demo.Model;

namespace Demo.Service.Interface
{
    public interface IUserServ
    {
        User GetOne(Guid id);
        bool Insert(User user);
    }
}