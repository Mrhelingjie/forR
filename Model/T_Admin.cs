using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Admin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Admin
	{
		public T_Admin()
		{}
		#region Model
		private int _admin_id;
		private string _login_name;
		private string _password;
		private string _note;
		private int _level=0;
		private string _rights;
		private bool _lock= false;
		private string _areas;
		private string _usertype;
		private string _dept;
		private string _idcard;
		private string _address;
		private string _remark;
		private string _realname;
		private string _sex;
		private string _telephone;
		/// <summary>
		/// 
		/// </summary>
		public int Admin_Id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Login_Name
		{
			set{ _login_name=value;}
			get{return _login_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Note
		{
			set{ _note=value;}
			get{return _note;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Level
		{
			set{ _level=value;}
			get{return _level;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rights
		{
			set{ _rights=value;}
			get{return _rights;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Lock
		{
			set{ _lock=value;}
			get{return _lock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Areas
		{
			set{ _areas=value;}
			get{return _areas;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dept
		{
			set{ _dept=value;}
			get{return _dept;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IDCard
		{
			set{ _idcard=value;}
			get{return _idcard;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddRess
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		#endregion Model

	}
}

