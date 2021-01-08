using Castle.DynamicProxy;
using Custom.lib.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Custom.lib.UnitofWork
{
    /// <summary>
    /// 默认工作单元异步拦截器，抽象可重写
    /// </summary>
    public abstract class UnitofWorkInterceptAsyncBase : CustomInterceptAsyncBase
    {
        private readonly DbContext _context;
        protected UnitofWorkInterceptAsyncBase(DbContext context)
        {
            _context = context;
        }

        protected override void InternalInterceptSynchronous(IInvocation invocation)
        {
            bool isOpen = TransactionOpen(invocation);
            try
            {
                if (!isOpen)
                {
                    invocation.Proceed();
                }
                else
                {
                    using var transaction = _context.Database.BeginTransaction();

                    invocation.Proceed();
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected override async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            bool isOpen = TransactionOpen(invocation);
            try
            {
                TResult result;
                if (!isOpen)
                {
                    invocation.Proceed();
                    var task = (Task<TResult>)invocation.ReturnValue;
                    result = await task;
                }
                else
                {
                    using var transaction = _context.Database.BeginTransaction();

                    invocation.Proceed();
                    var task = (Task<TResult>)invocation.ReturnValue;
                    result = await task;
                    transaction.Commit();
                }
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected override async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            bool isOpen = TransactionOpen(invocation);
            try
            {
                if (!isOpen)
                {
                    invocation.Proceed();
                    var task = (Task)invocation.ReturnValue;
                    await task;
                }
                else
                {
                    using var transaction = _context.Database.BeginTransaction();

                    invocation.Proceed();
                    var task = (Task)invocation.ReturnValue;
                    await task;
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                throw;
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
                isOpen = classAttributes.Where(a => typeof(UnitofWorkAttribute).IsAssignableFrom(a.GetType())).All(a => ((UnitofWorkAttribute)a).Transaction);
            }
            if (methodAttributes.Any(a => a.GetType() == typeof(UnitofWorkAttribute)))
            {
                isOpen = methodAttributes.Where(a=>typeof(UnitofWorkAttribute).IsAssignableFrom(a.GetType())).All(a => ((UnitofWorkAttribute)a).Transaction);
            }
            return isOpen;
        }
    }
}
