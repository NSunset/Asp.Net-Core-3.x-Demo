using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CustomController : ControllerBase
    {

        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            return base.Ok(GetResult(value));
        }

        private string GetResult(object value = null)
        {
            if (value == null)
                value = "";
            var data = new
            {
                code = 200,
                data = value
            };
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(data).ToLower();
            return result;
        }
    }



}
