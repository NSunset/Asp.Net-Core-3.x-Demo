using Common.CustomException;
using Common.LogConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Filter
{
    public class CustomExceptionFilter : IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                ExceptionHandle(context);
            }
            context.ExceptionHandled = true;
        }

        public virtual void ExceptionHandle(ExceptionContext context)
        {
            context.Result = null;
            context.HttpContext.Response.ContentType = "application/json";
            int statusCode;
            if (context.Exception is MessageException)
            {
                LogHelp.Log.Warn(context.Exception.ToString());
                statusCode = 200;
            }
            else
            {
                LogHelp.Log.Error(context.Exception.ToString());
                statusCode = 500;
            }
            context.HttpContext.Response.StatusCode = statusCode;
            context.HttpContext.Response.WriteAsync("{\"status\":" + statusCode + ",\"data\":\"" + context.Exception.Message + "\"}", Encoding.UTF8);
        }
    }
}
