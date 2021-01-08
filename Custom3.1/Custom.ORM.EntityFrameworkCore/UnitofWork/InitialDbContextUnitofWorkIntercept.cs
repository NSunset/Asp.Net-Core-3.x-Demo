using Custom.lib.IOC;
using Custom.lib.UnitofWork;
using Custom.ORM.EntityFrameworkCore.Db;

namespace Custom.ORM.EntityFrameworkCore.UnitofWork
{
    /// <summary>
    /// 工作单元拦截器。拦截InitialDbContext
    /// </summary>
    public class InitialDbContextUnitofWorkIntercept : UnitofWorkInterceptBase, IScope, IDependencyInterfaceIgnore
    {
        private readonly InitialDbContext _context;

        public InitialDbContextUnitofWorkIntercept(InitialDbContext context) : base(context)
        {
            _context = context;
        }
    }



    /// <summary>
    /// 异步工作单元拦截器。拦截InitialDbContext
    /// </summary>
    public class InitialDbContextUnitofWorkAsyncIntercept : UnitofWorkInterceptAsyncBase, IScope, IDependencyInterfaceIgnore
    {
        private readonly InitialDbContext _context;

        public InitialDbContextUnitofWorkAsyncIntercept(InitialDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
