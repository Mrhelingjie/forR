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
    /// ���ݿ��������
    /// </summary>
    public class Database : SingleTon<Database>
    {
        #region �ֶ�
        /// <summary>
        /// ��ǰʹ�õ����ݿ��ṩ������
        /// </summary>
        private string _providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
        /// <summary>
        /// ��ǰʹ�õ����ݿ������ַ���
        /// </summary>
        private string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        /// <summary>
        /// �����ṩ������
        /// </summary>
        private DbProviderFactory _factory;
        #endregion

        #region ˽�з���
        /// <summary>
        /// ���������Ӳ���
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

        #region ���캯��
        public Database()
        {
            _factory = DbProviderFactories.GetFactory(_providerName);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����ĳ��,��ͼ�ļܹ�
        /// </summary>
        /// <param name="tableName">��������ͼ��</param>
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
        /// ִ�в�ѯ��䷵�����ݶ�ȡ��
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <param name="parameters">DbParam����</param>
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
        /// ִ�в�ѯ���
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <param name="parameters">DbParam����</param>
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
        /// ִ�з�SQL��ѯ���
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <param name="parameters">DbParam����</param>
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
        /// ִ�б�����ѯ���
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <param name="parameters">DbParam����</param>
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