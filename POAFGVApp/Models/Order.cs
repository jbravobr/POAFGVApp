using System;
using System.Collections.Generic;

namespace POAFGVApp
{
    public class Order : BaseEntity
    {
        public List<OrderDetail> OrdersDetail { get; set; }
        public EnumPaymentType PaymentType { get; set; }
    }
}