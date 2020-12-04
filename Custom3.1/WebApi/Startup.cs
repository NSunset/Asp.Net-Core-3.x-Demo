using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Common.AppSettings;
using Common.AutoMapperConfig;
using Common.EntityFrameworkCore;
using Common.Filter;
using Common.IOC.AutoFac;
using Common.LogConfig;
using IApplication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ORM.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConnectionString ConnectionStr = new ConnectionString();
            _configuration.GetSection(ConnectionString.ConnectionStr).Bind(ConnectionStr);

            services.Configure<AppSetting>(_configuration);


            services.AddDbContext(ConnectionStr);
            services.AddControllers(options=> {
                //options.Filters.Add<CustomExceptionFilter>();
            });
        }

        /// <summary>
        /// 在ConfigureServices之后运行
        /// 将覆盖在ConfigureServices中进行的注册
        /// 不要建立容器； 由工厂为您完成
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //new AutoMapperModule(typeof(AppServiceAutoMapper).Assembly)
            builder.BuilderServiceProvider(new AutoMapperModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                //app.UseDeveloperExceptionPage();
            //}
            //else
            //{
                app.UseExceptionHandler(builder =>
                {
                    builder.Use(async (context, next) =>
                    {
                        if (!context.Response.HasStarted)
                        {
                            context.Response.ContentType = "application/json";

                        }
                    });
                });
            //}

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
            });
        }
    }
}
