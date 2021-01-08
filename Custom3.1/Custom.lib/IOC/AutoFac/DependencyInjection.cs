using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Castle.DynamicProxy;
using Custom.lib.Exceptions;
using Custom.lib.Reflection;
using Custom.lib.Tool;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.DynamicProxy;
using Custom.lib.DynamicProxy;

namespace Custom.lib.IOC.AutoFac
{
    public static class DependencyInjection
    {

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="builder">ioc容器</param>
        /// <param name="types">要注册的类型</param>
        /// <param name="serviceLifetime">生命周期</param>
        private static void RegisterTypes(ContainerBuilder builder, IEnumerable<Type> types, ServiceLifetime serviceLifetime, ProxyGenerator proxyGenerator)
        {
            if (!types.Any())
            {
                return;
            }
            var group = types.Select(x => new
            {
                AsSelf = x.GetInterfaces().IsAny(typeof(IDependencyInterfaceIgnore)),
                IsGenericType = x.IsGenericType,
                Type = x,
                isDynamicProxy = x.GetCustomAttributes(true).Any(attribute => attribute.GetType() == typeof(CustomDynamicProxyAttribute))
            }).GroupBy(x => new { x.AsSelf, x.IsGenericType, x.isDynamicProxy });

            foreach (var item in group)
            {
                if (!item.Key.isDynamicProxy)
                {
                    RegisterTypes(builder, item.Select(x => x.Type).ToList(), serviceLifetime, item.Key.AsSelf, item.Key.IsGenericType);
                }
                else
                {
                    RegisterTypes(builder, item.Select(x => x.Type).ToList(), serviceLifetime, item.Key.AsSelf, proxyGenerator);
                }
            }
        }

        /// <summary>
        /// 注册类型实例
        /// </summary>
        /// <param name="builder">ioc容器</param>
        /// <param name="types">要注册类型</param>
        /// <param name="serviceLifetime">要设置的生命周期</param>
        /// <param name="asSelf">是否只是注册自己，没有依赖</param>
        /// <param name="isGenericType">是否泛型</param>
        private static void RegisterTypes(ContainerBuilder builder, IEnumerable<Type> types, ServiceLifetime serviceLifetime, bool asSelf, bool isGenericType)
        {
            if (!types.Any())
            {
                return;
            }
            if (isGenericType)
            {
                foreach (var item in types)
                {
                    IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> regisBuilder;
                    var genericInterface = item.GetInterfaces().FirstOrDefault(x => x.IsGenericType);
                    if (genericInterface != null)
                    {
                        regisBuilder = builder.RegisterGeneric(item).As(genericInterface).PropertiesAutowired();
                    }
                    else
                    {
                        regisBuilder = builder.RegisterGeneric(item).PropertiesAutowired();
                    }
                    SetLifetime(regisBuilder, serviceLifetime);
                }
            }
            else
            {
                var regisBuilder = builder.RegisterTypes(types.ToArray()).PropertiesAutowired();
                if (asSelf)
                {
                    regisBuilder.AsSelf();
                }
                else
                {
                    regisBuilder.AsImplementedInterfaces();
                }
                SetLifetime(regisBuilder, serviceLifetime);
            }
        }

