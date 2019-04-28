using System.Data;
using System.Data.SqlClient;

namespace Mejoy.DataAccess.BasicPage
{
    public class BasicPage : BaseDataAccess
    {
        /// <summary>
        /// 插入
        /// </summary>
        public void Insert()
        {
            string Sql = "INSERT INTO T_BasicPage([Label],[Title],[Class],[Content]) ";
            Sql += "VALUES(@Label,@Title,@Class,@Content)";

            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.Add("@Label", SqlDbType.VarChar, 50).Value = this._Label;
            Cmd.Parameters.Add("@Title", SqlDbType.VarChar, 255).Value = this._Title;
            Cmd.Parameters.Add("@Class", SqlDbType.VarChar, 50).Value = this._Class;
            Cmd.Parameters.Add("@Content", SqlDbType.Text).Value = this._Content;

            this.EventId = this.ExecuteNonQuery(Cmd);
        }//End Insert();


        /// <summary>
        /// 更新
        /// </summary>
        public void Update(uint PageId)
        {
            string Sql = "UPDATE T_BasicPage SET [Label]=@Label,[Class]=@Class,[Title]=@Title,[Content]=@Content ";
            Sql += "WHERE Page_Id=@Page_Id";

            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.Add("@Label", SqlDbType.VarChar, 50).Value = this._Label;
            Cmd.Parameters.Add("@Title", SqlDbType.VarChar, 255).Value = this._Title;
            Cmd.Parameters.Add("@Class", SqlDbType.VarChar, 50).Value = this._Class;
            Cmd.Parameters.Add("@Content", SqlDbType.Text).Value = this._Content;
            Cmd.Parameters.Add("@Page_Id", SqlDbType.Int, 4).Value = PageId;
            this.EventId = this.ExecuteNonQuery(Cmd);
        }//End Update();


        /// <summary>
        /// 删除
        /// </summary>
        public void Delete(uint PageId)
        {
            string Sql = "DELETE FROM T_BasicPage WHERE Page_Id=@Page_Id";

            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.Add("@Page_Id", SqlDbType.Int, 4).Value = PageId;

            this.EventId = this.ExecuteNonQuery(Cmd);
        }//End Delete();



        /// <summary>
        /// 删除(多条)
        /// </summary>
        public void Delete(string Ids)
        {
            if (string.IsNullOrEmpty(Ids.Trim()))
            {
                return;
            }

            string Sql = string.Format("DELETE FROM T_BasicPage WHERE Page_Id IN({0})", Ids.Trim(",".ToCharArray()));

            this.EventId = this.ExecuteNonQuery(Sql);
        }//End Delete();



        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public DataRow Detail(uint PageId, string Label)
        {
            string Sql = "SELECT TOP 1 * FROM T_BasicPage WHERE ";
            if (PageId > 0)
            {
                Sql += string.Format("Page_Id={0}", PageId);
            }
            else
            {
                Sql += string.Format("[Label]='{0}'", Label);
            }

            return this.ExecuteDataRow(Sql);
        }//End Detail();
        public DataRow Detail(uint PageId)
        {
            return this.Detail(PageId, "");
        }//End Detail();
        public DataRow Detail(string Label)
        {
            return this.Detail(0, Label);
        }//End Detail();



