/** sendlist.cs
*
* 功 能： N/A
* 类 名： sendlist
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/4/22 18:35:32   N/A    初版
*
* Copyright (c) 2016 Redmantis. All rights reserved.
*/
using System;
namespace Mail_Test.Model
{
	/// <summary>
	/// sendlist:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sendlist
	{
		public sendlist()
		{}
		#region Model
		private int _userid;
		private int _mailid;
		private bool _issend= false;
		private DateTime _sendtime= DateTime.Now;
		private int _id;
		/// <summary>
		/// 
		/// </summary>
		public int userid
		{
			set{ _userid=value;}
			get{return _userid;}
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
		public bool issend
		{
			set{ _issend=value;}
			get{return _issend;}
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
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		#endregion Model

	}
}

