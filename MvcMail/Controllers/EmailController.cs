using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections;
using System.Data;
using System.Net.Mail;
using Mail_Test.Mail;
using Mail_Test.BLL;
using MvcMail.App_Code;
using System.Text;
//using Smtp.Mailer;
//using Smtp.Mailer.Objects;

namespace MvcMail.Controllers
{
    public class EmailController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";
            ViewBag.namefrom = Config.TestFromName;
            ViewBag.mailfrom = Config.TestFromAddress;
            ViewBag.guid = Guid.NewGuid();
            return View(new member().GetAllList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Sendmail()
        {

            bool isAsync = false;
            string idlist = "";
            if (Request.Form["selmember"] != null)
            {
                idlist = Request.Form["selmember"].ToString();
            }
            idlist = idlist.Replace("false", "0");


            MailHelper mail = new MailHelper(isAsync);


            //保存邮件信息
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
            mailinfo.guid = new Guid(Request.Form["guid"].ToString());
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
            JsonResult jsr = new JsonResult();
            jsr.Data = "邮件发送完成";
            return jsr;
        }

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="isSimple">是否只发送一条</param>
        /// <param name="autoReleaseSmtp">是否自动释放SmtpClient</param>
        /// <param name="isReuse">是否重用SmtpClient</param>
        private void SendMessage(MailHelper mail, bool isSimple, ArrayList list, Mail_Test.Model.maillist mailinfo, bool autoReleaseSmtp, bool isReuse)
        {

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.qq.com";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("179911014@qq.com", "cjwmmxfkvcnhbgja");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("179911014@qq.com");
            msg.To.Add(new MailAddress("feiyufly001@gmail.com"));
            //msg.Attachment.Add(@"c:\测试.TXT");
            //msg.Attachment.Add(@"c:\screenshot.jpg");
            //msg.Attachment.Add(@"c:\www.shengys.cn.zip");

            if (list != null && list.Count > 0)
            {
                foreach (string filePath in list)
                {
                    msg.Attachments.Add(new Attachment(filePath));
                }
            }

            msg.Subject = "This is a test Email subject";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>Test HTML Email</b></body>");

            try
            {
                client.Send(msg);
               // lblMsg.Text = "Your message has been successfully sent.";
            }
            catch (Exception ex)
            {
                // lblMsg.ForeColor = Color.Red;
                // lblMsg.Text = "Error occured while sending your message." + ex.Message;
                string x = ex.ToString();
            }


            //SmtpMailClient client = new SmtpMailClient("stmp.163.com", 25, true, Config.TestUserName, Config.TestPassword);
            //client.Timeout = 18000;
            //MailMessage msg = new MailMessage();
            //msg.From = new MailAccount(Config.TestFromAddress, Config.TestFromName);
            //msg.AddTo(new MailAccount(mailinfo.mto, mailinfo.toname));
            //msg.AddCC(new MailAccount(mailinfo.mcc, mailinfo.ccname));
            //msg.MailContent = mailinfo.mailcontent;
            //msg.Subject = mailinfo.title;
            //msg.IsHtml = true;
            //client.Send(msg);

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
        private void savefj(ArrayList flist, int mailid)
        {
            fjlist bf = new fjlist();
            foreach (string file in flist)
            {
                Mail_Test.Model.fjlist model = new Mail_Test.Model.fjlist();
                model.mailid = mailid;
                model.filename = file;
                bf.Add(model);
            }
        }

        [HttpPost]
        public JsonResult GetIsSendList(Guid mailid)
        {
            ArrayList list = new ArrayList();
            StringBuilder s = new StringBuilder();
            Mail_Test.Model.maillist m = new maillist().GetModel(mailid);
            if (m != null)
            {
                DataSet ds = new sendlist().GetList(" issend=1 and mailid = " + m.id.ToString());
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    s.Append(dr["userid"].ToString());
                    s.Append(",");
                }
            }
            JsonResult jsr = new JsonResult();
            jsr.Data = s.ToString();
            return jsr;
        }

    }
}