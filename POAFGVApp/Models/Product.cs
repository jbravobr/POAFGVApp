using System;
namespace POAFGVApp
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}