using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace POAFGVApp
{
    public class Order : BaseEntity
    {
        [OneToMany]
        public List<OrderDetail> OrdersDetail { get; set; }
        public EnumPaymentType PaymentType { get; set; }
    }
}