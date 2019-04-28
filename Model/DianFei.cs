using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DianFei:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DianFei
	{
		public DianFei()
		{}
		#region Model
		private int _id;
		private string _price;
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
		public string Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		#endregion Model

	}
}