        /// <summary>
        /// 取记录
        /// </summary>
        /// <returns></returns>
        public DataTable Fill(uint Num, uint Page, string Field, out uint Total, bool Count)
        {
            //取记录
            if (string.IsNullOrEmpty(Field))
            {
                Field = "b.Page_Id,b.[Label],b.[Title],b.[Content]";
            }
            string Select = string.Format("SELECT TOP {0} {1} ", Num, Field);
            string From = "FROM T_BasicPage AS b ";
            string OrderBy = "ORDER BY b.Page_Id DESC ";
            string Where = "WHERE DATALENGTH(b.[Title])>0 ";
            string CountWhere = Where;
            if (Page > 1)
            {
                Where += string.Format("AND b.Page_Id NOT IN(SELECT TOP {0} b.Page_Id {1} {2} {3}) ", (Page - 1) * Num, From, Where, OrderBy);
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



        /// <summary>
        /// 取记录(分类)
        /// </summary>
        /// <returns></returns>
        public DataTable Fill(uint Num, uint Page, string Field, string ClassName, out uint Total, bool Count)
        {
            //取记录
            if (string.IsNullOrEmpty(Field))
            {
                Field = "b.Page_Id,b.[Label],b.[Title],b.[Content]";
            }
            string Select = string.Format("SELECT TOP {0} {1} ", Num, Field);
            string From = "FROM T_BasicPage AS b ";
            string OrderBy = "ORDER BY b.Page_Id DESC ";
            string Where = "WHERE DATALENGTH(b.[Title])>0 ";
            string CountWhere = Where;
            if (!string.IsNullOrEmpty(ClassName))
            {
                Where += string.Format("AND b.Class='{0}' ", ClassName);
            }
            if (Page > 1)
            {
                Where += string.Format("AND b.Page_Id NOT IN(SELECT TOP {0} b.Page_Id {1} {2} {3}) ", (Page - 1) * Num, From, Where, OrderBy);
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



        /// <summary>
        /// 获取自定义下分类列表
        /// </summary>
        /// <returns></returns>
        public DataTable FillClass()
        {
            string Sql = "SELECT DISTINCT([Class]) FROM T_BasicPage WHERE [Class] IS NOT NULL";

            DataTable dt = this.ExecuteDataTable(Sql);
            return dt;
        }



        /// <summary>
        /// 设置某列值
        /// </summary>
        public void SetValue(uint PageId, string CellName, object CellValue)
        {
            string Sql = string.Format("UPDATE T_BasicPage SET {0}=@{0} WHERE PageId=@PageId", CellName);
            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.AddWithValue("@" + CellName, CellValue);
            Cmd.Parameters.Add("@Page_Id", SqlDbType.Int, 4).Value = PageId;
            this.ExecuteNonQuery(Cmd);
        }//End SetValue();



        /// <summary>
        /// 获取某列值
        /// </summary>
        /// <returns></returns>
        public T GetValue<T>(uint PageId, string CellName)
        {
            string Sql = string.Format("SELECT TOP 1 [{0}] FROM T_BasicPage WHERE Page_Id={1}", CellName, PageId);
            return this.ExecuteScalar<T>(Sql);
        }//End GetValue();



        /// <summary>
        /// 修改Class值
        /// </summary>
        /// <param name="CellValue"></param>
        public void SetClassValue(string oldValue, string newValue)
        {
            string Sql = "UPDATE T_BasicPage SET [Class]=@Class WHERE [Class]=@OldClass";
            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.AddWithValue("@Class", newValue);
            Cmd.Parameters.AddWithValue("@OldClass", oldValue);
            this.ExecuteNonQuery(Cmd);
        }



        /// <summary>
        /// 检查Class值是否已经存在
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public bool IsExsits(string className)
        {
            string Sql = string.Format("SELECT TOP 1 [Page_Id] FROM T_BasicPage WHERE [Class]='{0}'", className);

            DataTable dt = this.ExecuteDataTable(Sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteByClass(string className)
        {
            string Sql = "DELETE FROM T_BasicPage WHERE [Class]=@Class";

            Cmd = new SqlCommand();
            Cmd.CommandText = Sql;
            Cmd.Parameters.Add("@Class", SqlDbType.VarChar, 50).Value = className;

            this.EventId = this.ExecuteNonQuery(Cmd);
        }//End Delete();



        /// <summary>
        /// 检查Class值是否已经被使用（删除）
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public bool IsUsed(string className)
        {
            string Sql = string.Format("SELECT TOP 1 [Page_Id] FROM T_BasicPage WHERE [Class]='{0}' AND DATALENGTH([Title])>0", className);

            DataTable dt = this.ExecuteDataTable(Sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                return true;
            }
            return false;
        }



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
        /// <summary>
        /// 标签
        /// </summary>
        public string _Label { get; set; }
        /// <summary>
        /// 分类，主要应用于自定义部分
        /// </summary>
        public string _Class { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string _Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string _Content { get; set; }
        #endregion
    }
}
