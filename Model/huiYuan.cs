using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// huiYuan:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class huiYuan
	{
		public huiYuan()
		{}
		#region Model
		private int _id;
		private string _chepai;
		private string _phone;
		private string _name;
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
		public string ChePai
		{
			set{ _chepai=value;}
			get{return _chepai;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		#endregion Model

	}
}

