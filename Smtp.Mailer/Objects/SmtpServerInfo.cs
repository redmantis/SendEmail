using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects
{
    /// <summary>
    /// STMP 服务器信息
    /// </summary>
    public class SmtpServerInfo
    {
        public string SmtpServer { get; set; } //SMTP 服务器地址
        public int ServerPort = 25; //端口
        public string Account { get; set; } //用户名
        public string Password { get; set; }//密码
        public bool IsSendAuth { get; set; }//是否需要验证
        public bool EnableSsl { get; set; } //是否通过ssl 传输数据
        public SmtpServerInfo(string smtpServer)
        {
            this.SmtpServer = smtpServer;
        }
        public SmtpServerInfo(string smtpServer,int port)
        {

            this.SmtpServer = smtpServer;
            this.ServerPort = port;
        }
        public SmtpServerInfo(string smtpServer,int port,bool isSendAuth)
        {
            this.SmtpServer = smtpServer;
            this.ServerPort = port;
            this.IsSendAuth = isSendAuth;
        }
        public SmtpServerInfo(string smtpServer, int port, bool isSendAuth,string account,string password)
        {
            this.SmtpServer = smtpServer;
            this.ServerPort = port;
            this.IsSendAuth = isSendAuth;
            this.Account = account;
            this.Password = password;
        }
        
    }
}
