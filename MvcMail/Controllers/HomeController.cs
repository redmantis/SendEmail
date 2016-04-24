using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using Maticsoft.DBUtility;//Please add references
using Mail_Test;
using Mail_Test.BLL;

namespace MvcMail.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select * ");
            //strSql.Append(" FROM sendlist ");
            //ViewBag.Message= DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows.Count;
            return View();
        }

        public ActionResult About()
        {

            for (int i = 0; i < 100; i++)
            {
                makemember(i);
            }

            member ml = new member();
            DataSet ds = ml.GetList("1=1");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ViewBag.msg += dr["useremail"].ToString() + " - " + dr["username"].ToString()+"<br />"; 
            }

            ViewBag.msg += ds.Tables[0].Rows.Count;

            return View();
        }

        [HttpPost]
        public ActionResult Sendmail()
        { 
            return View();
        }    


        private void makemember(int i)
        {
            Mail_Test.Model.member model = new Mail_Test.Model.member();
            model.usergroup = 0;
            model.username = GenerateRandomString(8);
            model.useremail = getmail(i);
            new Mail_Test.BLL.member().Add(model);          
        }

        private string getmail(int i)
        {
            ArrayList list = new ArrayList();
            list.Add("179911014@qq.com");
            list.Add("13355746451@163.com");
            list.Add("feiyufly001@hotmail.com");
            list.Add("feiyufly001@gmail.com");
            return list[i % 4].ToString();
        }

        private static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        /// <summary>  
        /// 生成0-z的随机字符串  
        /// </summary>  
        /// <param name="length">字符串长度</param>  
        /// <returns>随机字符串</returns>  
        public static string GenerateRandomString(int length)
        {
            string checkCode = String.Empty;
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                checkCode += constant[rd.Next(36)].ToString();
            }
            return checkCode;
        }
    }
}
