using Custom.lib.IOC;
using Custom.IApplication;
using System;
using Custom.lib.DynamicProxy;
using Custom.ORM.EntityFrameworkCore.UnitofWork;

namespace Custom.Application
{
    [CustomDynamicProxy(new Type[] { typeof(DefaultDbContextUnitofWorkAsyncIntercept), typeof(InitialDbContextUnitofWorkAsyncIntercept) })]
    public abstract class ApplicationBase : IApplicationBase, IScope
    {
    }
}
