using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects
{
    public class MailMessage
    {
        public string Encoding = "utf-8";
        public List<MailAccount> Tos { get; set; }
        public List<MailAccount> CCs { get; set; }
        public MailAccount From { get; set; }
        public bool IsHtml { get; set; }
        public List<string> Attachment { get; set; }
        public string MailContent{get;set;}
        public string Subject { get; set; }
        public MailAccount DisplayFrom { get; set; }
        public List<MailAccount> Mccs { get; set; }
        public System.Net.Mail.MailPriority priority = System.Net.Mail.MailPriority.Normal;
        public MailMessage()
        {
            Tos = new List<MailAccount>();
            CCs = new List<MailAccount>();
            Attachment = new List<string>();
            Mccs = new List<MailAccount>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">发件人</param>
        /// <param name="to">收件人</param>
        /// <param name="content">邮件内容</param>
        public MailMessage(MailAccount from, MailAccount to, string content):this()
        {
            this.From = from;
            this.Tos.Add(to);
            this.MailContent = content;
        }
        /// <summary>
        /// 添加收件人
        /// </summary>
        /// <param name="to">收件人</param>
        public void AddTo(MailAccount to)
        {
            this.Tos.Add(to);
        }
        /// <summary>
        /// 添加抄送
        /// </summary>
        /// <param name="cc"></param>
        public void AddCC(MailAccount cc)
        {
            this.CCs.Add(cc);
        }
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void AddAttachment(string fileName)
        {
            this.Attachment.Add(fileName);
        }
        /// <summary>
        /// 添加密送人
        /// </summary>
        /// <param name="cc"></param>
        public void AddMCC(MailAccount cc)
        {
            this.CCs.Add(cc);
        }
    }
}

