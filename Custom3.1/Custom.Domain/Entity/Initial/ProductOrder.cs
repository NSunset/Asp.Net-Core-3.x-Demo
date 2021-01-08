﻿using Custom.lib.Domain;

namespace Custom.Domain.Entity.Initial
{
    public partial class ProductOrder : GeneralEntity
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        //public Order Order { get; set; }
        //public Product Product { get; set; }
    }
}
