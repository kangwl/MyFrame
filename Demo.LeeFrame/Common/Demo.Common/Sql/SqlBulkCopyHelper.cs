using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Demo.Common.Sql {
    public class SqlBulkCopyHelper
    { 
        public SqlBulkCopyHelper(SqlConnection connection, string toTableName, Dictionary<string, string> dicMap)
        {
            Create(connection.ConnectionString, toTableName, dicMap);
        }

        public SqlBulkCopyHelper(string connStr, string toTableName, Dictionary<string, string> dicMap)
        {
            Create(connStr, toTableName, dicMap);
        }

        /// <summary>
        /// SqlBulkCopy 对象
        /// </summary>
        public SqlBulkCopy SqlBulkCopyObj { get; set; }

        /// <summary>
        /// 创建 SqlBulkCopy 对象
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="toTableName"></param>
        /// <param name="dicMap"></param>
        private void Create(string connStr, string toTableName, Dictionary<string, string> dicMap)
        {
            SqlBulkCopyObj = new SqlBulkCopy(connStr);
            SqlBulkCopyObj.DestinationTableName = toTableName;
            var columnMappings = GetColumnMappings(dicMap);
            foreach (SqlBulkCopyColumnMapping mapping in columnMappings)
            {
                SqlBulkCopyObj.ColumnMappings.Add(mapping);
            }
            SqlBulkCopyObj.NotifyAfter = NotifyCount;
        }

        /// <summary>
        /// 解析映射关系
        /// </summary>
        /// <param name="dicMap"></param>
        /// <returns></returns>
        private IEnumerable<SqlBulkCopyColumnMapping> GetColumnMappings(Dictionary<string, string> dicMap)
        {
            List<SqlBulkCopyColumnMapping> columnMappings = new List<SqlBulkCopyColumnMapping>();
            foreach (KeyValuePair<string, string> pair in dicMap)
            {
                columnMappings.Add(new SqlBulkCopyColumnMapping(pair.Key, pair.Value));
            }
            return columnMappings;
        }

        /// <summary>
        /// 配合 SqlRowsCopied 使用
        /// 处理多少条数据后给予通知
        /// </summary>
        public int NotifyCount { get; set; }

        /// <summary>
        /// 开始写入数据库 
        /// </summary>
        /// <param name="srcReader">IDataReader</param>
        public void StartWrite(IDataReader srcReader)
        {
            using (SqlBulkCopyObj)
            {
                StartWrite2Server(srcReader);
            }
        }

        /// <summary>
        /// 回调
        /// </summary>
        public Action<SqlRowsCopiedEventArgs> SqlRowsCopied { get; set; }



        private void SqlBulkCopyObj_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            SqlRowsCopied(e);
        }

        private void StartWrite2Server(dynamic data)
        {
            if (NotifyCount > 0)
            {
                SqlBulkCopyObj.SqlRowsCopied += SqlBulkCopyObj_SqlRowsCopied;
            }
            SqlBulkCopyObj.WriteToServer(data);
        }

        public void StartWrite(List<DataTable> srcDataTables)
        {
            using (SqlBulkCopyObj)
            {
                foreach (DataTable table in srcDataTables)
                {
                    StartWrite2Server(table);
                }
            }
        }

        /// <summary>
        /// 开始写入数据库
        /// </summary>
        /// <param name="srcDataTable">DataTable</param>
        public void StartWrite(DataTable srcDataTable)
        {
            using (SqlBulkCopyObj)
            {
                StartWrite2Server(srcDataTable);
            }
        }

        /// <summary>
        /// 开始写入数据库
        /// </summary>
        /// <param name="srcDataRows">DataRow[]</param>
        public void StartWrite(DataRow[] srcDataRows)
        {
            using (SqlBulkCopyObj)
            {
                StartWrite2Server(srcDataRows);
            }
        }


    }
}
