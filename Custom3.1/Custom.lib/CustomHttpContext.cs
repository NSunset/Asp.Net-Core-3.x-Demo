using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.lib
{
    public class CustomHttpContext
    {
        private static IHttpContextAccessor _httpContextAccessor { get; set; }


        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext CurrentHttpContext
        {
            get
            {
                if (_httpContextAccessor == null) return null;
                return _httpContextAccessor.HttpContext;
            }
        }
    }
}
