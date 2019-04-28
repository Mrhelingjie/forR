using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using Havsh.Application.Aml;
namespace Havsh.Application.Dal
{
    /// <summary>
    /// 数据库操作对象
    /// </summary>
    public class Database : SingleTon<Database>
    {
        #region 字段
        /// <summary>
        /// 当前使用的数据库提供器名称
        /// </summary>
        private string _providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
        /// <summary>
        /// 当前使用的数据库连接字符串
        /// </summary>
        private string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        /// <summary>
        /// 数据提供器工厂
        /// </summary>
        private DbProviderFactory _factory;
        #endregion

        #region 私有方法
        /// <summary>
        /// 命令对象添加参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        private void AddParameter(DbCommand cmd, DbParamCollection parameters)
        {
            DbParameter currentParameter = null;
            foreach (DbParam parameter in parameters)
            {
                currentParameter = cmd.CreateParameter();
                currentParameter.ParameterName = parameter.Name;
                currentParameter.DbType = (DbType)Enum.Parse(typeof(DbType), parameter.Type);
                currentParameter.Value = parameter.Value;
                cmd.Parameters.Add(currentParameter);
            }
            currentParameter = null;
        }
        #endregion

        #region 构造函数
        public Database()
        {
            _factory = DbProviderFactories.GetFactory(_providerName);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 反射某表,视图的架构
        /// </summary>
        /// <param name="tableName">表名，视图名</param>
        public DataTable GetTableReflectionSchema(string name)
        {
            using (DbConnection con = _factory.CreateConnection())
            {
                con.ConnectionString = _connectionString;
                con.Open();
                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "select * from " + name + " where 0=1";
                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                    {
                        return reader.GetSchemaTable();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句返回数据读取器
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">DbParam集合</param>
        public DbDataReader ExecuteReader(string strSql, DbParamCollection parameters)
        {
            DbConnection con = _factory.CreateConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            DbCommand cmd = con.CreateCommand();
            cmd.CommandText = strSql;
            if (parameters != null)
            {
                AddParameter(cmd, parameters);
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">DbParam集合</param>
        public DataSet ExecuteSelect(string strSql, DbParamCollection parameters)
        {
            using (DbConnection con = _factory.CreateConnection())
            {
                con.ConnectionString = _connectionString;
                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = strSql;
                    if (parameters != null)
                    {
                        AddParameter(cmd, parameters);
                    }
                    using (DbDataAdapter adapter = _factory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        /// <summary>
        /// 执行非SQL查询语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">DbParam集合</param>
        public int ExecuteNonQuery(string strSql, DbParamCollection parameters)
        {
            using (DbConnection con = _factory.CreateConnection())
            {
                con.ConnectionString = _connectionString;
                con.Open();
                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = strSql;
                    if (parameters != null)
                    {
                        AddParameter(cmd, parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 执行标量查询语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">DbParam集合</param>
        public int ExecuteScale(string strSql, DbParamCollection parameters)
        {
            using (DbConnection con = _factory.CreateConnection())
            {
                con.ConnectionString = _connectionString;
                con.Open();
                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = strSql;
                    if (parameters != null)
                    {
                        AddParameter(cmd, parameters);
                    }
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        #endregion
    }
}