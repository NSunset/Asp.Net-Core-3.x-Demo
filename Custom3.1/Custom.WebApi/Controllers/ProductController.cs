using System;
using System.Threading.Tasks;
using Custom.lib.Exceptions;
using Custom.IApplication;
using Custom.IApplication.Dtos.Initial;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Custom.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomController
    {
        private IProductAppService _productAppService;
        private IUserAppService _userAppservice;

        public ProductController(IProductAppService productAppService,
                                IUserAppService userAppService)
        {
            _productAppService = productAppService;
            _userAppservice = userAppService;
        }

        [HttpGet]
        [Route("/")]
        [Route("/product")]
        public async Task<IActionResult> Get()
        {
            var result = await _productAppService.FindAllAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("/product/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            //throw new MessageException("自定义错误记录请求数据");
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new CustomNullOrWhiteSpaceException(nameof(username));
            }
            var result = await _productAppService.FindAllAsync();
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
            return Ok(new { msg = "添加商品" });
        }

        [Route("/user")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userAppservice.GetAllAsync();
            return Ok(users);
        }

        [Route("/user/product")]
        public async Task<IActionResult> GetUesrProduct()
        {
            var result = await _userAppservice.GetUserProductAsync();
            return Ok(result);
        }

        [Route("/user/update")]
        public async Task<IActionResult> UserUpdate()
        {
            var result = await _userAppservice.UpdateAsync(new Custom.IRepository.Model.Default.UserInfo
            {
                Id = 1,
                UserName = "张三",
                CreateTime = DateTime.Now
            });
            return Ok(result);
        }
    }
}
