using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Common.DB;

namespace Demo.DataCenter.Dapper
{
    /// <summary>
    /// 事务组装器
    /// </summary>
    public class TranscationBuilder
    {
        public string SqlWithParams { get; set; }
        public object Params { get; set; }
    }

    public class SqlBuilder
    {
        #region ready
        public static DBEnum _dialect;

        public static string _wrapper;

        public static string _parameterChar;

        private static readonly string dbType = DataFactory.DbProviderStr;
        static SqlBuilder()
        {
            switch (dbType)
            {
                default:

                    _dialect = DBEnum.MSSQL;

                    _wrapper = "[{0}]";

                    _parameterChar = "@";

                    break;

                case "oracle":

                    _dialect = DBEnum.Oracle;

                    _wrapper = "{0}";

                    _parameterChar = ":";

                    break;

                case "postgre":

                    _dialect = DBEnum.Postgre;

                    _wrapper = "{0}";

                    _parameterChar = "@";

                    break;

                case "sqlite":

                    _dialect = DBEnum.SQLite;

                    _wrapper = "{0}";

                    _parameterChar = "@";

                    break;

                case "mysql":

                    _dialect = DBEnum.MySQL;

                    _wrapper = "`{0}`";

                    _parameterChar = "@";

                    break;
            }
        } 
        #endregion


        public static string BuildSelect(string table, List<string> selectFieldlList, List<WhereItem> whereFieldList)
        {
            string fields = " * ";
            if (selectFieldlList.Count > 0)
            {
                fields = string.Join(",", selectFieldlList);
            } 
            string whereStr = BuildWhere(whereFieldList);
            string sql = $"select {fields} from {string.Format(_wrapper, table)} where {whereStr}";

            return sql;
        }

        public static string BuildInsert(string table, List<string> fieldList)
        {
            string fileds = string.Join(",", fieldList.Select(one => string.Format(_wrapper, one)));
            string fieldsVal = string.Join(",", fieldList.Select(one => _parameterChar + one));
            string sql = $"insert into {string.Format(_wrapper, table)} ({fileds}) values({fieldsVal})";

            return sql;
        }

        public static string BuildUpdate(string table, List<string> updateFieldList, List<WhereItem> whereFieldList)
        {
            string setItemTemp = "{0}={1}{2}";
            List<string> setItemList = new List<string>();
            updateFieldList.ForEach(one =>
            {
                string setItem = string.Format(setItemTemp, string.Format(_wrapper, one), _parameterChar, one);
                setItemList.Add(setItem);
            });
            string setStr = string.Join(",", setItemList);
            //where 
            string whereStr = BuildWhere(whereFieldList);

            string sql = $"update {string.Format(_wrapper, table)} set {setStr} where {whereStr}"; 
            return sql;
        }

        public static string BuildDelete(string table, List<WhereItem> whereFieldList)
        {
            string whereStr = BuildWhere(whereFieldList);
            string sql = $"delete from {string.Format(_wrapper, table)} where {whereStr}";
            return sql;
        }

        public static string BuildWhere(List<WhereItem> whereFieldList)
        {
            if (whereFieldList.Count == 0) return " 1=1 ";
            string whereItemTemp = "{0} {1} {2}{3}";
            List<string> whereItemList = new List<string>();
            whereFieldList.ForEach(one =>
            {
                string whereItem = string.Format(whereItemTemp, string.Format(_wrapper, one.Field), one.Signal,
                    _parameterChar, one.Field);
                whereItemList.Add(whereItem);
            });
            string whereStr = string.Join(" and ", whereItemList);

            return whereStr;
        }
 
    }
}
