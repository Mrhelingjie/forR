using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// YongDian:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YongDian
	{
		public YongDian()
		{}
		#region Model
		private int _id;
		private string _yonghuid;
		private string _year;
		private string _yue;
		private string _qidushu;
		private string _zongjia;
		private string _danjia;
		private string _wandushu;
		private string _shidushu;
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
		public string YongHuID
		{
			set{ _yonghuid=value;}
			get{return _yonghuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Year
		{
			set{ _year=value;}
			get{return _year;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Yue
		{
			set{ _yue=value;}
			get{return _yue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QiDuShu
		{
			set{ _qidushu=value;}
			get{return _qidushu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZongJia
		{
			set{ _zongjia=value;}
			get{return _zongjia;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DanJia
		{
			set{ _danjia=value;}
			get{return _danjia;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WanDuShu
		{
			set{ _wandushu=value;}
			get{return _wandushu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShiDuShu
		{
			set{ _shidushu=value;}
			get{return _shidushu;}
		}
		#endregion Model

	}
}

