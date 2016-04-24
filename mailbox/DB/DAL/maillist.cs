using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Mail_Test.DAL
{
	/// <summary>
	/// 数据访问类:maillist
	/// </summary>
	public partial class maillist
	{
		public maillist()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "maillist"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from maillist");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Mail_Test.Model.maillist model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into maillist(");
			strSql.Append("title,mailcontent,mto,mcc,mfrom,toname,ccname,sendtime,fromname,guid)");
			strSql.Append(" values (");
			strSql.Append("@title,@mailcontent,@mto,@mcc,@mfrom,@toname,@ccname,@sendtime,@fromname,@guid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.VarChar,100),
					new SqlParameter("@mailcontent", SqlDbType.Text),
					new SqlParameter("@mto", SqlDbType.VarChar,100),
					new SqlParameter("@mcc", SqlDbType.VarChar,100),
					new SqlParameter("@mfrom", SqlDbType.VarChar,100),
					new SqlParameter("@toname", SqlDbType.VarChar,50),
					new SqlParameter("@ccname", SqlDbType.VarChar,50),
					new SqlParameter("@sendtime", SqlDbType.DateTime),
					new SqlParameter("@fromname", SqlDbType.VarChar,50),
					new SqlParameter("@guid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.title;
			parameters[1].Value = model.mailcontent;
			parameters[2].Value = model.mto;
			parameters[3].Value = model.mcc;
			parameters[4].Value = model.mfrom;
			parameters[5].Value = model.toname;
			parameters[6].Value = model.ccname;
			parameters[7].Value = model.sendtime;
			parameters[8].Value = model.fromname;
            parameters[9].Value = model.guid;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Mail_Test.Model.maillist model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update maillist set ");
			strSql.Append("title=@title,");
			strSql.Append("mailcontent=@mailcontent,");
			strSql.Append("mto=@mto,");
			strSql.Append("mcc=@mcc,");
			strSql.Append("mfrom=@mfrom,");
			strSql.Append("toname=@toname,");
			strSql.Append("ccname=@ccname,");
			strSql.Append("sendtime=@sendtime,");
			strSql.Append("fromname=@fromname,");
			strSql.Append("guid=@guid");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.VarChar,100),
					new SqlParameter("@mailcontent", SqlDbType.Text),
					new SqlParameter("@mto", SqlDbType.VarChar,100),
					new SqlParameter("@mcc", SqlDbType.VarChar,100),
					new SqlParameter("@mfrom", SqlDbType.VarChar,100),
					new SqlParameter("@toname", SqlDbType.VarChar,50),
					new SqlParameter("@ccname", SqlDbType.VarChar,50),
					new SqlParameter("@sendtime", SqlDbType.DateTime),
					new SqlParameter("@fromname", SqlDbType.VarChar,50),
					new SqlParameter("@guid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.title;
			parameters[1].Value = model.mailcontent;
			parameters[2].Value = model.mto;
			parameters[3].Value = model.mcc;
			parameters[4].Value = model.mfrom;
			parameters[5].Value = model.toname;
			parameters[6].Value = model.ccname;
			parameters[7].Value = model.sendtime;
			parameters[8].Value = model.fromname;
			parameters[9].Value = model.guid;
			parameters[10].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from maillist ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from maillist ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Mail_Test.Model.maillist GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,title,mailcontent,mto,mcc,mfrom,toname,ccname,sendtime,fromname,guid from maillist ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Mail_Test.Model.maillist model=new Mail_Test.Model.maillist();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


        public Mail_Test.Model.maillist GetModel(Guid guid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,mailcontent,mto,mcc,mfrom,toname,ccname,sendtime,fromname,guid from maillist ");
            strSql.Append(" where guid=@guid");
            SqlParameter[] parameters = {
                    new SqlParameter("@guid", SqlDbType.UniqueIdentifier,16)
            };
            parameters[0].Value = guid;

            Mail_Test.Model.maillist model = new Mail_Test.Model.maillist();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Mail_Test.Model.maillist DataRowToModel(DataRow row)
		{
			Mail_Test.Model.maillist model=new Mail_Test.Model.maillist();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["title"]!=null)
				{
					model.title=row["title"].ToString();
				}
				if(row["mailcontent"]!=null)
				{
					model.mailcontent=row["mailcontent"].ToString();
				}
				if(row["mto"]!=null)
				{
					model.mto=row["mto"].ToString();
				}
				if(row["mcc"]!=null)
				{
					model.mcc=row["mcc"].ToString();
				}
				if(row["mfrom"]!=null)
				{
					model.mfrom=row["mfrom"].ToString();
				}
				if(row["toname"]!=null)
				{
					model.toname=row["toname"].ToString();
				}
				if(row["ccname"]!=null)
				{
					model.ccname=row["ccname"].ToString();
				}
				if(row["sendtime"]!=null && row["sendtime"].ToString()!="")
				{
					model.sendtime=DateTime.Parse(row["sendtime"].ToString());
				}
				if(row["fromname"]!=null)
				{
					model.fromname=row["fromname"].ToString();
				}
				if(row["guid"]!=null && row["guid"].ToString()!="")
				{
					model.guid= new Guid(row["guid"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,title,mailcontent,mto,mcc,mfrom,toname,ccname,sendtime,fromname,guid ");
			strSql.Append(" FROM maillist ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,title,mailcontent,mto,mcc,mfrom,toname,ccname,sendtime,fromname,guid ");
			strSql.Append(" FROM maillist ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM maillist ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from maillist T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "maillist";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

