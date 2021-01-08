using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.lib.DbContextConfig
{
    public interface IDbContextSeedData
    {
        /// <summary>
        /// 添加基础数据
        /// </summary>
        void Initializer();
    }
}
