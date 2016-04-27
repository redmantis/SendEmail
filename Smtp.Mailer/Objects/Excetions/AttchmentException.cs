using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects.Excetions
{
    public class AttchmentException:ApplicationException
    {
        public AttchmentException(string message)
            : base(message)
        { 
        }
    }
}
