using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Demo.DataCenter.Dapper.Repository.IRepository;
using Demo.Model;

namespace Demo.DataCenter.Dapper.Repository
{
    public class UserRepository :BaseRepository<User>, IUserRepository
    {
        public string TableName
        {
            get { return "User"; }
        }

        public bool Insert(User t)
        {

            List<string> insertFieldList = new List<string>()
            {
                nameof(t.ID),
                nameof(t.Name),
                nameof(t.Age),
                nameof(t.QQ), 
                nameof(t.CreateTime)
            };
            //string sql = SqlBuilder.BuildInsert(TableName, insertFieldList); 
            bool ok = base.Insert(TableName, insertFieldList, t);
            return ok;
        }

        public bool Delete(User t)
        {
            List<SqlBuilder.WhereItem> whereItems = new List<SqlBuilder.WhereItem>();

            //whereItems.Add(new SqlBuilder.WhereItem()
            //{
            //    Field = nameof(t.ID),
            //    Sign = " = "
            //});
            //whereItems.Add(new SqlBuilder.WhereItem()
            //{
            //    Field = nameof(t.Age),
            //    Sign = " >= "
            //});
            whereItems.Add(new SqlBuilder.WhereItem()
            {
                Field = nameof(t.Name),
                Signal = " like "
            });

            t.Name = "%e%";
            return base.Delete(TableName, whereItems, t);
        }

        public User GetOne(Guid ID, User t)
        {
            List<SqlBuilder.WhereItem> whereItems = new List<SqlBuilder.WhereItem>();
            whereItems.Add(new SqlBuilder.WhereItem() {Field = nameof(t.ID), Signal = "="});
            User user = base.GetOne(TableName, new List<string>(), whereItems, t);
            return user;
        }

    }

}
