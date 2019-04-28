using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_Admin
	/// </summary>
	public partial class T_Admin
	{
		public T_Admin()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Admin_Id", "T_Admin"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Admin_Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Admin");
			strSql.Append(" where Admin_Id="+Admin_Id+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_Admin model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.Login_Name != null)
			{
				strSql1.Append("Login_Name,");
				strSql2.Append("'"+model.Login_Name+"',");
			}
			if (model.Password != null)
			{
				strSql1.Append("Password,");
				strSql2.Append("'"+model.Password+"',");
			}
			if (model.Note != null)
			{
				strSql1.Append("Note,");
				strSql2.Append("'"+model.Note+"',");
			}
			if (model.Level != null)
			{
				strSql1.Append("Level,");
				strSql2.Append(""+model.Level+",");
			}
			if (model.Rights != null)
			{
				strSql1.Append("Rights,");
				strSql2.Append("'"+model.Rights+"',");
			}
			if (model.Lock != null)
			{
				strSql1.Append("Lock,");
				strSql2.Append(""+(model.Lock? 1 : 0) +",");
			}
			if (model.Areas != null)
			{
				strSql1.Append("Areas,");
				strSql2.Append("'"+model.Areas+"',");
			}
			if (model.UserType != null)
			{
				strSql1.Append("UserType,");
				strSql2.Append("'"+model.UserType+"',");
			}
			if (model.Dept != null)
			{
				strSql1.Append("Dept,");
				strSql2.Append("'"+model.Dept+"',");
			}
			if (model.IDCard != null)
			{
				strSql1.Append("IDCard,");
				strSql2.Append("'"+model.IDCard+"',");
			}
			if (model.AddRess != null)
			{
				strSql1.Append("AddRess,");
				strSql2.Append("'"+model.AddRess+"',");
			}
			if (model.Remark != null)
			{
				strSql1.Append("Remark,");
				strSql2.Append("'"+model.Remark+"',");
			}
			if (model.RealName != null)
			{
				strSql1.Append("RealName,");
				strSql2.Append("'"+model.RealName+"',");
			}
			if (model.Sex != null)
			{
				strSql1.Append("Sex,");
				strSql2.Append("'"+model.Sex+"',");
			}
			if (model.Telephone != null)
			{
				strSql1.Append("Telephone,");
				strSql2.Append("'"+model.Telephone+"',");
			}
			strSql.Append("insert into T_Admin(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			strSql.Append(";select @@IDENTITY");
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.T_Admin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Admin set ");
			if (model.Login_Name != null)
			{
				strSql.Append("Login_Name='"+model.Login_Name+"',");
			}
			if (model.Password != null)
			{
				strSql.Append("Password='"+model.Password+"',");
			}
			if (model.Note != null)
			{
				strSql.Append("Note='"+model.Note+"',");
			}
			else
			{
				strSql.Append("Note= null ,");
			}
			if (model.Level != null)
			{
				strSql.Append("Level="+model.Level+",");
			}
			if (model.Rights != null)
			{
				strSql.Append("Rights='"+model.Rights+"',");
			}
			else
			{
				strSql.Append("Rights= null ,");
			}
			if (model.Lock != null)
			{
				strSql.Append("Lock="+ (model.Lock? 1 : 0) +",");
			}
			if (model.Areas != null)
			{
				strSql.Append("Areas='"+model.Areas+"',");
			}
			else
			{
				strSql.Append("Areas= null ,");
			}
			if (model.UserType != null)
			{
				strSql.Append("UserType='"+model.UserType+"',");
			}
			else
			{
				strSql.Append("UserType= null ,");
			}
			if (model.Dept != null)
			{
				strSql.Append("Dept='"+model.Dept+"',");
			}
			else
			{
				strSql.Append("Dept= null ,");
			}
			if (model.IDCard != null)
			{
				strSql.Append("IDCard='"+model.IDCard+"',");
			}
			else
			{
				strSql.Append("IDCard= null ,");
			}
			if (model.AddRess != null)
			{
				strSql.Append("AddRess='"+model.AddRess+"',");
			}
			else
			{
				strSql.Append("AddRess= null ,");
			}
			if (model.Remark != null)
			{
				strSql.Append("Remark='"+model.Remark+"',");
			}
			else
			{
				strSql.Append("Remark= null ,");
			}
			if (model.RealName != null)
			{
				strSql.Append("RealName='"+model.RealName+"',");
			}
			else
			{
				strSql.Append("RealName= null ,");
			}
			if (model.Sex != null)
			{
				strSql.Append("Sex='"+model.Sex+"',");
			}
			else
			{
				strSql.Append("Sex= null ,");
			}
			if (model.Telephone != null)
			{
				strSql.Append("Telephone='"+model.Telephone+"',");
			}
			else
			{
				strSql.Append("Telephone= null ,");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where Admin_Id="+ model.Admin_Id+"");
			int rowsAffected=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Admin_Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Admin ");
			strSql.Append(" where Admin_Id="+Admin_Id+"" );
			int rowsAffected=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Admin_Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Admin ");
			strSql.Append(" where Admin_Id in ("+Admin_Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.T_Admin GetModel(int Admin_Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" Admin_Id,Login_Name,Password,Note,Level,Rights,Lock,Areas,UserType,Dept,IDCard,AddRess,Remark,RealName,Sex,Telephone ");
			strSql.Append(" from T_Admin ");
			strSql.Append(" where Admin_Id="+Admin_Id+"" );
			Maticsoft.Model.T_Admin model=new Maticsoft.Model.T_Admin();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.T_Admin DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_Admin model=new Maticsoft.Model.T_Admin();
			if (row != null)
			{
				if(row["Admin_Id"]!=null && row["Admin_Id"].ToString()!="")
				{
					model.Admin_Id=int.Parse(row["Admin_Id"].ToString());
				}
				if(row["Login_Name"]!=null)
				{
					model.Login_Name=row["Login_Name"].ToString();
				}
				if(row["Password"]!=null)
				{
					model.Password=row["Password"].ToString();
				}
				if(row["Note"]!=null)
				{
					model.Note=row["Note"].ToString();
				}
				if(row["Level"]!=null && row["Level"].ToString()!="")
				{
					model.Level=int.Parse(row["Level"].ToString());
				}
				if(row["Rights"]!=null)
				{
					model.Rights=row["Rights"].ToString();
				}
				if(row["Lock"]!=null && row["Lock"].ToString()!="")
				{
					if((row["Lock"].ToString()=="1")||(row["Lock"].ToString().ToLower()=="true"))
					{
						model.Lock=true;
					}
					else
					{
						model.Lock=false;
					}
				}
				if(row["Areas"]!=null)
				{
					model.Areas=row["Areas"].ToString();
				}
				if(row["UserType"]!=null)
				{
					model.UserType=row["UserType"].ToString();
				}
				if(row["Dept"]!=null)
				{
					model.Dept=row["Dept"].ToString();
				}
				if(row["IDCard"]!=null)
				{
					model.IDCard=row["IDCard"].ToString();
				}
				if(row["AddRess"]!=null)
				{
					model.AddRess=row["AddRess"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["RealName"]!=null)
				{
					model.RealName=row["RealName"].ToString();
				}
				if(row["Sex"]!=null)
				{
					model.Sex=row["Sex"].ToString();
				}
				if(row["Telephone"]!=null)
				{
					model.Telephone=row["Telephone"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Admin_Id,Login_Name,Password,Note,Level,Rights,Lock,Areas,UserType,Dept,IDCard,AddRess,Remark,RealName,Sex,Telephone ");
			strSql.Append(" FROM T_Admin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Admin_Id,Login_Name,Password,Note,Level,Rights,Lock,Areas,UserType,Dept,IDCard,AddRess,Remark,RealName,Sex,Telephone ");
			strSql.Append(" FROM T_Admin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM T_Admin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Admin_Id desc");
			}
			strSql.Append(")AS Row, T.*  from T_Admin T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

		#endregion  Method
		#region  MethodEx

		#endregion  MethodEx
	}
}

