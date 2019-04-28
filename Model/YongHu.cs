using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// YongHu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YongHu
	{
		public YongHu()
		{}
		#region Model
		private int _id;
		private string _number;
		private string _name;
		private string _address;
		private string _phone;
		private string _biaohao;
		private string _idcard;
		private string _xiaoqu;
		private string _danyuanhao;
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
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
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
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
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
		public string BiaoHao
		{
			set{ _biaohao=value;}
			get{return _biaohao;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IdCard
		{
			set{ _idcard=value;}
			get{return _idcard;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XiaoQu
		{
			set{ _xiaoqu=value;}
			get{return _xiaoqu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DanYuanHao
		{
			set{ _danyuanhao=value;}
			get{return _danyuanhao;}
		}
		#endregion Model

	}
}

