﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Demo.DataCenter.EF;
using Demo.Model;
using Demo.Service.Extend;
using Demo.Service.Interface;

namespace Demo.Service
{
    public class UserServ : ServiceBaseSimple<User>, IUserServ<User>
    {
        public string TableName { get { return "User"; } }

        public User GetOne(Guid id)
        {
            using (var context = GetContext())
            {
                return Data.First(one => one.ID == id);
            }
        }

        public bool Insert(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetPaged<TKey>(Func<User, bool> funcWhere, Func<User, TKey> funcOrderBy, int pageIndex, int pageSize, bool asc = true)
        {
            throw new NotImplementedException();
        }
    }
}
