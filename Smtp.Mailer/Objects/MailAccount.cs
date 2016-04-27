using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects
{
    /// <summary>
    /// 邮箱用户,收件人和发件人
    /// </summary>
    public class MailAccount
    {
        public string Name { get; set; }
        public string MailAddress { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mailAddress">邮箱地址</param>
        public MailAccount(string mailAddress)
        {
            this.MailAddress = mailAddress;
            this.Name = mailAddress;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailAddress">邮箱地址</param>
        /// <param name="name">邮箱显示的名字</param>
        public MailAccount(string mailAddress, string name)
        {
            this.MailAddress = mailAddress;
            this.Name = name;
        }
    }
}
