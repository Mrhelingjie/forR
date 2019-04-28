using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// ShouFeiType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShouFeiType
	{
		public ShouFeiType()
		{}
		#region Model
		private int _id;
		private string _leixing;
		private string _jine;
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
		public string LeiXing
		{
			set{ _leixing=value;}
			get{return _leixing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Jine
		{
			set{ _jine=value;}
			get{return _jine;}
		}
		#endregion Model

	}
}

