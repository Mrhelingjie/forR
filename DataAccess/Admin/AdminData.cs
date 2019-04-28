using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using Mejoy.Library;

namespace Mejoy.DataAccess.Admin
{
    /// <summary>
    /// 模块功能：管理员数据层
    /// </summary>
    public class AdminData : BaseDataAccess
    {
        /// <summary>
        /// 添加新管理员
        /// </summary>
        public void Insert()
        {
            if (this.Exists(this._LoginName))
            {
                this.EventId = 11;
            }
            else
            {
                string Sql = "INSERT INTO T_Admin(Login_Name,[Password],[Note],[Level],[Rights],[Lock],[Areas]) ";
                Sql += "VALUES(@Login_Name,@Password,@Note,@Level,@Rights,@Lock,@Area)";

                Cmd = new SqlCommand();
                Cmd.CommandText = Sql;
                Cmd.Parameters.Add("@Login_Name", SqlDbType.VarChar, 30).Value = this._LoginName;
                Cmd.Parameters.Add("@Password", SqlDbType.VarChar, 32).Value = Function.MD5(this._Password);
                Cmd.Parameters.Add("@Note", SqlDbType.VarChar, 100).Value = this._Note;
                Cmd.Parameters.Add("@Level", SqlDbType.TinyInt).Value = this._Level;
                Cmd.Parameters.Add("@Rights", SqlDbType.VarChar, 1000).Value = this._Rights;
                Cmd.Parameters.Add("@Lock", SqlDbType.Bit).Value = this._Lock;
                Cmd.Parameters.Add("@Area", SqlDbType.VarChar,100).Value = this._Area;
                this.EventId = this.ExecuteNonQuery(Cmd);
            }
        }//End Insert()


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="AdminId">管理帐号编号</param>
        public void Update(uint AdminId)
        {
            //判断用户名是否存在
            if (this.Exists(this._LoginName, AdminId))
            {
                this.EventId = 11;   //已有相同的登录帐号名
            }
            else
            {
                //判断是否是将最后一个可用的超级管理员设为普通管理员
                if (this._Level != 99 && this._Lock != 1)
                {
                    byte Count = 0;
                    Sql = string.Format("SELECT Count(*) FROM T_Admin WHERE [Lock]=0 AND Admin_Id<>{0}", AdminId);
                    Count = this.ExecuteScalar<byte>(Sql);
                    if (Count == 0)
                    {
                        this.EventId = 12;  //不能修改管理员级别
                        return;
                    }
                }

                //更新
                Sql = "UPDATE T_Admin SET ";
                Sql += "Login_Name=@Login_Name,";
                if (this._Password != "")
                {
                    Sql += "[Password]=@Password,";
                }
                Sql += "Note=@Note,Rights=@Rights,[Level]=@Level,[Lock]=@Lock,[Areas]=@Area ";
                Sql += "WHERE Admin_Id=@Admin_Id";

                Cmd = new SqlCommand();
                Cmd.CommandText = Sql;
                Cmd.Parameters.Add("@Login_Name", SqlDbType.VarChar, 30).Value = this._LoginName;
                if (this._Password != "")
                {
                    Cmd.Parameters.Add("@Password", SqlDbType.VarChar, 32).Value = Function.MD5(this._Password);
                }
                Cmd.Parameters.Add("@Note", SqlDbType.VarChar, 100).Value = this._Note;
                Cmd.Parameters.Add("@Rights", SqlDbType.VarChar, 500).Value = this._Rights;
                Cmd.Parameters.Add("@Level", SqlDbType.TinyInt).Value = this._Level;
                Cmd.Parameters.Add("@Lock", SqlDbType.TinyInt).Value = this._Lock;
                Cmd.Parameters.Add("@Admin_Id", SqlDbType.Int).Value = AdminId;
                Cmd.Parameters.Add("@Area", SqlDbType.VarChar, 100).Value = this._Area;
                this.EventId = this.ExecuteNonQuery(Cmd);
            }
        }//End Update()


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="AdminId">管理帐号编号</param>
        public void Delete(uint AdminId)
        {
            //判断是否是在删除最后一个可用超级管理员
            byte CurLevel = 0;
            Sql = string.Format("SELECT [Level] FROM T_Admin WHERE Admin_Id={0}", AdminId);
            CurLevel = this.ExecuteScalar<byte>(Sql);
            if (CurLevel == 99)
            {
                //统计删除后可用超级管理员的数目
                int Count = 0;
                Sql = string.Format("SELECT COUNT(*) FROM T_Admin WHERE [Level]=99 AND Admin_Id<>{0}", AdminId);
                Count = this.ExecuteScalar<int>(Sql);
                if (Count < 1)
                {
                    this.EventId = 1;   //不能删除
                    return;
                }
            }
            //删除
            Sql = string.Format("DELETE FROM T_Admin WHERE Admin_Id={0}", AdminId);
            this.ExecuteNonQuery(Sql);
        }//End Delete()


