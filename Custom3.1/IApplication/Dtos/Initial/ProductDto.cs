using System;
using System.Collections.Generic;
using System.Text;

namespace IApplication.Dtos.Initial
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CreateTime { get; set; }
    }
}
