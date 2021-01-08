using Castle.DynamicProxy;

namespace Custom.lib.DynamicProxy
{
    /// <summary>
    /// 拦截器父类:都是同步方法用这个
    /// </summary>
    public abstract class CustomInterceptBase : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            SynchronousIntercept(invocation);
        }

        /// <summary>
        /// 同步拦截处理
        /// </summary>
        /// <param name="invocation"></param>
        protected abstract void SynchronousIntercept(IInvocation invocation);
    }

}
