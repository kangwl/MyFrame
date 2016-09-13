using System;
using System.Collections.Generic;
using Demo.Common.DB;
using Demo.Common.DB.Operate;
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
            InsertEntity<User> insertEntity = new InsertEntity<User>();
            insertEntity.TableName = TableName;
            insertEntity.InsertFieldList = insertFieldList;
            insertEntity.TEntity = t;
            //string sql = SqlBuilder.BuildInsert(TableName, insertFieldList); 
            bool ok = base.InsertBase(insertEntity);
            return ok;
        }

        public bool Delete(DeleteEntity<User> deleteEntity)
        {

            //whereItems.Add(new WhereItem()
            //{
            //    Field = nameof(t.ID),
            //    Sign = " = "
            //});
            //whereItems.Add(new WhereItem()
            //{
            //    Field = nameof(t.Age),
            //    Sign = " >= "
            //}); 
            return base.DeleteBase(deleteEntity);
        }

        public User GetOne(List<WhereItem> whereItems, User t)
        { 
            //whereItems.Add(new WhereItem() {Field = nameof(t.ID), Signal = "="});
            SelectEntity<User> selectEntity = new SelectEntity<User>
            {
                TableName = TableName,
                SelectFieldList = new List<string>(),
                TEntity = t,
                WhereItems = whereItems
            };
            User user = base.GetOneBase(selectEntity);
            return user;
        }

        public List<User> Paged(int pageIndex, int pageSize, List<WhereItem> whereItems, OrderByItem orderByItem, User t)
        {
            PagedEntity<User> pagedEntity = new PagedEntity<User>
            {
                TableName = TableName,
                OrderByItem = orderByItem,
                PageIndex = pageIndex,
                PageSize = pageSize,
                SelectFieldList = new List<string>(),
                TEntity = t,
                WhereItems = whereItems
            };

            List<User> users = base.GetPagedBase(pagedEntity);

            return users;
        }

        public void InsertBath(List<User> users)
        {
            User userStruct;
            List<TranscationBuilder> transcationBuilders = new List<TranscationBuilder>(); 
            string sqlWithParams = SqlBuilder.BuildInsert(TableName,
                new List<string>() {nameof(userStruct.ID), nameof(userStruct.Name), nameof(userStruct.Age), nameof(userStruct.CreateTime)}); 
            users.ForEach(user =>
            {
                transcationBuilders.Add(new TranscationBuilder() { SqlWithParams = sqlWithParams, Params = user });
            }); 
            base.ExecuteTransBase(transcationBuilders);
        }

    }

}
