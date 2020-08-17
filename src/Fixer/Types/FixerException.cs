using System;
using System.Collections.Generic;
using System.Text;

namespace Fixer.Types
{

    public class FixerException : Exception
    {
        public string Code { get; }

        public FixerException()
        {
        }
        public FixerException(string code)
        {
            Code = code;
        }

        public FixerException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public FixerException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public FixerException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public FixerException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }

    }
}
