using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.lib.UnitofWork
{
    /// <summary>
    /// 使用工作单元的话打上这个标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UnitofWorkAttribute : Attribute
    {
        /// <summary>
        /// 是否打开事务
        /// </summary>
        public bool Transaction { get; set; }
    }
}
