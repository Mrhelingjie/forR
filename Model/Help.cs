using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// Help:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Help
	{
		public Help()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _shuoming;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShuoMing
		{
			set{ _shuoming=value;}
			get{return _shuoming;}
		}
		#endregion Model

	}
}

