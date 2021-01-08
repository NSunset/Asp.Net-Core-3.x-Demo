using Custom.lib.Exceptions;
using Custom.lib.LogUtil;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Custom.lib.Middlewares
{
    /// <summary>
    /// 除项目启动错误外的管道全局错误处理
    /// </summary>
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// 1、在这里注入的信息可以不在容器里。可在添加到管道的时候手动实例化注入信息
        /// 2、在这里注入的信息只会执行一次，因为类只会实例一次
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        /// <summary>
        /// 1、在这里注入的信息必须是已经在容器里的。否则找不到
        /// 2、在这里注入的信息是多会多次创建的
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogHelp.Error(ex.Message, ex);
                var code = 500;
                var message = "服务器错误";
                if (ex is CustomMessageException customMessageException)
                {
                    code = customMessageException.Code;
                    message = customMessageException.Message;
                }
                var response = new
                {
                    code,
                    message,
                    data = ""
                };

                var json = JsonSerializer.Serialize(response, _jsonSerializerOptions);
                context.Response.ContentType = "application/json;charset=utf-8;";
                await context.Response.WriteAsync(json);
            }

        }

    }
}
