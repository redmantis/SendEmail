using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
namespace Smtp.Mailer.util
{
    public static class CommonValidator
    {
        const string email = "^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$";
        public static bool CheckEmail(string emailAddress)
        {
            if (emailAddress == null || emailAddress.Length < 1)
                return false;
            return Regex.IsMatch(emailAddress, email);
        }
        public static bool CheckFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            try
            {
                return File.Exists(fileName);
            }
            catch
            {
                return false;
            }
        }
    }
}
