using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Model;
using Demo.Repository.Interface;

namespace Demo.Repository
{
    public class UserRepository : IRepository<User>
    {
        public bool Insert(User t)
        {
            throw new NotImplementedException();
        }

        public bool Update(User t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User t)
        {
            throw new NotImplementedException();
        }

        public User GetOne()
        {
            throw new NotImplementedException();
        }

        public List<User> GetPaged(int pageIndex, int pageSize, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}
