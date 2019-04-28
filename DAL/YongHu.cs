using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:YongHu
	/// </summary>
	public partial class YongHu
	{
		public YongHu()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "YongHu"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YongHu");
			strSql.Append(" where ID="+ID+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.YongHu model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.Number != null)
			{
				strSql1.Append("Number,");
				strSql2.Append("'"+model.Number+"',");
			}
			if (model.Name != null)
			{
				strSql1.Append("Name,");
				strSql2.Append("'"+model.Name+"',");
			}
			if (model.Address != null)
			{
				strSql1.Append("Address,");
				strSql2.Append("'"+model.Address+"',");
			}
			if (model.Phone != null)
			{
				strSql1.Append("Phone,");
				strSql2.Append("'"+model.Phone+"',");
			}
			if (model.BiaoHao != null)
			{
				strSql1.Append("BiaoHao,");
				strSql2.Append("'"+model.BiaoHao+"',");
			}
			if (model.IdCard != null)
			{
				strSql1.Append("IdCard,");
				strSql2.Append("'"+model.IdCard+"',");
			}
			if (model.XiaoQu != null)
			{
				strSql1.Append("XiaoQu,");
				strSql2.Append("'"+model.XiaoQu+"',");
			}
			if (model.DanYuanHao != null)
			{
				strSql1.Append("DanYuanHao,");
				strSql2.Append("'"+model.DanYuanHao+"',");
			}
			strSql.Append("insert into YongHu(");
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
		public bool Update(Maticsoft.Model.YongHu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YongHu set ");
			if (model.Number != null)
			{
				strSql.Append("Number='"+model.Number+"',");
			}
			else
			{
				strSql.Append("Number= null ,");
			}
			if (model.Name != null)
			{
				strSql.Append("Name='"+model.Name+"',");
			}
			else
			{
				strSql.Append("Name= null ,");
			}
			if (model.Address != null)
			{
				strSql.Append("Address='"+model.Address+"',");
			}
			else
			{
				strSql.Append("Address= null ,");
			}
			if (model.Phone != null)
			{
				strSql.Append("Phone='"+model.Phone+"',");
			}
			else
			{
				strSql.Append("Phone= null ,");
			}
			if (model.BiaoHao != null)
			{
				strSql.Append("BiaoHao='"+model.BiaoHao+"',");
			}
			else
			{
				strSql.Append("BiaoHao= null ,");
			}
			if (model.IdCard != null)
			{
				strSql.Append("IdCard='"+model.IdCard+"',");
			}
			else
			{
				strSql.Append("IdCard= null ,");
			}
			if (model.XiaoQu != null)
			{
				strSql.Append("XiaoQu='"+model.XiaoQu+"',");
			}
			else
			{
				strSql.Append("XiaoQu= null ,");
			}
			if (model.DanYuanHao != null)
			{
				strSql.Append("DanYuanHao='"+model.DanYuanHao+"',");
			}
			else
			{
				strSql.Append("DanYuanHao= null ,");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ID="+ model.ID+"");
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
		public bool Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YongHu ");
			strSql.Append(" where ID="+ID+"" );
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YongHu ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public Maticsoft.Model.YongHu GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ID,Number,Name,Address,Phone,BiaoHao,IdCard,XiaoQu,DanYuanHao ");
			strSql.Append(" from YongHu ");
			strSql.Append(" where ID="+ID+"" );
			Maticsoft.Model.YongHu model=new Maticsoft.Model.YongHu();
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
		public Maticsoft.Model.YongHu DataRowToModel(DataRow row)
		{
			Maticsoft.Model.YongHu model=new Maticsoft.Model.YongHu();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["Number"]!=null)
				{
					model.Number=row["Number"].ToString();
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["Phone"]!=null)
				{
					model.Phone=row["Phone"].ToString();
				}
				if(row["BiaoHao"]!=null)
				{
					model.BiaoHao=row["BiaoHao"].ToString();
				}
				if(row["IdCard"]!=null)
				{
					model.IdCard=row["IdCard"].ToString();
				}
				if(row["XiaoQu"]!=null)
				{
					model.XiaoQu=row["XiaoQu"].ToString();
				}
				if(row["DanYuanHao"]!=null)
				{
					model.DanYuanHao=row["DanYuanHao"].ToString();
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
			strSql.Append("select ID,Number,Name,Address,Phone,BiaoHao,IdCard,XiaoQu,DanYuanHao ");
			strSql.Append(" FROM YongHu ");
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
			strSql.Append(" ID,Number,Name,Address,Phone,BiaoHao,IdCard,XiaoQu,DanYuanHao ");
			strSql.Append(" FROM YongHu ");
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
			strSql.Append("select count(1) FROM YongHu ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from YongHu T ");
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

