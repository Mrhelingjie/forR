using System;
using System.Data;
using System.Data.SqlClient;

namespace Mejoy.DataAccess.Admin
{
    public class AdminMenuData : BaseDataAccess
    {
        /// <summary>
        /// 后台管理菜单类型
        /// </summary>
        public enum AdminMenuType : byte
        {
            All = 1,      //所有菜单
            User = 2,     //用户菜单
            User2 = 3,     //用户菜单
        }


        /// <summary>
        /// 添加
        /// </summary>
        public void Insert()
        {
            string Sql = "INSERT INTO T_Admin_Menu(Parent_Id,[Name],[Link],[Index],Tree_Close,[Type],[Target]) VALUES(@ParentId,@Name,@Link,@Index,@TreeClose,@Type,@Target)";

            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.Add("@ParentId", SqlDbType.Int, 4).Value = this._ParentId;
            Cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = this._Name;
            Cmd.Parameters.Add("@Link", SqlDbType.VarChar, 200).Value = this._Link;
            Cmd.Parameters.Add("@Index", SqlDbType.SmallInt, 2).Value = this._Index;
            Cmd.Parameters.Add("@TreeClose", SqlDbType.TinyInt, 1).Value = this._TreeClose;
            Cmd.Parameters.Add("@Type", SqlDbType.TinyInt, 1).Value = this._Type;
            Cmd.Parameters.Add("@Target", SqlDbType.TinyInt, 1).Value = this._Target;

            this.EventId = this.ExecuteNonQuery(Cmd);
        }//End Insert();


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="MenuId"></param>
        public void Update(uint MenuId)
        {
            //不能将已有子菜单的一级分类移到其他分类下
            //是否移动
            string Sql = string.Format("SELECT TOP 1 Parent_Id FROM T_Admin_Menu WHERE Menu_Id={0}", MenuId);
            int ParentId = this.ExecuteScalar<int>(Sql);

            if (ParentId != this._ParentId)
            {
                //移动
                Sql = string.Format("SELECT COUNT(*) FROM T_Admin_Menu WHERE Parent_Id={0}", MenuId);
                int Count = this.ExecuteScalar<int>(Sql);
                if (Count > 0)
                {
                    this.EventId = 1;
                    return;
                }
            }

            //修改
            Sql = "UPDATE T_Admin_Menu SET Parent_Id=@ParentId,[Name]=@Name,[Link]=@Link,[Index]=@Index,Tree_Close=@TreeClose,[Type]=@Type,[Target]=@Target WHERE Menu_Id=@MenuId";

            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.Add("@ParentId", SqlDbType.Int, 4).Value = this._ParentId;
            Cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = this._Name;
            Cmd.Parameters.Add("@Link", SqlDbType.VarChar, 200).Value = this._Link;
            Cmd.Parameters.Add("@Index", SqlDbType.SmallInt, 2).Value = this._Index;
            Cmd.Parameters.Add("@TreeClose", SqlDbType.TinyInt, 1).Value = this._TreeClose;
            Cmd.Parameters.Add("@Type", SqlDbType.TinyInt, 1).Value = this._Type;
            Cmd.Parameters.Add("@Target", SqlDbType.TinyInt, 1).Value = this._Target;
            Cmd.Parameters.Add("@MenuId", SqlDbType.Int, 4).Value = MenuId;

            this.EventId = this.ExecuteNonQuery(Cmd);
        }//End Update()


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="MenuId"></param>
        public void Delete(uint MenuId)
        {
            //统计子菜单
            string Sql = string.Format("SELECT COUNT(*) FROM T_Admin_Menu WHERE Parent_Id={0}", MenuId);
            int Count = this.ExecuteScalar<int>(Sql);
            if (Count > 0)
            {
                this.EventId = 1;  //还有子菜单
            }
            else
            {
                //删除
                Sql = string.Format("DELETE FROM T_Admin_Menu WHERE Menu_Id={0}", MenuId);
                this.EventId = this.ExecuteNonQuery(Sql);
            }
        }//End Delete()


        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        public DataRow Detail(uint MenuId)
        {
            string Sql = string.Format("SELECT TOP 1 * FROM T_Admin_Menu WHERE Menu_Id={0}", MenuId);

            return this.ExecuteDataRow(Sql);
        }


        /// <summary>
        /// 取所有菜单
        /// </summary>
        /// <returns></returns>
        public DataTable Fill(string type)
        {
            string Sql = "SELECT Menu_Id,Parent_Id,[Name],[Link],[Index],Tree_Close,[Target] FROM T_Admin_Menu ";
            Sql += "WHERE [Type]=" + Convert.ToInt32(type) + " ";
            Sql += "ORDER BY [Index] DESC,Menu_Id DESC";

            return this.ExecuteDataTable(Sql);
        }
        /// <summary>
        /// 取所有菜单(重载)
        /// </summary>
        /// <returns></returns>
        //public DataTable Fill(string type)
        //{
        //    return this.Fill(type);
        //}//End Fill()


