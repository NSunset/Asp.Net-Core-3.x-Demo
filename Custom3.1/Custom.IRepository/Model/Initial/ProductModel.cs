using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.IRepository.Model.Initial
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
