using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.lib.Exceptions
{
    public class CustomNullOrWhiteSpaceException : CustomMessageException
    {
        public CustomNullOrWhiteSpaceException(string name) : base($"{name}不能为null或空或仅由空格组成", 400)
        {

        }
    }
}
