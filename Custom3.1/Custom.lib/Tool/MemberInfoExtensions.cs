using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.lib.Tool
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// 是否包含指定类型
        /// </summary>
        /// <param name="types"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAny(this Type[] types, Type type)
        {
            return types.Any(a => a == type);
        }
    }
}
