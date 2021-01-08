using Castle.DynamicProxy;
using Custom.lib.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Custom.lib.UnitofWork
{
    /// <summary>
    /// 默认工作单元拦截器，抽象可重写
    /// </summary>
    public abstract class UnitofWorkInterceptBase : CustomInterceptBase
    {
        private readonly DbContext _context;

        protected UnitofWorkInterceptBase(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 同步拦截处理
        /// </summary>
        /// <param name="invocation"></param>
        protected override void SynchronousIntercept(IInvocation invocation)
        {
            bool isOpen = TransactionOpen(invocation);
            if (!isOpen)
            {
                invocation.Proceed();
            }
            else
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    invocation.Proceed();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 是否开启事务
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private bool TransactionOpen(IInvocation invocation)
        {
            var classAttributes = invocation.TargetType.GetCustomAttributes(true);
            var methodAttributes = invocation.MethodInvocationTarget.GetCustomAttributes(true);
            bool isOpen = false;
            if (classAttributes.Any(a => a.GetType() == typeof(UnitofWorkAttribute)))
            {
                isOpen = classAttributes.All(a => ((UnitofWorkAttribute)a).Transaction);
            }
            if (methodAttributes.Any(a => a.GetType() == typeof(UnitofWorkAttribute)))
            {
                isOpen = methodAttributes.All(a => ((UnitofWorkAttribute)a).Transaction);
            }
            return isOpen;
        }

    }

}