        /// <summary>
        /// 取所有一级菜单
        /// </summary>
        /// <returns></returns>
        public DataTable FillTopMenu(AdminMenuType type, uint AdminId, byte Level)
        {
            string Sql = "";
            string MenuIds = "";

            if (type == AdminMenuType.User)
            {
                //取用户允许访问的菜单
                Sql = string.Format("SELECT TOP 1 [Permission] FROM MJ_Admin WHERE Admin_Id={0}", AdminId);
                MenuIds = this.ExecuteScalar<string>(Sql);
            }

            string Select = "SELECT Menu_Id,Parent_Id,[Name],[Link],[Index],Tree_Close,[Type] ";
            string From = "FROM T_Admin_Menu ";
            string Order = "ORDER BY [Index] DESC, Menu_Id DESC";

            string Where = "WHERE Parent_Id=0 ";
            if (type == AdminMenuType.User)
            {
                Where += " AND [Type]=0 ";
                if (Level != 99 && !string.IsNullOrEmpty(MenuIds))
                {
                    Where += string.Format(" AND (SELECT COUNT(*) FROM T_Admin_Menu WHERE Parent_Id>0 AND [Type]=0 AND Menu_Id IN({0}))>0 ", MenuIds);
                }
            }
            Sql = Select + From + Where + Order;

            return this.ExecuteDataTable(Sql);
        }
        /// <summary>
        /// 取所有一级菜单(重载)
        /// </summary>
        /// <returns></returns>
        public DataTable FillTopMenu()
        {
            return this.FillTopMenu(AdminMenuType.All, 0, 99);
        }//End FillTopMenu()


        /// <summary>
        /// 取所有一级菜单
        /// </summary>
        /// <returns></returns>
        public DataTable FillChildMenu(uint ParentId, AdminMenuType type, uint AdminId)
        {
            string MenuIds = "";
            string Sql = "";

            if (type == AdminMenuType.User)
            {
                //取用户允许访问的菜单
                Sql = string.Format("SELECT TOP 1 [Permission] FROM MJ_Admin WHERE Admin_Id={0}", AdminId);
                MenuIds = this.ExecuteScalar<string>(Sql);
            }

            string Select = "SELECT Menu_Id,Parent_Id,[Name],[Link],[Index],Tree_Close,[Type],[Target] ";
            string From = "FROM T_Admin_Menu ";
            string Where = string.Format("WHERE Parent_Id={0} ", ParentId);
            string Order = "ORDER BY [Index] DESC";
            if (type == AdminMenuType.User)
            {
                Where += "AND [Type]=0 ";
                if (!string.IsNullOrEmpty(MenuIds))
                {
                    Where += string.Format("AND Menu_Id IN({0}) ", MenuIds);
                }
            }
            Sql = Select + From + Where + Order;

            return this.ExecuteDataTable(Sql);
        }//End FillChildMenu()

        /// <summary>
        /// 取所有一级菜单(重载)
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public DataTable FillChildMenu(uint ParentId)
        {
            return this.FillChildMenu(ParentId, AdminMenuType.All, 0);
        }//End FillChildMenu()


        /// <summary>
        /// 移动菜单
        /// </summary>
        /// <param name="MenuId"></param>
        /// <param name="Type"></param>
        public void MoveMenu(uint MenuId, string Type)
        {
            int Move = (Type.ToLower() == "up") ? 1 : -1;

            string Sql = string.Format("UPDATE T_Admin_Menu SET [Index]=[Index]+{0} WHERE Menu_Id={1}", Move, MenuId);
            this.ExecuteNonQuery(Sql);
        }



        /// <summary>
        /// 判断是否是合法的链接
        /// </summary>
        public bool IsValidAdminLink(uint AdminId, byte Level, string Rights)
        {
            if (Level == 100)
            {
                return true;
            }
            else
            {
                //取当前链接的文件
                string Url = "";
                try
                {
                    Url = Convert.ToString(Context.Request.ServerVariables["Url"]);
                }
                catch { }
                try
                {
                    Url = Url.Substring(Common.Config.DIR_ADMIN.Length);
                }
                catch
                {
                    Url = "";
                }
                //当前链接的编号
                Sql = "SELECT TOP 1 Menu_Id,[Type] FROM T_Admin_Menu WHERE [Link]=@Link";

                Cmd = new SqlCommand();
                Cmd.CommandText = Sql;
                Cmd.Parameters.Add("@Link", SqlDbType.VarChar, 200).Value = Url;
                DataRow dr = this.ExecuteDataRow(Cmd);
                if (dr == null)
                {
                    return false;  //无效链接
                }
                else
                {
                    uint MenuId = Convert.ToUInt32(dr["Menu_Id"]);
                    byte Type = Convert.ToByte(dr["Type"]);

                    if (Type == 1)
                    {
                        return false;  //仅系统调试员对高级菜单内容才有操作权限
                    }
                    else if (Level == 99)
                    {
                        return true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Rights))
                        {
                            return false;
                        }
                        else
                        {
                            string[] All = Rights.Split(",".ToCharArray());
                            for (uint i = 0; i < All.Length; i++)
                            {
                                if (All[i].ToString() == MenuId.ToString())
                                {
                                    return true;
                                }
                            }
                            return false;
                        }
                    }
                }
            }
        }


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
        /// 菜单编号
        /// </summary>
        public uint _MenuId { get; set; }
        /// <summary>
        /// 上级编号
        /// </summary>
        public uint _ParentId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 菜单链接
        /// </summary>
        public string _Link { get; set; }
        /// <summary>
        /// 排序编号（正数提升，负数下降）
        /// </summary>
        public int _Index { get; set; }
        /// <summary>
        /// 菜单关闭
        /// </summary>
        public byte _TreeClose { get; set; }
        /// <summary>
        /// 类型：0-普通菜单，1-高级菜单
        /// </summary>
        public byte _Type { get; set; }
        /// <summary>
        /// 打开窗口：0-默认，1－新窗口
        /// </summary>
        public byte _Target { get; set; }
        #endregion
    }
}
