using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects
{
    public class MailCode
    {
        public const string OK = "250";
        public const string AUTH = "334";
        public const string AUTH_OK = "235";
        public const string DATA_READY = "354";
        public const string READY = "220";
        //string url="http://www.cnoffice.info/Program/CCC/CCCnetprogram/Program_56410.html";
        //static Dictionary<string, string> ErrorCode = new Dictionary<string, string>();
        //static Dictionary<string, string> OkCode = new Dictionary<string, string>();
        static MailCode()
        {
            //ErrorCode.Add("500", "邮箱地址错误");
            //ErrorCode.Add("501", "参数格式错误");
            //ErrorCode.Add("502", "命令不可实现");
            //ErrorCode.Add("503", "服务器需要SMTP验证");
            //ErrorCode.Add("504", "命令参数不可实现");
            //ErrorCode.Add("421", "服务未就绪，关闭传输信道");
            //ErrorCode.Add("450", "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）");
            //ErrorCode.Add("550", "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）");
            //ErrorCode.Add("451", "放弃要求的操作；处理过程中出错");
            //ErrorCode.Add("551", "用户非本地，请尝试<forward-path>");
            //ErrorCode.Add("452", "系统存储不足，要求的操作未执行");
            //ErrorCode.Add("552", "过量的存储分配，要求的操作未执行");
            //ErrorCode.Add("553", "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）");
            //ErrorCode.Add("432", "需要一个密码转换");
            //ErrorCode.Add("534", "认证机制过于简单");
            //ErrorCode.Add("538", "当前请求的认证机制需要加密");
            //ErrorCode.Add("454", "临时认证失败");
            //ErrorCode.Add("530", "需要认证");
            //OkCode.Add("220", "服务就绪");
            //OkCode.Add("250", "要求的邮件操作完成");
            //OkCode.Add("251", "用户非本地，将转发向<forward-path>");
            //OkCode.Add("354", "开始邮件输入，以<CRLF>.<CRLF>结束");
            //OkCode.Add("221", "服务关闭传输信道");
            //OkCode.Add("334", "服务器响应验证Base64字符串");
            //OkCode.Add("235", "验证成功");
        }
        //public static string GetMessage(string code)
        //{
        //    if (ErrorCode.Keys.Contains(code))
        //        return string.Format("{0}:{1}", code, ErrorCode[code]);
        //    if (OkCode.Keys.Contains(code))
        //        return string.Format("{0}:{1}", code, OkCode[code]);
        //    else
        //        return "没有此code";
        //}
        //public static bool IsSuccessful(string code)
        //{
        //    return OkCode.Keys.Contains(code);
        //}
    }
}
