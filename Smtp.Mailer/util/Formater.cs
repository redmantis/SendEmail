using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smtp.Mailer.Objects;
namespace Smtp.Mailer.util
{
    /// <summary>
    /// 格式化
    /// </summary>
    public class Formater
    {
        /// <summary>
        /// 邮箱地址格式化
        /// </summary>
        /// <param name="address">邮箱账号</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string AddressFormat(MailAccount address, string encoding)
        {
            return string.Format("{0}<{1}>", ConvertToBase64String(address.Name, encoding), address.MailAddress);
        }
        /// <summary>
        /// 日期格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateFormat(DateTime date)
        {
            return string.Format("Date:{0}", date.ToUniversalTime().ToString("r"));
        }
        /// <summary>
        /// 主题
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SubjectFormat(string subject, string encoding)
        {
            return string.Format("Subject:{0}", ConvertToBase64String(subject, encoding));

        }
        /// <summary>
        /// 转换base 64
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertToBase64String(string value, string encoding)
        {
            byte[] subs = Encoding.Convert(Encoding.Default, Encoding.GetEncoding(encoding), Encoding.Default.GetBytes(value));
            return string.Format("=?{0}?B?{1}?=", encoding, Convert.ToBase64String(subs));
        }
        /// <summary>
        /// 优先级格式化
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static string PriorityFormat(System.Net.Mail.MailPriority priority)
        {
            int priority2 = 0;
            switch (priority)
            {
                case System.Net.Mail.MailPriority.High:
                    priority2 = 1;
                    break;
                case System.Net.Mail.MailPriority.Low:
                    priority2 = 5;
                    break;
                case System.Net.Mail.MailPriority.Normal:
                    priority2 = 3;
                    break;
                default:
                    priority2 = 3;
                    break;

            }
            return string.Format("X-Priority:{0}", priority2);
        }
        /// <summary>
        /// 邮件ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string MessageIdFormat(string id)
        {
            return string.Format("Message-Id:{0}", id);
        }
        /// <summary>
        /// 编码格式化
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncodingFormat(string encoding)
        {
            return string.Format("Content-Transfer-Encoding: {0}", encoding);
        }
        /// <summary>
        /// 发件人
        /// </summary>
        /// <param name="address"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string FormFormat(MailAccount address, string encoding)
        {
            return string.Format("From:{0}", AddressFormat(address, encoding));
        }
        /// <summary>
        /// 收件人
        /// </summary>
        /// <param name="address"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string TosFormat(List<MailAccount> address, string encoding)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < address.Count; i++)
            {
                sb.Append(AddressFormat(address[i], encoding));
                if (i != address.Count - 1)
                    sb.Append(",");
            }
            return string.Format("To:{0}", sb.ToString(), encoding);
        }

        /// <summary>
        /// 抄送
        /// </summary>
        /// <param name="address"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string CCsFormat(List<MailAccount> address, string encoding)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < address.Count; i++)
            {
                sb.Append(AddressFormat(address[i], encoding));
                if (i != address.Count - 1)
                    sb.Append(",");
            }
            return string.Format("Cc:{0}", sb.ToString());
        }
        /// <summary>
        /// 密送
        /// </summary>
        /// <param name="address"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string MCCsFormat(List<MailAccount> address, string encoding)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < address.Count; i++)
            {
                sb.Append(AddressFormat(address[i], encoding));
                if (i != address.Count - 1)
                    sb.Append(",");
            }
            return string.Format("MCc:{0}", sb.ToString());
        }
        /// <summary>
        /// 内容头
        /// </summary>
        /// <param name="isHtml"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string BodyHeaderFormat(bool isHtml, string encoding)
        {
            return string.Format("Content-Type:{0};charset=\"{1}\"", isHtml ? "text/html" : "text/plain", encoding);
        }

    }
}
