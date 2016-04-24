
using System;
namespace Mail_Test.Model
{
	/// <summary>
	/// fjlist:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class fjlist
	{
		public fjlist()
		{}
		#region Model
		private int _id;
		private int _mailid;
		private string _filename;
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
		public int mailid
		{
			set{ _mailid=value;}
			get{return _mailid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filename
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		#endregion Model

	}
}

