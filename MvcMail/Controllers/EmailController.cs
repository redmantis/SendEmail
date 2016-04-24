using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections;
using System.ComponentModel;
using System.Net.Mail;
using System.IO;
using System.Data;
using Mail_Test.Mail;
using Mail_Test.BLL;
using MvcMail.App_Code;
using System.Text;

namespace MvcMail.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";
            ViewBag.namefrom = Config.TestFromName;
            ViewBag.mailfrom = Config.TestFromAddress;   
            return View(new member().GetAllList());
        }

        [HttpPost]
        public ActionResult Sendmail()
        {
            bool isAsync = false;
            string idlist = Request.Form["selmember"].ToString();
            idlist = idlist.Replace("false", "0");

            MailHelper mail = new MailHelper(isAsync);        

            /*保存邮件信息*/
            Mail_Test.Model.maillist mailinfo = new Mail_Test.Model.maillist();            
            mailinfo.mcc = Request.Form["txtCc"].ToString();
            mailinfo.ccname = Request.Form["txtCcname"].ToString();
            mailinfo.fromname = Request.Form["txtfromname"].ToString();
            mailinfo.mailcontent = Request.Form["txtContent"].ToString();
            mailinfo.mfrom = Request.Form["txtfrom"].ToString();
            mailinfo.mto = Request.Form["txtTo"].ToString();
            mailinfo.title = Request.Form["txtTitle"].ToString();
            mailinfo.toname = Request.Form["txtToname"].ToString();
            mailinfo.sendtime = DateTime.Now;
            int mailid = new Mail_Test.BLL.maillist().Add(mailinfo);

            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads\\" + mailid.ToString() + "\\";
            ArrayList list = Config.uploadfile(System.Web.HttpContext.Current.Request.Files, path);

            savefj(list, mailid);

            if (Config.IsEmailString(mailinfo.mto))
            {
                this.SendMessage(mail, false, list, mailinfo, true, true);
            }

            //保存收件人信息
            DataSet ds = getmemberlist(idlist);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Mail_Test.Model.sendlist model = new Mail_Test.Model.sendlist();
                model.mailid = mailid;
                model.userid = int.Parse(dr["id"].ToString());
                model.issend = false;
                model.sendtime = DateTime.Now;
                new sendlist().Add(model);
            }


         

            foreach (DataRow dr in ds.Tables[0].Rows)
            {              
                mailinfo.mto = dr["useremail"].ToString();
                mailinfo.toname = dr["username"].ToString();
                this.SendMessage(mail, false, list, mailinfo, true, true);

                Mail_Test.Model.sendlist model = new Mail_Test.Model.sendlist();
                model.mailid = mailid;
                model.userid = int.Parse(dr["id"].ToString());
                model.issend = true;
                model.sendtime = DateTime.Now;
                new sendlist().Update(model);

            }
         
            ViewBag.msg = "邮件发送完成"; ;            

            /* bool isAsync = false;

             long count = 5;//发送数量

             MailHelper mail = new MailHelper(isAsync);
             string path = AppDomain.CurrentDomain.BaseDirectory + "uploads\\";
             ArrayList list = Config.uploadfile(System.Web.HttpContext.Current.Request.Files, path);
             Mail_Test.Model.maillist mailinfo = new Mail_Test.Model.maillist();

             mailinfo.mcc = Request.Form["txtCc"].ToString();
             mailinfo.ccname = Request.Form["txtCcname"].ToString();
             mailinfo.fromname= Request.Form["txtfromname"].ToString();
             mailinfo.mailcontent = Request.Form["txtContent"].ToString();
             mailinfo.mfrom = Request.Form["txtfrom"].ToString();
             mailinfo.mto = Request.Form["txtTo"].ToString();
             mailinfo.title = Request.Form["txtTitle"].ToString();
             mailinfo.toname = Request.Form["txtToname"].ToString();

             for (long i = 1; i <= count; i++)
             {
                 this.SendMessage(mail, false, list, mailinfo, true, true);
             }
             mail.SetBatchMailCount(count);
          */
            return View();
        }

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="isSimple">是否只发送一条</param>
        /// <param name="autoReleaseSmtp">是否自动释放SmtpClient</param>
        /// <param name="isReuse">是否重用SmtpClient</param>
        private void SendMessage(MailHelper mail, bool isSimple, ArrayList list, Mail_Test.Model.maillist mailinfo, bool autoReleaseSmtp, bool isReuse)
        {
            mail.IsBodyHtml = true;

            string bcc = "";//密送
            if (mailinfo.mto.Length > 0)
                mail.AddReceive(EmailAddrType.To, mailinfo.mto, mailinfo.toname);
            if (mailinfo.mcc.Length > 0)
                mail.AddReceive(EmailAddrType.CC, mailinfo.mcc, mailinfo.ccname);

            if (bcc.Length > 0)
                mail.AddReceive(EmailAddrType.Bcc, bcc, Config.GetAddressName(bcc));

            mail.Subject = mailinfo.title;

            // Guid.NewGuid() 防止重复内容，被SMTP服务器拒绝接收邮件
            mail.Body = mailinfo.mailcontent;
            mail.From = mailinfo.mfrom;
            mail.FromDisplayName = mailinfo.fromname;

            if (!isReuse || !mail.ExistsSmtpClient())
            {
                mail.SetSmtpClient(
                       new SmtpHelper(Config.TestEmailType, false, Config.TestUserName, Config.TestPassword).SmtpClient
                       , autoReleaseSmtp
                       );
            }

            if (list != null && list.Count > 0)
            {
                foreach (string filePath in list)
                {
                    mail.AddAttachment(filePath);
                }
            }


            Dictionary<MailInfoType, string> dic = mail.CheckSendMail();
            if (dic.Count > 0 && MailInfoHelper.ExistsError(dic))
            {
                // 反馈“错误+提示”信息
                ViewBag.msg += MailInfoHelper.GetMailInfoStr(dic);
            }
            else
            {
                string msg = String.Empty;
                if (dic.Count > 0)
                {
                    // 反馈“提示”信息
                    ViewBag.msg = MailInfoHelper.GetMailInfoStr(dic);
                }

                try
                {
                    if (isSimple)
                    {
                        mail.SendOneMail();
                    }
                    else
                    {
                        // 发送
                        mail.SendBatchMail();
                    }
                }
                catch (Exception ex)
                {
                    // 反馈异常信息
                    ViewBag.msg += (ex.InnerException == null ? ex.Message : ex.Message + ex.InnerException.Message) + Environment.NewLine;
                }
                finally
                {
                    // 输出到界面
                    if (msg.Length > 0)
                        ViewBag.msg += Environment.NewLine;
                }
            }

            mail.Reset();
        }

        /// <summary>
        /// 读取收件人信息列表
        /// </summary>
        /// <param name="idlsit"></param>
        /// <returns></returns>
        private DataSet getmemberlist(string idlsit)
        {
            DataSet ds = new member().GetList(" id in (" + idlsit + ")");
            return ds;
        }

        /// <summary>
        /// 存储附件信息
        /// </summary>
        /// <param name="flist"></param>
        /// <param name="mailid"></param>
        private void savefj(ArrayList flist,int mailid)
        {
            fjlist bf = new fjlist();
            foreach(string file in flist)
            {
                Mail_Test.Model.fjlist model = new Mail_Test.Model.fjlist();
                model.mailid = mailid;
                model.filename = file;
                bf.Add(model);
            }
        }

    }
}