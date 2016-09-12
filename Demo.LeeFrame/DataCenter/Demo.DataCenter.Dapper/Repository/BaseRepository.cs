using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Demo.DataCenter.Dapper.Repository
{
    /// <summary>
    /// 事务组装器
    /// </summary>
    public class TranscationBuilder
    {
        public string SqlWithParams { get; set; }
        public object Params { get; set; }
    }

    /// <summary>
    /// 一些基础操作
    /// </summary>
    /// <typeparam name="T">用于为sql参数赋值</typeparam>
    public class BaseRepository<T> where T:class 
    {
        public bool Insert(string table, List<string> insertFieldList, T t)
        { 
            using (DataFactory factory=new DataFactory())
            {
                string sql = SqlBuilder.BuildInsert(table, insertFieldList);
                return factory.DapperConn.Execute(sql, t) > 0;
            }
        }

        /// <summary>
        /// 事务
        /// 批量操作
        /// </summary>
        /// <param name="transcationBuilders"></param>
        public void ExecuteTrans(List<TranscationBuilder> transcationBuilders)
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

        
        public bool Update(string table, List<string> updateFieldList, List<SqlBuilder.WhereItem> whereFieldList, T t)
        { 
            using (DataFactory factory = new DataFactory())
            {
                string sql = SqlBuilder.BuildUpdate(table, updateFieldList, whereFieldList);
                return factory.DapperConn.Execute(sql, t) > 0;
            } 
        }

        public bool Delete(string table, List<SqlBuilder.WhereItem> whereFieldList, T t)
        { 
            using (DataFactory factory = new DataFactory())
            {
                string sql = SqlBuilder.BuildDelete(table, whereFieldList);
                return factory.DapperConn.Execute(sql, t) > 0;
            } 
        }


        public T GetOne(string table, List<string> selectFieldList, List<SqlBuilder.WhereItem> whereFieldList, T t)
        { 
            using (DataFactory factory = new DataFactory())
            {
                string sql = SqlBuilder.BuildSelect(table, selectFieldList, whereFieldList);
                return factory.DapperConn.QuerySingle<T>(sql, t);
            } 
        }

        public bool Exist(string table, List<SqlBuilder.WhereItem> whereFieldList, T t)
        {
            using (DataFactory factory = new DataFactory())
            {
                string sqlWhere = SqlBuilder.BuildWhere(whereFieldList);
                string sql = $"select count(1) from {table} where {sqlWhere}";
                int count = factory.DapperConn.QuerySingle<int>(sql, t);
                return count > 0;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="table"></param>
        /// <param name="pageIndex">从1开始</param>
        /// <param name="pageSize"></param>
        /// <param name="selectFieldList"></param>
        /// <param name="whereFieldList"></param>
        /// <param name="orderBy"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetPaged(string table, int pageIndex, int pageSize, List<string> selectFieldList,
            List<SqlBuilder.WhereItem> whereFieldList, string orderBy,
            T t)
        {
            using (DataFactory factory = new DataFactory())
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
        }




    }
}
