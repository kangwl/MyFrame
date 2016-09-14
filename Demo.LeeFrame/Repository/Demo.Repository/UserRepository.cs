using System;
using System.Collections.Generic;
using Demo.Common.DB;
using Demo.Common.DB.Operate;
using Demo.DataCenter.Dapper;
using Demo.DataCenter.Dapper.Ext;
using Demo.Model;
using Demo.Repository.IRepository;
using Demo.Repository._Base;

namespace Demo.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public string TableName
        {
            get { return "User"; }
        }

        public bool Insert(User user)
        {
            InsertEntity<User> insertEntity = new InsertEntity<User>(TableName, user, new List<string>()
            {
                nameof(user.ID),
                nameof(user.Age),
                nameof(user.Name),
                nameof(user.QQ),
                nameof(user.CreateTime),
                nameof(user.UpdateTime)
            });
            return base.InsertBase(insertEntity);
        }

        public bool Delete(Guid id)
        {
            User user = new User() {ID = id};
            List<WhereItem> whereItems = new List<WhereItem>() {new WhereItem(nameof(user.ID), "=")}; 
            return Delete(whereItems,user);
        }
        public bool Delete(List<WhereItem> whereItems, User user)
        {
            DeleteEntity<User> deleteEntity = new DeleteEntity<User>(TableName, whereItems, user);
            return base.DeleteBase(deleteEntity);
        }

        public bool Update(List<string> updateFieldList, List<WhereItem> whereItems, User user)
        {
            UpdateEntity<User> updateEntity = new UpdateEntity<User>(TableName, updateFieldList, whereItems, user);

            return base.UpdateBase(updateEntity);
        }

        public User GetOne(Guid ID)
        {
            User user = new User() {ID = ID};
            List<WhereItem> whereItems = new List<WhereItem>()
            {
                new WhereItem() {Field = nameof(user.ID), Signal = "="}
            };
            SelectEntity<User> selectEntity = new SelectEntity<User>(TableName, new List<string>(), whereItems, user);

            return base.GetOneBase(selectEntity);
        }

        public bool Exist(List<WhereItem> whereItems, User user)
        {
            SelectEntity<User> selectEntity = new SelectEntity<User>(TableName, new List<string>(), whereItems, user);
            return base.ExistBase(selectEntity);
        }

        public int GetRecordCount(List<WhereItem> whereItems, User user)
        {
            SelectEntity<User> selectEntity = new SelectEntity<User>(TableName, new List<string>(), whereItems, user);
            return base.GetRecordCountBase(selectEntity);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selectFieldList">需要获取的字段 （全部字段 * 时,其Count属性为0即可 ）</param>
        /// <param name="orderByItem">order by item</param>
        /// <param name="whereItems">where item</param>
        /// <param name="user">用于取值</param>
        /// <returns></returns>
        public List<User> GetPaged(int pageIndex, int pageSize, List<string> selectFieldList, OrderByItem orderByItem,
            List<WhereItem> whereItems, User user)
        {
            PagedEntity<User> pagedEntity = new PagedEntity<User>(TableName, orderByItem, pageIndex, pageSize)
            {
                SelectFieldList = selectFieldList,
                TEntity = user,
                WhereItems = whereItems
            };

            return base.GetPagedBase(pagedEntity);
        }

        public void OtherLogic()
        {
            using (DataFactory factory = new DataFactory())
            {
              
                //factory.DapperConn.Query()
            }
        }

    }
}
