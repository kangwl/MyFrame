﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataCenter.Dapper;
using Demo.Model;
using Demo.Service.Interface;

namespace Demo.Service.Dapper
{
    public class UserServ : IUserServ<User>
    {
        public string TableName
        {
            get { return "User"; }
        }

        public User GetOne(Guid id)
        {
            return new User();
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

        public List<User> GetPaged<TKey>(Func<User, bool> funcWhere, Func<User, TKey> funcOrderBy, int pageIndex,
            int pageSize, bool asc = true)
        {
            throw new NotImplementedException();
        }
    }

}
