using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POAFGVApp
{
    public class Order : BaseEntity
    {
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<OrderDetail> OrdersDetail { get; set; }
        public string PaymentType { get; set; }
        public DateTimeOffset OrderDateTime { get; set; }

        [Ignore]
        public string FormattedProducts
        {
            get
            {
                if (OrdersDetail != null && OrdersDetail.Any())
                    return OrdersDetail.SelectMany(o => o.Products).Select(x => x.Description).Aggregate((p1, p2) => $"{p1}, {p2}");

                return "Nenhum produto encontado.";
            }
        }

        [Ignore]
        public string FormattedPayment
        {
            get
            {
                if (OrdersDetail != null && OrdersDetail.Any())
                    return $"Pedido pago com {PaymentType}";

                return "Nenhum pagamento encontrado.";
            }
        }
    }
}