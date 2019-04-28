using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// TingChe:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TingChe
	{
		public TingChe()
		{}
		#region Model
		private int _id;
		private string _ruchangshijian;
		private string _chuchangshijian;
		private string _chepai;
		private string _jine;
		private string _zongjishi;
		private string _shoufeixingshi;
		private string _zhuangtai;
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
		public string RuChangShiJian
		{
			set{ _ruchangshijian=value;}
			get{return _ruchangshijian;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChuChangShiJian
		{
			set{ _chuchangshijian=value;}
			get{return _chuchangshijian;}
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
		public string JinE
		{
			set{ _jine=value;}
			get{return _jine;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZongJiShi
		{
			set{ _zongjishi=value;}
			get{return _zongjishi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShouFeiXingShi
		{
			set{ _shoufeixingshi=value;}
			get{return _shoufeixingshi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhuangTai
		{
			set{ _zhuangtai=value;}
			get{return _zhuangtai;}
		}
		#endregion Model

	}
}

