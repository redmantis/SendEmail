/** member.cs
*
* 功 能： N/A
* 类 名： member
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
	/// member:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class member
	{
		public member()
		{}
		#region Model
		private int _id;
		private string _username;
		private string _useremail;
		private int _usergroup=0;
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
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string useremail
		{
			set{ _useremail=value;}
			get{return _useremail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int usergroup
		{
			set{ _usergroup=value;}
			get{return _usergroup;}
		}
		#endregion Model

	}
}

