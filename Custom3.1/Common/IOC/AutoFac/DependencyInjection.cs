using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Common.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Common.IOC.AutoFac
{
    public static class DependencyInjection
    {

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="builder">ioc容器</param>
        /// <param name="types">要注册的类型</param>
        /// <param name="serviceLifetime">生命周期</param>
        private static void RegisterTypes(ContainerBuilder builder, IEnumerable<Type> types, ServiceLifetime serviceLifetime)
        {
            if (!types.Any())
            {
                return;
            }
            var group = types.Select(x => new
            {
                AsSelf = x.GetInterfaces().Any(@interface => @interface == typeof(IDependencyInterfaceIgnore)),
                IsGenericType = x.IsGenericType,
                Type = x
            }).GroupBy(x => new { x.AsSelf, x.IsGenericType });

            foreach (var item in group)
            {
                RegisterTypes(builder, item.Select(x => x.Type).ToList(), serviceLifetime, item.Key.AsSelf, item.Key.IsGenericType);
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

            //获取所有类型
            IEnumerable<Type> types = ReflectionTool.GetLoadAssemblyTypes();

            //筛选单例、作用域、瞬态的类型
            var singletons = types.Where(t => t.GetInterfaces().Contains(typeof(ISingleton)));
            var scopes = types.Where(t => t.GetInterfaces().Contains(typeof(IScope)));
            var transients = types.Where(t => t.GetInterfaces().Contains(typeof(ITransient)));


            //单例
            RegisterTypes(builder, singletons, ServiceLifetime.Singleton);

            //作用域
            RegisterTypes(builder, scopes, ServiceLifetime.Scoped);

            //瞬态
            RegisterTypes(builder, transients, ServiceLifetime.Transient);

            return builder;
        }



    }
}
