using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smtp.Mailer.Objects.Exceptions
{
    public class SendInfoException:ApplicationException
    {
        public SendInfoException(string message)
            : base(message)
        { }
    }
}
