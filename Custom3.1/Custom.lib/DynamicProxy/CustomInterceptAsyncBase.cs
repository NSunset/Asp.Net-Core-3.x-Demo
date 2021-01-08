using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.lib.DynamicProxy
{
    /// <summary>
    /// 异步拦截器父类:只要有异步方法就用这个。
    /// </summary>
    public abstract class CustomInterceptAsyncBase : IAsyncInterceptor,IInterceptor
    {
        /// <summary>
        /// 异步拦截返回Task使用
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        protected abstract Task InternalInterceptAsynchronous(IInvocation invocation);

        /// <summary>
        /// 异步拦截返回Task<T>时使用
        /// </summary>
        /// <typeparam name="TResult">T返回内容</typeparam>
        /// <param name="invocation"></param>
        /// <returns></returns>
        protected abstract Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation);

        /// <summary>
        /// 同步拦截时使用
        /// </summary>
        /// <param name="invocation"></param>
        protected abstract void InternalInterceptSynchronous(IInvocation invocation);

        /// <summary>
        /// 异步拦截返回Task使用
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        /// <summary>
        /// 异步拦截返回Task<T>时使用
        /// </summary>
        /// <typeparam name="TResult">T返回内容</typeparam>
        /// <param name="invocation">拦截器</param>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }


        /// <summary>
        /// 同步方法拦截时使用
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptSynchronous(IInvocation invocation)
        {
            InternalInterceptSynchronous(invocation);
        }

        public void Intercept(IInvocation invocation)
        {
            this.ToInterceptor().Intercept(invocation);
        }
    }
}
