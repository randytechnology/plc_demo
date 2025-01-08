using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plc_demo.Utils
{
    /// <summary>
    /// SqliteHelper 帮助类
    /// </summary>
    public static class SqliteHelper
    {
        private static string _connstring = string.Empty;

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connstring"></param>
        public static void SetConnectionString(string connstring) { 
            _connstring = connstring;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return _connstring;
        }

        /// <summary>
        /// 检查某个表是否存在
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static async Task<bool> TableExist(string tablename) {
            object? objCount = await ExecuteScalar("select count(*) from sqlite_master where type='table' AND name='"+ tablename + "'");
            return objCount == null ? false : Convert.ToInt32(objCount) > 0;
        }

        /// <summary>
        /// 执行非查询的数据库操作
        /// </summary>
        /// <param name="sqlString">要执行的sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回受影响的条数</returns>
        public static async Task<int> ExecuteNonQuery(string sqlString, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connstring))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlString;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// 执行查询并返回查询结果第一行第一列
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="sqlparams">参数列表</param>
        /// <returns></returns>
        public static async Task<object?> ExecuteScalar(string sqlString, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connstring))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlString;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return await cmd.ExecuteScalarAsync();
                }
            }
        }

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回查询的数据表</returns>
        public static DataTable GetDataTable(string sqlString, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connstring))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlString;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    DataTable dt = new DataTable();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