        /// <summary>
        /// 详细内容
        /// </summary>
        /// <param name="AdminId">管理帐号编号</param>
        /// <returns></returns>
        public DataRow Detail(uint AdminId)
        {
            Sql = string.Format("SELECT TOP 1 * FROM T_Admin WHERE Admin_Id={0}", AdminId);
            return this.ExecuteDataRow(Sql);
        }//End Detail()


        /// <summary>
        /// 取记录
        /// </summary>
        /// <param name="Num">记录数</param>
        /// <param name="Page">当前页数</param>
        /// <param name="Key">搜索字符</param>
        /// <param name="Total">总记录数</param>
        /// <returns></returns>
        public DataTable Fill(uint Num, uint Page, string Key, out uint Total)
        {
            string Select = string.Format("SELECT TOP {0} Admin_Id,Login_Name,Note,[Level],[Lock],[Areas] ", Num);
            string From = "FROM T_Admin ";
            string Order = "ORDER BY Admin_Id DESC ";
            string Where = "WHERE 1=1 ";
            if (Key != "")
            {
                Where += string.Format(" AND (Login_Name LIKE '%{0}%' OR True_Name LIKE '%{0}%')", Key);
            }
            string CountWhere = Where;
            if (Page > 1)
            {
                Where += string.Format(" AND Admin_Id NOT IN(SELECT TOP {0} Admin_Id {1} {2} {3}) ", (Page - 1) * Num, From, Where, Order);
            }
            Sql = Select + From + Where + Order;

            DataTable dt = this.ExecuteDataTable(Sql);

            //统计
            Sql = string.Format("SELECT COUNT(*) {0} {1}", From, CountWhere);
            Total = this.ExecuteScalar<uint>(Sql);

            return dt;
        }//End Fill()


