using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects.Excetions
{
    public sealed class InfoMissingException:ApplicationException
    {
        public InfoMissingException(string missingProperty):base(string.Format("缺少{0}的信息"))
        { 
        
        }
    }
}
