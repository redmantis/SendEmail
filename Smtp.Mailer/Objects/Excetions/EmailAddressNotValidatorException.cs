using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects.Excetions
{
    public sealed class EmailAddressNotValidatorException:ApplicationException
    {
        public EmailAddressNotValidatorException()
            : base("Email 地址无效")
        { 
        
        }
    }
}
