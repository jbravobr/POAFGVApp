using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace POAFGVApp
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}