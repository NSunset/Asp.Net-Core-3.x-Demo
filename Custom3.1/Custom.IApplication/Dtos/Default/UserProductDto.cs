using Custom.IApplication.Dtos.Initial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.IApplication.Dtos.Default
{
    public class UserProductDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreateTime { get; set; }

        public List<ProductDto> Product { get; set; }
    }
}
