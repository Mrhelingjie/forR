using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// CheWeiNumber:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CheWeiNumber
	{
		public CheWeiNumber()
		{}
		#region Model
		private int _id;
		private string _cheweishu;
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
		public string CheWeiShu
		{
			set{ _cheweishu=value;}
			get{return _cheweishu;}
		}
		#endregion Model

	}
}

