using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Text;
using System.Web;

using Mejoy.Common;

namespace Mejoy.DataAccess
{
    public class BaseDataAccess : System.Web.UI.Page
    {
        /// <summary>
        /// SqlCommand
        /// </summary>
        protected static SqlCommand Cmd = null;

        /// <summary>
        /// 数据库操作标记
        /// </summary>
        protected int EventId = 99;

        protected string Sql = string.Empty;


        /// <summary>
        /// 连接数据库
        /// </summary>
        protected SqlConnection DBConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.UserID = Common.Config.DB_USER_ID;
            builder.Password = Common.Config.DB_USER_PASSWORD;
            builder.DataSource = Common.Config.DB_DATA_SOURCE;
            builder.InitialCatalog = Common.Config.DB_INITIAL_CATALOG;
            builder.IntegratedSecurity = false;

            SqlConnection Conn = new SqlConnection(builder.ConnectionString);
            try
            {
                Conn.Open();
            }
            catch
            {
                builder.Clear();
                builder = null;

                HttpContext.Current.Response.Write("抱歉，数据库连接失败！");
                HttpContext.Current.Response.End();
            }

            builder.Clear();
            builder = null;

            return Conn;
        }//End DBConnection();

        public string GetConnection
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.UserID = Common.Config.DB_USER_ID;
                builder.Password = Common.Config.DB_USER_PASSWORD;
                builder.DataSource = Common.Config.DB_DATA_SOURCE;
                builder.InitialCatalog = Common.Config.DB_INITIAL_CATALOG;
                builder.IntegratedSecurity = false;
                return builder.ConnectionString;
            }
        }


        /// <summary>
        /// 执行T-SQL
        /// </summary>
        /// <param name="cmdTxt">T-SQL词句</param>
        /// <returns>返回受影响的行数，-1时表示未成功执行T-SQL</returns>
        protected int ExecuteNonQuery(string cmdText)
        {
            try
            {
                SqlConnection Conn = this.DBConnection();
                BaseDataAccess.Cmd = new SqlCommand(cmdText, Conn);
                int Num = Cmd.ExecuteNonQuery();

                Conn.Close();
                Conn.Dispose();
                Conn = null;

                return Num;
            }
            catch
            {
                return -1;
            }
        }//End ExecuteNonQuery();



        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="Cmd">SqlCommand</param>
        /// <returns>返回受影响的行数，-1时表示未成功执行T-SQL</returns>
        protected int ExecuteNonQuery(SqlCommand Cmd)
        {
            try
            {
                SqlConnection Conn = this.DBConnection();
                Cmd.Connection = Conn;
                int Num = Cmd.ExecuteNonQuery();

                Conn.Close();
                Conn.Dispose();
                Conn = null;

                return Num;
            }
            catch
            {
                return -1;
            }
        }//End ExecuteNonQuery();


        /// <summary>
        /// 执行并返回第一行第一列值
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="cmdText">T-SQL词句</param>
        /// <param name="RefValue"></param>
        protected T ExecuteScalar<T>(string cmdText)
        {
            try
            {
                SqlConnection Conn = this.DBConnection();
                Cmd = new SqlCommand(cmdText, Conn);
                T ReturnValue = (T)Convert.ChangeType(BaseDataAccess.Cmd.ExecuteScalar(), typeof(T));

                Conn.Close();
                Conn.Dispose();
                Conn = null;

                return ReturnValue;
            }
            catch
            {
                return default(T);
            }
        }//End ExecuteScalar();



        /// <summary>
        /// 执行并返回第一行第一列值
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="Cmd">SqlCommand</param>
        /// <param name="RefValue"></param>
        protected T ExecuteScalar<T>(SqlCommand Cmd)
        {
            try
            {
                SqlConnection Conn = this.DBConnection();
                Cmd.Connection = Conn;
                T ReturnValue = (T)Convert.ChangeType(BaseDataAccess.Cmd.ExecuteScalar(), typeof(T));

                Conn.Close();
                Conn.Dispose();
                Conn = null;

                return ReturnValue;
            }
            catch
            {
                return default(T);
            }
        }//End ExecuteScalar();



        /// <summary>
        /// 执行并返回第一行第一列值
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="Cmd">SqlCommand</param>
        /// <param name="RefValue"></param>
        protected void ExecuteScalar<T>(SqlCommand Cmd, ref T ReturnValue)
        {
            try
            {
                SqlConnection Conn = this.DBConnection();
                Cmd.Connection = Conn;
                ReturnValue = (T)Convert.ChangeType(BaseDataAccess.Cmd.ExecuteScalar(), typeof(T));

                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
            catch
            {
                ReturnValue = default(T);
            }
        }//End ExecuteScalar();



        /// <summary>
        /// 执行并返回数据集DataTable
        /// </summary>
        /// <param name="cmdText">T-SQL词句</param>
        /// <returns></returns>
        protected DataTable ExecuteDataTable(string cmdText)
        {
            SqlConnection Conn = this.DBConnection();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdText, Conn);
            da.Fill(dt);

            da.Dispose();
            da = null;

            Conn.Close();
            Conn.Dispose();
            Conn = null;

            return dt;
        }//End ExecuteDataTable();



        /// <summary>
        /// 执行并返回数据集DataTable
        /// </summary>
        /// <param name="Cmd">SqlCommand</param>
        /// <returns></returns>
        protected DataTable ExecuteDataTable(SqlCommand Cmd)
        {
            SqlConnection Conn = this.DBConnection();

            DataTable dt = new DataTable();
            Cmd.Connection = Conn;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            da.Fill(dt);

            da.Dispose();
            da = null;

            Conn.Close();
            Conn.Dispose();
            Conn = null;

            return dt;
        }//End ExecuteDataTable();



        /// <summary>
        /// 执行，并返回数据行DataRow
        /// </summary>
        /// <param name="cmdText">T-SQL词句</param>
        /// <returns></returns>
        protected DataRow ExecuteDataRow(string cmdText)
        {
            DataTable dt = this.ExecuteDataTable(cmdText);

            DataRow dr = null;
            if (dt.Rows.Count == 1)
            {
                dr = dt.Rows[0];
            }
            dt.Dispose();
            dt = null;

            return dr;
        }//End ExecuteDataRow();



        /// <summary>
        /// 执行，并返回数据行DataRow
        /// </summary>
        /// <param name="Cmd">SqlCommand</param>
        /// <returns></returns>
        protected DataRow ExecuteDataRow(SqlCommand Cmd)
        {
            DataTable dt = this.ExecuteDataTable(Cmd);

            DataRow dr = null;
            if (dt.Rows.Count == 1)
            {
                dr = dt.Rows[0];
            }
            dt.Dispose();
            dt = null;

            return dr;
        }//End ExecuteDataRow();
    }
}