        /// <summary>
        /// 当前登录管理员密码修改
        /// </summary>
        /// <param name="AdminId"></param>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        public void EditPassword(uint AdminId, string OldPassword, string NewPassword)
        {
            //判断旧密码是否正确
            int Count = 0;
            Sql = string.Format("SELECT COUNT(*) FROM T_Admin WHERE Admin_Id={0} AND [Password]='{1}'", AdminId, OldPassword);
            Count = this.ExecuteScalar<int>(Sql);
            if (Count > 0)
            {
                //修改
                Sql = string.Format("UPDATE T_Admin SET [Password]='{0}' WHERE Admin_Id={1}", NewPassword, AdminId);
                this.EventId = this.ExecuteNonQuery(Sql);
            }
            else
            {
                this.EventId = 2;  //旧密码不正确
            }
        }//End EditPassword()


        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="LoginName">登录帐号</param>
        /// <param name="Password">登录密码</param>
        public void Login(string LoginName, string Password)
        {
            if (!Function.IsValidLoginName(LoginName) || Password == String.Empty || Password == "")
            {
                this.EventId = 1;
            }
            else
            {
                Sql = "SELECT TOP 1 * ";
                Sql += "FROM T_Admin ";
                Sql += "WHERE Login_Name=@LoginName AND [Password]=@Password";

                Cmd = new SqlCommand();
                Cmd.CommandText = Sql;
                Cmd.Parameters.Add("@LoginName", SqlDbType.VarChar, 30).Value = LoginName;
                Cmd.Parameters.Add("@Password", SqlDbType.VarChar, 32).Value = Password;

                DataRow dr = this.ExecuteDataRow(Cmd);

                if (dr == null)
                {
                    this.EventId = 2;   //帐号或密码不正确
                }
                else
                {
                    if (Convert.ToByte(dr["Lock"]) != 0)
                    {
                        this.EventId = 3;   //帐号已被锁定
                    }
                    else
                    {
                        this._AdminId = Convert.ToUInt16(dr["Admin_Id"]);
                        this._Level = Convert.ToByte(dr["Level"]);
                        this._Rights = Convert.ToString(dr["Rights"]);
                        this._Area = Convert.ToString(dr["Areas"]);
                        HttpCookie cookie = new HttpCookie(Common.Config.COOKIE_ADMIN_LOGIN_KEY);
                        Session["UserType"] = Convert.ToString(dr["UserType"]);
                        Session["RealName"] = Convert.ToString(dr["RealName"]);
                        cookie.Values.Add("AdminId", this._AdminId.ToString());
                        cookie.Values.Add("LoginName", LoginName);
                        cookie.Values.Add("RealName", Convert.ToString(dr["RealName"]));
                        cookie.Values.Add("UserType", Convert.ToString(dr["UserType"]));
                        Context.Response.Cookies.Add(cookie);

                        Session["REALNAME"] = Convert.ToString(dr["RealName"]);
                    }
                }
            }
        }//End Login()


        /// <summary>
        /// 检查帐号是否存在
        /// </summary>
        /// <param name="LoginName">检查的帐号</param>
        /// <param name="AdminId">不包含的编号</param>
        /// <returns></returns>
        public bool Exists(string LoginName, uint AdminId)
        {
            if (!Function.IsValidLoginName(LoginName))
            {
                return true;
            }
            else
            {
                Sql = string.Format("SELECT COUNT(*) FROM T_Admin WHERE Login_Name='{0}' ", LoginName);
                if (AdminId > 0)
                {
                    Sql += string.Format("AND Admin_Id<>{0}", AdminId);
                }

                int Count = this.ExecuteScalar<int>(Sql);

                return (Count > 0) ? true : false;
            }
        }
        /// <summary>
        /// 检查帐号是否存在
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public bool Exists(string LoginName)
        {
            return this.Exists(LoginName, 0);
        }//End Exists();


        /// <summary>
        /// 取管理员ID
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public uint GetAdminId(string LoginName)
        {
            Sql = string.Format("SELECT TOP 1 Admin_Id FROM T_Admin WHERE  Login_Name='{0}'", LoginName);
            return this.ExecuteScalar<uint>(Sql);
        }//End GetAdminId();


        /// <summary>
        /// 取管理员说明
        /// </summary>
        /// <param name="AdminId"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public string GetAdminNote(uint AdminId, string LoginName)
        {
            Sql = "SELECT TOP 1 [Note] FROM T_Admin WHERE 1=1 ";
            if (AdminId > 0)
            {
                Sql += string.Format("AND Admin_Id={0} ", AdminId);
            }
            if (!string.IsNullOrEmpty(LoginName))
            {
                Sql += string.Format("AND Login_Name='{0}' ", LoginName);
            }

            string Note = this.ExecuteScalar<string>(Sql);
            return (string.IsNullOrEmpty(Note)) ? "" : Note;
        }//End GetAdminNote();


        #region  全局属性
        /// <summary>
        /// 操作标记
        /// </summary>
        public int _EventId
        {
            get
            {
                return this.EventId;
            }
        }
        /// <summary>
        /// 管理员编号
        /// </summary>
        public uint _AdminId { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string _LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string _Password { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string _Note { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public byte _Level { get; set; }
        /// <summary>
        /// 锁定
        /// </summary>
        public byte _Lock { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string _Rights { get; set; }

        /// <summary>
        /// 小区
        /// </summary>
        public string _Area{ get; set; }
        #endregion
    }
}
