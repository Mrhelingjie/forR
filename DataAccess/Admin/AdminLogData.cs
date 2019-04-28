using System.Data;
using System.Data.SqlClient;

using Mejoy.Library;

namespace Mejoy.DataAccess.Admin
{
    public class AdminLogData : BaseDataAccess
    {
        /// <summary>
        /// 插入
        /// </summary>
        public void Add(uint AdminId, string AdminLoginName, string Event, byte EventLevel)
        {
            string Sql = "INSERT INTO T_Admin_Log(Admin_Id,Admin_Login_Name,[Event],Event_Time,Event_IP,Event_Level) ";
            Sql += "VALUES(@Admin_Id,@Admin_Login_Name,@Event,GETDATE(),@Event_IP,@Event_Level)";

            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.Add("@Admin_Id", SqlDbType.Int, 4).Value = AdminId;
            Cmd.Parameters.Add("@Admin_Login_Name", SqlDbType.VarChar, 30).Value = AdminLoginName;
            Cmd.Parameters.Add("@Event", SqlDbType.VarChar, 255).Value = Event;
            Cmd.Parameters.Add("@Event_Level", SqlDbType.TinyInt, 1).Value = EventLevel;
            Cmd.Parameters.Add("@Event_IP", SqlDbType.VarChar, 20).Value = Function.GetUserIP();

            this.EventId = this.ExecuteNonQuery(Cmd);
        }//End Insert();


        /// <summary>
        /// 删除
        /// </summary>
        public void Delete(uint LogId, uint Days)
        {
            string Sql = "DELETE FROM T_Admin_Log WHERE 1=1 ";
            if (LogId > 0)
            {
                Sql += string.Format("AND Log_Id={0} ", LogId);
            }
            if (Days > 0)
            {
                Sql += string.Format("AND DATEDIFF(day, Event_Time, GETDATE())>{0} ", Days);
            }

            this.EventId = this.ExecuteNonQuery(Sql);
        }//End Delete();



        /// <summary>
        /// 取记录
        /// </summary>
        /// <returns></returns>
        public DataTable Fill(uint Num, uint Page, string Field, uint AdminId, int EventLevel, out uint Total, bool Count)
        {
            //取记录
            if (string.IsNullOrEmpty(Field))
            {
                Field = "l.Log_Id,l.Admin_Id,l.Admin_Login_Name,l.[Event],l.Event_Time,l.Event_IP,l.Event_Level";
            }
            string Select = string.Format("SELECT TOP {0} {1} ", Num, Field);
            string From = "FROM T_Admin_Log AS l ";
            string OrderBy = "ORDER BY l.Log_Id DESC ";
            string Where = "WHERE 1=1 ";
            //管理ID
            if (AdminId > 0)
            {
                Where += string.Format("AND Admin_Id={0} ", AdminId);
            }
            //警告级别
            if (EventLevel != -1)
            {
                Where += string.Format("AND Event_Level={0} ", EventLevel);
            }
            string CountWhere = Where;
            if (Page > 1)
            {
                Where += string.Format("AND l.Log_Id NOT IN(SELECT TOP {0} l.Log_Id {1} {2} {3}) ", (Page - 1) * Num, From, Where, OrderBy);
            }
            string Sql = Select + From + Where + OrderBy;

            DataTable dt = this.ExecuteDataTable(Sql);

            //统计
            Total = 0;
            if (Count)
            {
                Sql = string.Format("SELECT COUNT(*) {0} {1}", From, CountWhere);
                Total = this.ExecuteScalar<uint>(Sql);
            }

            return dt;
        }//End Fill();


        #region  全局属性定义
        /// <summary>
        /// 操作标记(只读)
        /// </summary>
        public int _EventId
        {
            get
            {
                return this.EventId;
            }
        }
        #endregion
    }
}
