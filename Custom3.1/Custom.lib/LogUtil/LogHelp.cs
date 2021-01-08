using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Custom.lib.LogUtil
{
    public class LogHelp
    {
        private readonly static ILog _log;

        private static Assembly repositoryAssembly = Assembly.GetEntryAssembly();
        static LogHelp()
        {
            if (_log == null)
            {
                _log = LoadLog4NetConfig();
            }
        }

        private static ILog LoadLog4NetConfig()
        {
            var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "log4net.config");
            var repository = LogManager.CreateRepository(repositoryAssembly, typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repository, new System.IO.FileInfo(path));
            return LogManager.GetLogger(repositoryAssembly.GetType());

        }

        public static void Error(string message)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(FormatDetailMessage(message));
            }
        }
        public static void Error(string message, Exception exception)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(FormatDetailMessage(message, exception.StackTrace));
            }
        }

        public static void Info(string message, Exception exception)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(FormatDetailMessage(message, exception.StackTrace));
            }
        }
        public static void Info(string message)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(FormatDetailMessage(message));
            }
        }

        public static void Debug(string message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(FormatDetailMessage(message));
            }
        }

        public static void Debug(string message, Exception exception)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(FormatDetailMessage(message, exception.StackTrace));
            }
        }

        public static void Warn(string message)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(FormatDetailMessage(message));
            }
        }

        public static void Warn(string message, Exception exception)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(FormatDetailMessage(message, exception.StackTrace));
            }
        }


        private static string FormatDetailMessage(string logMsg, string stackTrace = "")
        {
            HttpContext context = CustomHttpContext.CurrentHttpContext;

            string msg = @"{0}|{1}*RequestUrl:{2}*LogMessage:{3}*Machine:{4}*ClientIP:{5}*Headers:{6}*Parameters:{7}*StackTrae:{8}*";
            msg = msg.Replace("*", Environment.NewLine);

            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var requestUrl = "";
            var machine = Environment.MachineName;
            var clientIP = "";
            string headerInfo = "";
            string requestBody = "";
            if (context != null)
            {
                var request = context.Request;
                clientIP = context.Connection.RemoteIpAddress.ToString();
                requestUrl = $"{request.Host.Value}{request.Path.Value}";

                headerInfo = GetHeaderInfo(request);

                if (HttpMethods.IsGet(request.Method))
                {
                    requestBody = GetGetRequestInfo(request);
                }
                if (HttpMethods.IsPost(request.Method))
                {
                    requestBody = GetPostRequestInfo(request);
                }
            }

            return String.Format(msg, "Custom.Api", datetime, requestUrl, logMsg, machine, clientIP, headerInfo, requestBody, stackTrace);
        }

        private static string GetHeaderInfo(HttpRequest request)
        {
            var headers = request.Headers;
            string header = "";
            if (headers != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in headers)
                {
                    if (headers.TryGetValue(item.Key, out StringValues values))
                    {
                        foreach (string value in values)
                        {
                            sb.Append($"{item.Key}:{value};");
                        }
                    }
                }
                header = sb.ToString();
            }
            return header;
        }

        private static string GetGetRequestInfo(HttpRequest request)
        {
            string requestBody = "";
            if (request.QueryString.HasValue)
            {
                requestBody = System.Web.HttpUtility.UrlDecode(request.QueryString.ToString());
            }
            if (request.HasFormContentType && request.Form.Any())
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in request.Form.Keys)
                {
                    if (request.Form.TryGetValue(key, out StringValues values))
                    {
                        foreach (string item in values)
                        {
                            sb.Append($"{key}:{item};");
                        }
                    }
                }
                requestBody = sb.ToString();
            }
            return requestBody;
        }

        private static string GetPostRequestInfo(HttpRequest request)
        {
            request.EnableBuffering();
            var read = new System.IO.StreamReader(request.Body);
            var requestBody = read.ReadToEnd();
            request.Body.Seek(0, System.IO.SeekOrigin.Begin);
            return requestBody;
        }
    }
}
