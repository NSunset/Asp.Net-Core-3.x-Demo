using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Custom.WebApi.Controllers
{
    public class CustomController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            var result = base.Ok(GetResult(value));
            result.ContentTypes.Add("application/json");
            return result;
        }

        public override OkResult Ok()
        {
            return new CustomOkResult();
        }

        private object GetResult(object value = null)
        {
            if (value == null)
                value = "";
            var data = new
            {
                code = 200,
                message = "",
                data = value
            };
            return data;
        }

    }

    public class CustomOkResult : OkResult
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public CustomOkResult()
        {
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        public override void ExecuteResult(ActionContext context)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                code = 200,
                message = "",
                data = ""
            }, _jsonSerializerOptions);
            context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
            context.HttpContext.Response.WriteAsync(json);
        }
    }



}
