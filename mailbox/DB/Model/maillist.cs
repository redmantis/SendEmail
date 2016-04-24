using System;
namespace Mail_Test.Model
{
	/// <summary>
	/// maillist:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class maillist
	{
		public maillist()
		{}
		#region Model
		private int _id;
		private string _title;
		private string _mailcontent;
		private string _mto;
		private string _mcc;
		private string _mfrom;
		private string _toname;
		private string _ccname;
		private DateTime _sendtime;
		private string _fromname;
		private Guid _guid;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mailcontent
		{
			set{ _mailcontent=value;}
			get{return _mailcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mto
		{
			set{ _mto=value;}
			get{return _mto;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mcc
		{
			set{ _mcc=value;}
			get{return _mcc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mfrom
		{
			set{ _mfrom=value;}
			get{return _mfrom;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string toname
		{
			set{ _toname=value;}
			get{return _toname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ccname
		{
			set{ _ccname=value;}
			get{return _ccname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime sendtime
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fromname
		{
			set{ _fromname=value;}
			get{return _fromname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid guid
		{
			set{ _guid=value;}
			get{return _guid;}
		}
		#endregion Model

	}
}

