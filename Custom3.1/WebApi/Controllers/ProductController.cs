using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.CustomException;
using Common.LogConfig;
using IApplication;
using IApplication.Dtos.Initial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomController
    {
        private IProductAppService _productAppService;
        private IUserAppService _userAppservice;

        private ILogger _log;

        public ProductController(IProductAppService productAppService,
                                ILogger<ProductController> log,
                                IUserAppService userAppService)
        {
            _productAppService = productAppService;
            _log = log;
            _userAppservice = userAppService;
        }

        [HttpGet]
        [Route("/")]
        [Route("/product")]
        public async Task<IActionResult> Get()
        {
            throw new MessageException("自定义错误记录请求数据");
            var result = await _productAppService.FindAll();
            return Ok(result);
        }

        [Route("/product/getbycondition")]
        public async Task<IActionResult> GetXLProduct([FromQuery] ProductDto input)
        {
            return Ok();
        }

        [HttpPost]
        [Route("/product")]
        public async Task<IActionResult> ProductAdd(ProductDto input)
        {
            return Ok(new {msg="添加商品" });
        }

        [Route("/user")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userAppservice.GetAll();
            return Ok(users);
        }

        [Route("/user/product")]
        public async Task<IActionResult> GetUesrProduct()
        {
            var result = await _userAppservice.GetUserProduct();
            return Ok(result);
        }
    }
}
