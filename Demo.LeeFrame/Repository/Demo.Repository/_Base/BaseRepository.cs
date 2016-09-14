using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Demo.Common.DB;
using Demo.Common.DB.Operate;
using Demo.DataCenter.Dapper;

namespace Demo.Repository._Base
{

    /// <summary>
    /// 一些基础操作
    /// </summary>
    public class BaseRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">用于为sql参数赋值,以下一样</typeparam>
        /// <param name="insertEntity"></param>
        /// <returns></returns>
        public bool InsertBase<T>(InsertEntity<T> insertEntity) where T : class, new()
        { 
            using (DataFactory factory=new DataFactory())
            {
                string sql = SqlBuilder.BuildInsert(insertEntity.TableName, insertEntity.InsertFieldList);
                return factory.DapperConn.Execute(sql, insertEntity.TEntity) > 0;
            }
        }
         
        
        public bool UpdateBase<T>(UpdateEntity<T> updateEntity) where T : class, new()
        { 
            using (DataFactory factory = new DataFactory())
            {
                string sql = SqlBuilder.BuildUpdate(updateEntity.TableName, updateEntity.UpdateFieldList, updateEntity.WhereItems);
                return factory.DapperConn.Execute(sql, updateEntity.TEntity) > 0;
            } 
        }

        public bool DeleteBase<T>(DeleteEntity<T> deleteEntity) where T : class, new()
        {
            using (DataFactory factory = new DataFactory())
            {
                string sql = SqlBuilder.BuildDelete(deleteEntity.TableName, deleteEntity.WhereItems);
                return factory.DapperConn.Execute(sql, deleteEntity.TEntity) > 0;
            }
        }


        public T GetOneBase<T>(SelectEntity<T> selectEntity) where T : class, new()
        { 
            using (DataFactory factory = new DataFactory())
            {
                string sql = SqlBuilder.BuildSelect(selectEntity.TableName, selectEntity.SelectFieldList, selectEntity.WhereItems);
                return factory.DapperConn.QuerySingle<T>(sql, selectEntity.TEntity);
            } 
        }

        public bool ExistBase<T>(SelectEntity<T> selectEntity) where T : class, new()
        {
            return GetRecordCountBase(selectEntity) > 0;
        }

        public int GetRecordCountBase<T>(SelectEntity<T> selectEntity) where T : class, new()
        {
            using (DataFactory factory = new DataFactory())
            {
                string sqlWhere = SqlBuilder.BuildWhere(selectEntity.WhereItems);
                string sql =
                    $"select count(1) from {string.Format(SqlBuilder._wrapper, selectEntity.TableName)} where {sqlWhere}";
                int count = factory.DapperConn.QuerySingle<int>(sql, selectEntity.TEntity);
                return count;
            }
        }

        /// <summary>
        /// 分页
        /// </summary> 
        /// <param name="pagedEntity"></param>
        /// <returns></returns>
        public List<T> GetPagedBase<T>(PagedEntity<T> pagedEntity) where T : class, new()
        {
            using (DataFactory factory = new DataFactory())
            {
                string orderby = "";
                if (pagedEntity.OrderByItems.Count > 0)
                {
                    List<string> orderItemList = new List<string>();
                    pagedEntity.OrderByItems.ForEach(one =>
                    {
                        string orderByField = string.Format(SqlBuilder._wrapper, one.OrderByFiled);
                         string orderbyitem = one.Asc
                            ? $"{orderByField} asc"
                            : $"{orderByField} desc";
                        orderItemList.Add(orderbyitem);
                    });
                    orderby = string.Join(",", orderItemList);
                }
                else
                {
                    string orderByField = string.Format(SqlBuilder._wrapper, pagedEntity.OrderByItem.OrderByFiled);
                    orderby = pagedEntity.OrderByItem.Asc
                      ? $"{orderByField} asc"
                      : $"{orderByField} desc";
                }
              
                List<T> list = GetPagedBase(factory, pagedEntity.TableName, pagedEntity.PageIndex, pagedEntity.PageSize,
                    pagedEntity.SelectFieldList, pagedEntity.WhereItems, orderby, pagedEntity.TEntity);

                return list;
            }
        }
        
        private List<T> GetPagedBase<T>(DataFactory factory, string table, int pageIndex, int pageSize,
            List<string> selectFieldList,
            List<WhereItem> whereFieldList, string orderBy,
            T t) where T : class, new()
        {
            string sqlPage = "";
            if (factory.DbType == DBEnum.MSSQL)
            {
                const string sqlPageForamat = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

                var startPageIndex = (pageIndex - 1)*pageSize + 1;
                var endPageIndex = pageIndex*pageSize;

                string sql = SqlBuilder.BuildSelect(table, selectFieldList, whereFieldList);
                sqlPage = string.Format(sqlPageForamat, sql, orderBy, startPageIndex, endPageIndex);
            }
            else if (factory.DbType == DBEnum.MySQL)
            {
                string sql = SqlBuilder.BuildSelect(table, selectFieldList, whereFieldList);
                sql += $" order by {orderBy} limit {pageIndex - 1},{pageSize}";
                sqlPage = sql;
            }

            List<T> list = factory.DapperConn.Query<T>(sqlPage, t).ToList();
            return list;

        }

        /// <summary>
        /// 事务
        /// 批量操作
        /// insert/update/delete
        /// </summary>
        /// <param name="transcationBuilders"></param>
        public void ExecuteTransBase(List<TranscationBuilder> transcationBuilders)
        {
            using (DataFactory factory = new DataFactory())
            {
                IDbTransaction transaction = factory.DapperConn.BeginTransaction();
                try
                {
                    transcationBuilders.ForEach(one =>
                    {
                        factory.DapperConn.Execute(one.SqlWithParams, one.Params, transaction);
                    });
                    //commit
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    //出现异常，事务Rollback
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }


    }
}
