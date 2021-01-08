using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Custom.lib;
using Custom.lib.UnitofWork;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Autofac;
using Custom.lib.IOC;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCustomHttpContext(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            IHttpContextAccessor httpContextAccessor = services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();
            CustomHttpContext.Configure(httpContextAccessor);
            return services;
        }

     
    }
}
