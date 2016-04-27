using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smtp.Mailer.Objects;
using System.IO;
using System.Collections;
namespace Smtp.Mailer.util
{
    public class MailerMethods
    {
        const string SP = "--mailboundary";//内容分隔符
        const string FILE_HEADER = "Content-Type: application/octet-stream;name=\"{0}\""; //文件头
        //const string CONTENT_ENCODING = "Content-Transfer-Encoding: {0}";  //内容编码
        const string FILE_DESCRIPTION = "Content-Disposition:attachment;filename=\"{0}\"";  //文件注解

        /// <summary>
        /// 附件发送
        /// </summary>
        /// <param name="attchments"></param>
        /// <param name="writer"></param>
        /// <param name="encoding"></param>
        public static void SendAttchments(List<string> attchments, Stream writer, string encoding)
        {
            foreach (string filePath in attchments)
            {
                byte[] fileDate;
                string fileName = "";
                //读取文件内容
                using (FileStream file = new FileStream(filePath,FileMode.Open))
                {

                    fileDate = new byte[Convert.ToInt32(file.Length)];
                    file.Read(fileDate, 0, fileDate.Length);
                    fileName = Path.GetFileName(filePath);
                }
                Writer(SP,encoding,writer);
                Writer(string.Format(FILE_HEADER, fileName),encoding,writer); //文件头
                Writer(Formater.EncodingFormat("base64"), encoding, writer); //编码

                Writer("\r\n", encoding, writer);
                Writer(Convert.ToBase64String(fileDate), encoding, writer); //文件内容
                Writer("", encoding, writer); //完成发送
            }
        }
        /// <summary>
        /// 内容发送
        /// </summary>
        /// <param name="body"></param>
        /// <param name="writer"></param>
        /// <param name="encoding"></param>
        /// <param name="isHtml"></param>
        public static void SendBody(string body, Stream writer, string encoding, bool isHtml)
        {




            Writer(Formater.BodyHeaderFormat(isHtml, encoding), encoding, writer);
            Writer(Formater.EncodingFormat("base64"),encoding,writer);
            Writer("\r\n", encoding, writer);
            
            Writer(Convert.ToBase64String(Encoding.GetEncoding(encoding).GetBytes(body)),encoding,writer);

            Writer("", encoding, writer);
        }
        /// <summary>
        /// 命令发送
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="writer"></param>
        /// <param name="encoding"></param>
        public static void SendCommand(string cmd, Stream writer,string encoding)
        {
            Writer(cmd, encoding, writer);

        }
        /// <summary>
        /// 读取响应信息
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public  static string ReaderCode(Stream sr)
        {
            byte[] by = new byte[255];
            int size=sr.Read(by, 0, by.Length);
            string message=null;
            if (size > 0)
            {
                message = Encoding.Default.GetString(by);
            }
            return message;
        }
        /// <summary>
        /// 邮件头发送
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="message"></param>
        public static void SendHeader(Stream writer, MailMessage message)
        {
            Writer(Formater.DateFormat(DateTime.Now), message.Encoding, writer); //日期
            Writer(Formater.FormFormat(message.DisplayFrom,message.Encoding), message.Encoding, writer); //发件人
            Writer(Formater.TosFormat(message.Tos,message.Encoding), message.Encoding, writer); //收件人
            Writer("REPLY-TO:"+message.DisplayFrom.MailAddress, message.Encoding, writer); //回复地址
            if (message.CCs.Count > 0)
            {
                Writer(Formater.CCsFormat(message.CCs,message.Encoding), message.Encoding, writer); //抄送
            }
            if (message.Mccs.Count > 0)
            {
                Writer(Formater.MCCsFormat(message.Mccs, message.Encoding), message.Encoding, writer);//密送
            }
            Writer(Formater.PriorityFormat(message.priority),message.Encoding,writer); //优先级
            Writer(Formater.SubjectFormat(message.Subject, message.Encoding), message.Encoding, writer); //标题
            Writer(Formater.EncodingFormat("base64"),message.Encoding,writer); //编码
            Writer("X-Mailer:DS Mail Sender V1.0",message.Encoding,writer); //发件人
            Writer(Formater.MessageIdFormat(System.Guid.NewGuid().ToString()), message.Encoding, writer); 
            Writer("MIME-Version:1.0", message.Encoding, writer); 
            Writer("Content-Type: multipart/mixed; boundary=\"mailboundary\"\r\n", message.Encoding, writer); //分隔符
            Writer(SP, message.Encoding, writer); //分隔符
        }
        private static void Writer(string data, string encoding, Stream writer)
        {
            int size = 255;
            byte[] datas = Encoding.Convert(Encoding.Default,Encoding.GetEncoding(encoding),Encoding.Default.GetBytes(string.Format("{0}\r\n",data)));
            if (datas.Length < size)
            {
                writer.Write(datas, 0, datas.Length);
            }
            else
            {
                for (int i = 0; i < datas.Length; i += size)
                {
                    writer.Write(datas, i, datas.Length - i < size ? datas.Length - i : size);
                }
            }
        }
        /// <summary>
        /// 结束符
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="encoding"></param>
        public static void WriterEnd(Stream writer,string encoding)
        {
            Writer("", encoding, writer);
            Writer("--mailboundary--", encoding, writer);
            SendCommand(SmtpCommand.GetEndDataCommand(), writer, encoding);
        }
    }
}
