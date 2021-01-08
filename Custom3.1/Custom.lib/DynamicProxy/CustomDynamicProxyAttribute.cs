using Castle.DynamicProxy;
using Custom.lib.Exceptions;
using System;

namespace Custom.lib.DynamicProxy
{
    /// <summary>
    /// 需要动态代理的类打上这个标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CustomDynamicProxyAttribute : Attribute
    {
        /// <summary>
        /// 拦截器
        /// </summary>
        public Type[] Interceptors { get; }

        /// <summary>
        /// 标记为动态拦截。
        /// </summary>
        /// <param name="types">具体的拦截器</param>
        public CustomDynamicProxyAttribute(Type[] types)
        {
            foreach (var type in types)
            {
                if (!typeof(IInterceptor).IsAssignableFrom(type))
                {
                    throw new CustomMessageException($"{nameof(Type)}必须实现拦截器类型IInterceptor");
                }
            }
            Interceptors = types;
        }

    }

}
