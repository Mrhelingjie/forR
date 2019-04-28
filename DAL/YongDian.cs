using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:YongDian
	/// </summary>
	public partial class YongDian
	{
		public YongDian()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "YongDian"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YongDian");
			strSql.Append(" where ID="+ID+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.YongDian model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.YongHuID != null)
			{
				strSql1.Append("YongHuID,");
				strSql2.Append("'"+model.YongHuID+"',");
			}
			if (model.Year != null)
			{
				strSql1.Append("Year,");
				strSql2.Append("'"+model.Year+"',");
			}
			if (model.Yue != null)
			{
				strSql1.Append("Yue,");
				strSql2.Append("'"+model.Yue+"',");
			}
			if (model.QiDuShu != null)
			{
				strSql1.Append("QiDuShu,");
				strSql2.Append("'"+model.QiDuShu+"',");
			}
			if (model.ZongJia != null)
			{
				strSql1.Append("ZongJia,");
				strSql2.Append("'"+model.ZongJia+"',");
			}
			if (model.DanJia != null)
			{
				strSql1.Append("DanJia,");
				strSql2.Append("'"+model.DanJia+"',");
			}
			if (model.WanDuShu != null)
			{
				strSql1.Append("WanDuShu,");
				strSql2.Append("'"+model.WanDuShu+"',");
			}
			if (model.ShiDuShu != null)
			{
				strSql1.Append("ShiDuShu,");
				strSql2.Append("'"+model.ShiDuShu+"',");
			}
			strSql.Append("insert into YongDian(");
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
		public bool Update(Maticsoft.Model.YongDian model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YongDian set ");
			if (model.YongHuID != null)
			{
				strSql.Append("YongHuID='"+model.YongHuID+"',");
			}
			else
			{
				strSql.Append("YongHuID= null ,");
			}
			if (model.Year != null)
			{
				strSql.Append("Year='"+model.Year+"',");
			}
			else
			{
				strSql.Append("Year= null ,");
			}
			if (model.Yue != null)
			{
				strSql.Append("Yue='"+model.Yue+"',");
			}
			else
			{
				strSql.Append("Yue= null ,");
			}
			if (model.QiDuShu != null)
			{
				strSql.Append("QiDuShu='"+model.QiDuShu+"',");
			}
			else
			{
				strSql.Append("QiDuShu= null ,");
			}
			if (model.ZongJia != null)
			{
				strSql.Append("ZongJia='"+model.ZongJia+"',");
			}
			else
			{
				strSql.Append("ZongJia= null ,");
			}
			if (model.DanJia != null)
			{
				strSql.Append("DanJia='"+model.DanJia+"',");
			}
			else
			{
				strSql.Append("DanJia= null ,");
			}
			if (model.WanDuShu != null)
			{
				strSql.Append("WanDuShu='"+model.WanDuShu+"',");
			}
			else
			{
				strSql.Append("WanDuShu= null ,");
			}
			if (model.ShiDuShu != null)
			{
				strSql.Append("ShiDuShu='"+model.ShiDuShu+"',");
			}
			else
			{
				strSql.Append("ShiDuShu= null ,");
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
			strSql.Append("delete from YongDian ");
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
			strSql.Append("delete from YongDian ");
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
		public Maticsoft.Model.YongDian GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ID,YongHuID,Year,Yue,QiDuShu,ZongJia,DanJia,WanDuShu,ShiDuShu ");
			strSql.Append(" from YongDian ");
			strSql.Append(" where ID="+ID+"" );
			Maticsoft.Model.YongDian model=new Maticsoft.Model.YongDian();
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
		public Maticsoft.Model.YongDian DataRowToModel(DataRow row)
		{
			Maticsoft.Model.YongDian model=new Maticsoft.Model.YongDian();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["YongHuID"]!=null)
				{
					model.YongHuID=row["YongHuID"].ToString();
				}
				if(row["Year"]!=null)
				{
					model.Year=row["Year"].ToString();
				}
				if(row["Yue"]!=null)
				{
					model.Yue=row["Yue"].ToString();
				}
				if(row["QiDuShu"]!=null)
				{
					model.QiDuShu=row["QiDuShu"].ToString();
				}
				if(row["ZongJia"]!=null)
				{
					model.ZongJia=row["ZongJia"].ToString();
				}
				if(row["DanJia"]!=null)
				{
					model.DanJia=row["DanJia"].ToString();
				}
				if(row["WanDuShu"]!=null)
				{
					model.WanDuShu=row["WanDuShu"].ToString();
				}
				if(row["ShiDuShu"]!=null)
				{
					model.ShiDuShu=row["ShiDuShu"].ToString();
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
			strSql.Append("select ID,YongHuID,Year,Yue,QiDuShu,ZongJia,DanJia,WanDuShu,ShiDuShu ");
			strSql.Append(" FROM YongDian ");
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
			strSql.Append(" ID,YongHuID,Year,Yue,QiDuShu,ZongJia,DanJia,WanDuShu,ShiDuShu ");
			strSql.Append(" FROM YongDian ");
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
			strSql.Append("select count(1) FROM YongDian ");
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
			strSql.Append(")AS Row, T.*  from YongDian T ");
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

