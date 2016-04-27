using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects
{
    /// <summary>
    /// SMTP 命令
    /// </summary>
    public class SmtpCommand
    {
        public struct CommandCount
        {
            public int count;
            public string command;
        }
        private const string HELLO = "HELO {0}"; //打招呼
        private const string EHELLO = "EHLO {0}";//验证用户身份招呼
        private const string LOGIN = "AUTH LOGIN";//验证用户身份
        private const string MAILFROM = "MAIL FROM:<{0}>";//发件人
        private const string RECP = "RCPT TO:<{0}>";//收件人
        private const string DATA = "DATA";//邮件内容
        private const string END_DATA = ".";//结束
        private const string QUIT = "QUIT";//退出
        public static string GetHelloCommand(string name, bool isAuth)
        {
            if (isAuth)
                return string.Format(EHELLO,name);
            return string.Format(HELLO, name);
        }
        public static string GetLoginCommand()
        {
            return LOGIN;
        }
        public static string GetMailFromCommand(string email)
        {
            return string.Format(MAILFROM, email);
        }
        public static string GetMailToCommand(string email)
        {
            return string.Format(RECP,email);
        }
        public static string GetDataCommand()
        { 
            return DATA;
        }
        public static string GetQuitCommand()
        {
            return QUIT;
        }
        public static string GetEndDataCommand()
        {
            return END_DATA;
        }
    }
}
