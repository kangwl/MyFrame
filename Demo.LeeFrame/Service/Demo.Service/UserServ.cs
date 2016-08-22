using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Demo.DataCenter.EF;
using Demo.Model;
using Demo.Service.Extend;
using Demo.Service.Interface;

namespace Demo.Service
{
    public class UserServ : ServiceBase<User>, IUserServ<User>
    {
        public User GetOne(Guid id)
        {
            using (GetContext())
            {
                return base.GetOne(one => one.ID == id);
            }
        } 

    }
}
