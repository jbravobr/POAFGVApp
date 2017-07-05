using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace POAFGVApp
{
    public class OrderDetail : BaseEntity
    {
        [OneToMany]
        public List<Product> Products { get; set; }

        [ForeignKey(typeof(Order))]
        public int OrderId { get; set; }
    }
}