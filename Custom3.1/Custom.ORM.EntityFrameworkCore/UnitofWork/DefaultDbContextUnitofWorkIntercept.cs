using Custom.lib.IOC;
using Custom.lib.UnitofWork;
using Custom.ORM.EntityFrameworkCore.Db;

namespace Custom.ORM.EntityFrameworkCore.UnitofWork
{
    /// <summary>
    /// 工作单元拦截器。拦截DefaultDbContext
    /// </summary>
    public class DefaultDbContextUnitofWorkIntercept : UnitofWorkInterceptBase, IScope, IDependencyInterfaceIgnore
    {
        private readonly DefaultDbContext _context;

        public DefaultDbContextUnitofWorkIntercept(DefaultDbContext context) : base(context)
        {
            _context = context;
        }
    }



    /// <summary>
    /// 异步工作单元拦截器。拦截DefaultDbContext
    /// </summary>
    public class DefaultDbContextUnitofWorkAsyncIntercept : UnitofWorkInterceptAsyncBase, IScope, IDependencyInterfaceIgnore
    {
        private readonly DefaultDbContext _context;

        public DefaultDbContextUnitofWorkAsyncIntercept(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
