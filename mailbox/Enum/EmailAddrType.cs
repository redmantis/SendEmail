//======================================================================
//        开源日期:           2013/09/01
//            作者:           何雨泉    
//            博客：          http://www.cnblogs.com/heyuquan/
//            版本:           0.0.0.1
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mail_Test.Mail
{
    /// <summary>
    /// 接收邮件地址类型
    /// </summary>
    public enum EmailAddrType
    {
        /// <summary>
        /// 发件人
        /// </summary>
        From = 0,
        /// <summary>
        /// 收件人
        /// </summary>
        To = 2,
        /// <summary>
        /// 抄送人
        /// </summary>
        CC = 4,
        /// <summary>
        /// 密送人
        /// </summary>
        Bcc = 8,
    }
}
