using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

using Mail_Test.Mail;
using Mail_Test.BLL;

namespace MvcMail.App_Code
{
    public static class Config
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public static EmailType TestEmailType = (EmailType)Maticsoft.Common.ConfigHelper.GetConfigInt("TestEmailType");
        public static string TestUserName = Maticsoft.Common.ConfigHelper.GetConfigString("TestUserName");
        public static string TestPassword = Maticsoft.Common.ConfigHelper.GetConfigString("TestPassword");

        /// <summary>
        /// 测试发送地址
        /// </summary>
        public static string TestFromAddress = Maticsoft.Common.ConfigHelper.GetConfigString("TestFromAddress");
        public static string TestFromName = Maticsoft.Common.ConfigHelper.GetConfigString("TestFromName");
        
    

        /// <summary>
        /// 测试收件地址  
        /// </summary>
        public static string TestToAddress = "";

        private static Dictionary<string, string> m_dicNameMap = null;
        /// <summary>
        /// 用于获取显示名称
        /// </summary>
        private static Dictionary<string, string> DicNameMap
        {
            get
            {
                if (m_dicNameMap == null)
                {
                    m_dicNameMap = new Dictionary<string, string>();

                    DataSet ds = new member().GetList("1=1");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        m_dicNameMap.Add(dr["useremail"].ToString(), dr["username"].ToString());
                    }                    
                }
                return m_dicNameMap;
            }
        }

        /// <summary>
        /// 获取邮件地址对应的显示名称
        /// </summary>
        /// <param name="address">邮件地址</param>
        /// <returns>邮件显示名称</returns>
        public static string GetAddressName(string address)
        {
            if (DicNameMap.ContainsKey(address))
                return DicNameMap[address];
            else
                return String.Empty;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="_files">文件列表</param>
        /// <param name="webFilePath">保存目录</param>
        /// <returns></returns>
        public static ArrayList uploadfile(HttpFileCollection _files,string webFilePath )
        {
            ArrayList list = new ArrayList();
            int flag = _files.Count;            
            for (int i = 0; i < _files.Count; i++)
            {
                string name = _files[i].FileName;              
                try
                {
                    if (!Directory.Exists(webFilePath))
                    {
                        Directory.CreateDirectory(webFilePath);
                    }

                    FileInfo fi = new FileInfo(name);
                    string filename = webFilePath + fi.Name;

                    _files[i].SaveAs(filename);
                    list.Add(filename);
                }
                catch (Exception ex)
                {
                   
                }
            }
            return list;
        }

        /// <summary>
        /// 是否Email
        /// </summary>
        public static bool IsEmailString(string strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return objAlphaNumericPattern.IsMatch(strToCheck);
        }
    }
}