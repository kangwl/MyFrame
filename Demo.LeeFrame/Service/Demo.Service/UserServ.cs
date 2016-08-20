using System;
using System.Linq;
using Demo.DataCenter.EF;
using Demo.Model;
using Demo.Service.Extend;
using Demo.Service.Interface;

namespace Demo.Service
{
    public class UserServ : ServiceBase<User>, IUserServ
    {
        public User GetOne(Guid id)
        { 
            using (Context)
            {
                return Data.SingleOrDefault(one => one.ID == id);
            } 
        }
         

        public bool Insert(User user)
        { 
            using (Context)
            {
                Data.Add(user);
                int ret = Context.SaveChanges();
                return ret > 0;
            } 
        }
    }
}
