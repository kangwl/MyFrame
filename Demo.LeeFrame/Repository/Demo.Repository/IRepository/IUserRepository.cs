using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Common.DB;
using Demo.Model;
using Demo.Repository._Base;

namespace Demo.Repository.IRepository
{
    public interface IUserRepository : IRepositoryBase
    {
        bool Insert(User user);
        bool Delete(List<WhereItem> whereItems, User user);
        bool Update(List<string> updateFieldList, List<WhereItem> whereItems, User user);
        User GetOne(Guid ID, User user);
        bool Exist(List<WhereItem> whereItems, User user);
        int GetRecordCount(List<WhereItem> whereItems, User user);

        List<User> GetPaged(int pageIndex, int pageSize, List<string> selectFieldList, OrderByItem orderByItem,
            List<WhereItem> whereItems, User user);
    }
}
