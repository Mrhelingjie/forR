using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//�����������
using Maticsoft.DAL;
namespace Maticsoft.DAL
{
	/// <summary>
	/// ���ݷ�����ActorFunction��
	/// </summary>
	public class ActorFunction
	{
		public ActorFunction()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("FunctionId", "ActorFunction"); 
		}


		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int FunctionId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ActorFunction");
			strSql.Append(" where FunctionId="+FunctionId+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

        ///// <summary>
        ///// ����һ������
        ///// </summary>
        //public int Add(Maticsoft.Model.ActorFunction model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    StringBuilder strSql1=new StringBuilder();
        //    StringBuilder strSql2=new StringBuilder();
        //    if (model.ActorId != null)
        //    {
        //        strSql1.Append("ActorId,");
        //        strSql2.Append(""+model.ActorId+",");
        //    }
        //    if (model.Type != null)
        //    {
        //        strSql1.Append("Type,");
        //        strSql2.Append("'"+model.Type+"',");
        //    }
        //    if (model.Content != null)
        //    {
        //        strSql1.Append("Content,");
        //        strSql2.Append("'"+model.Content+"',");
        //    }
        //    strSql.Append("insert into ActorFunction(");
        //    strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
        //    strSql.Append(")");
        //    strSql.Append(" values (");
        //    strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
        //    strSql.Append(")");
        //    strSql.Append(";select @@IDENTITY");
        //    object obj = DbHelperSQL.GetSingle(strSql.ToString());
        //    if (obj == null)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //}

        ///// <summary>
        ///// ����һ������
        ///// </summary>
        //public void Update(Maticsoft.Model.ActorFunction model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("update ActorFunction set ");
        //    if (model.ActorId != null)
        //    {
        //        strSql.Append("ActorId="+model.ActorId+",");
        //    }
        //    if (model.Type != null)
        //    {
        //        strSql.Append("Type='"+model.Type+"',");
        //    }
        //    if (model.Content != null)
        //    {
        //        strSql.Append("Content='"+model.Content+"',");
        //    }
        //    int n = strSql.ToString().LastIndexOf(",");
        //    strSql.Remove(n, 1);
        //    strSql.Append(" where FunctionId="+ model.FunctionId+" ");
        //    DbHelperSQL.ExecuteSql(strSql.ToString());
        //}

        ///// <summary>
        ///// ɾ��һ������
        ///// </summary>
        //public void Delete(int FunctionId)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from ActorFunction ");
        //    strSql.Append(" where FunctionId="+FunctionId+" " );
        //    DbHelperSQL.ExecuteSql(strSql.ToString());
        //}

        ///// <summary>
        ///// �õ�һ������ʵ��
        ///// </summary>
        //public Maticsoft.Model.ActorFunction GetModel(int FunctionId)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1  ");
        //    strSql.Append(" FunctionId,ActorId,Type,Content ");
        //    strSql.Append(" from ActorFunction ");
        //    strSql.Append(" where FunctionId="+FunctionId+" " );
        //    Maticsoft.Model.ActorFunction model=new Maticsoft.Model.ActorFunction();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString());
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["FunctionId"].ToString()!="")
        //        {
        //            model.FunctionId=int.Parse(ds.Tables[0].Rows[0]["FunctionId"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["ActorId"].ToString()!="")
        //        {
        //            model.ActorId=int.Parse(ds.Tables[0].Rows[0]["ActorId"].ToString());
        //        }
        //        model.Type=ds.Tables[0].Rows[0]["Type"].ToString();
        //        model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        ///// <summary>
        ///// ��������б�
        ///// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select FunctionId,ActorId,Type,Content ");
        //    strSql.Append(" FROM ActorFunction ");
        //    if(strWhere.Trim()!="")
        //    {
        //        strSql.Append(" where "+strWhere);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        ///// <summary>
        ///// ���ǰ��������
        ///// </summary>
        //public DataSet GetList(int Top,string strWhere,string filedOrder)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select ");
        //    if(Top>0)
        //    {
        //        strSql.Append(" top "+Top.ToString());
        //    }
        //    strSql.Append(" FunctionId,ActorId,Type,Content ");
        //    strSql.Append(" FROM ActorFunction ");
        //    if(strWhere.Trim()!="")
        //    {
        //        strSql.Append(" where "+strWhere);
        //    }
        //    strSql.Append(" order by " + filedOrder);
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        ///*
        //*/

		#endregion  ��Ա����
	}
}

