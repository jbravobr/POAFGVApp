using System;
using System.Collections.Generic;

namespace POAFGVApp
{
    public class OrderDetail : BaseEntity
    {
        public List<Product> Products { get; set; }
    }
}