        /// <summary>
        /// 注册类型实例
        /// </summary>
        /// <param name="builder">ioc容器</param>
        /// <param name="types">要注册类型</param>
        /// <param name="serviceLifetime">要设置的生命周期</param>
        /// <param name="asSelf">是否只是注册自己，没有依赖</param>
        private static void RegisterTypes(ContainerBuilder builder, IEnumerable<Type> types, ServiceLifetime serviceLifetime, bool asSelf, ProxyGenerator proxyFactory)
        {
            if (!types.Any())
            {
                return;
            }
            foreach (var item in types)
            {
                var customDynamicProxyAttribute = item.GetCustomAttributes(true).First(a => a.GetType() == typeof(CustomDynamicProxyAttribute));

                var interceptors = (customDynamicProxyAttribute as CustomDynamicProxyAttribute).Interceptors;
                if (asSelf)
                {
                    IInterceptor[] interceptor = new IInterceptor[interceptors.Length];
                    var parameters = item.GetConstructors().First().GetParameters();
                    var orgs = new object[parameters.Length];
                    var regisBuilder = builder.Register(container =>
                    {
                        for (int i = 0; i < orgs.Length; i++)
                        {
                            var parameterType = parameters[i].ParameterType;

                            orgs[i] = container.Resolve(parameterType);
                        }

                        for (int i = 0; i < interceptor.Length; i++)
                        {
                            interceptor[i] = container.Resolve(interceptors[i]) as IInterceptor;
                        }

                        var interceptclass = proxyFactory.CreateClassProxy(item, orgs, interceptor);
                        return interceptclass;
                    });

                    regisBuilder.As(item);
                    SetLifetime(regisBuilder, serviceLifetime);
                }
                else
                {
                    var regisBuilder = builder.RegisterType(item).AsImplementedInterfaces()
                        .EnableInterfaceInterceptors()
                        .InterceptedBy(interceptors);

                    SetLifetime(regisBuilder, serviceLifetime);
                }
            }
        }

        /// <summary>
        /// 设置生命周期
        /// </summary>
        /// <typeparam name="TReflectionActivatorData">基于反射的激活器</typeparam>
        /// <param name="regisBuilder">注册容器</param>
        /// <param name="serviceLifetime">生命周期</param>
        private static void SetLifetime<TReflectionActivatorData>(IRegistrationBuilder<object, TReflectionActivatorData, DynamicRegistrationStyle> regisBuilder, ServiceLifetime serviceLifetime) where TReflectionActivatorData : ReflectionActivatorData
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton://单例
                    regisBuilder.SingleInstance();
                    break;
                case ServiceLifetime.Scoped://作用域
                    regisBuilder.InstancePerLifetimeScope();
                    break;
                case ServiceLifetime.Transient://瞬态
                    regisBuilder.InstancePerDependency();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设置生命周期
        /// </summary>
        /// <typeparam name="TReflectionActivatorData">基于反射的激活器</typeparam>
        /// <param name="regisBuilder">注册容器</param>
        /// <param name="serviceLifetime">生命周期</param>
        private static void SetLifetime<TReflectionActivatorData>(IRegistrationBuilder<object, TReflectionActivatorData, SingleRegistrationStyle> regisBuilder, ServiceLifetime serviceLifetime) where TReflectionActivatorData : IConcreteActivatorData
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton://单例
                    regisBuilder.SingleInstance();
                    break;
                case ServiceLifetime.Scoped://作用域
                    regisBuilder.InstancePerLifetimeScope();
                    break;
                case ServiceLifetime.Transient://瞬态
                    regisBuilder.InstancePerDependency();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder">ioc容器</param>
        /// <param name="modules">autofac打包的组件</param>
        /// <returns></returns>
        public static ContainerBuilder BuilderServiceProvider(this ContainerBuilder builder, params IModule[] modules)
        {
            if (modules != null && modules.Any())
            {
                foreach (var item in modules)
                {
                    builder.RegisterModule(item);
                }
            }

            var proxyFactory = new ProxyGenerator();

            //获取所有类型
            IEnumerable<Type> types = ReflectionTool.GetCustomRegisterTypes();

            //筛选单例、作用域、瞬态、工作单元的类型
            var singletons = types.Where(t => t.GetInterfaces().IsAny(typeof(ISingleton)));
            var scopes = types.Where(t => t.GetInterfaces().IsAny(typeof(IScope)));
            var transients = types.Where(t => t.GetInterfaces().IsAny(typeof(ITransient)));
            //单例
            RegisterTypes(builder, singletons, ServiceLifetime.Singleton, proxyFactory);

            //作用域
            RegisterTypes(builder, scopes, ServiceLifetime.Scoped, proxyFactory);

            //瞬态
            RegisterTypes(builder, transients, ServiceLifetime.Transient, proxyFactory);

            return builder;
        }





    }
}
