using System;
using System.Collections.Generic;

namespace POAFGVApp
{
    public static class OrderHelper
    {
        public static List<Order> Orders => new List<Order>()
        {
            new Order()
            {
                 Id = 1,
                 OrdersDetail = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                         Id =1,
                         OrderId =1 ,
                         Products = new List<Product>()
                        {
                            new Product()
                            {
                                 Id= 1,
                                 Description = "Calabreza",
                                 Price = 30.00M
                            }
                        }
                    }
                },
                OrderDateTime = DateTime.Now,
                PaymentType = "Dinheiro"
            },
            new Order()
            {
                 Id = 2,
                 OrdersDetail = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                         Id =2,
                         OrderId =2 ,
                         Products = new List<Product>()
                        {
                            new Product()
                            {
                                 Id= 2,
                                 Description = "Portuguesa",
                                 Price = 28.00M
                            }
                        }
                    }
                },
                OrderDateTime = DateTime.Now,
                PaymentType = "Cartão de Crédito"
            },
            new Order()
            {
                 Id = 3,
                 OrdersDetail = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                         Id =3,
                         OrderId =3 ,
                         Products = new List<Product>()
                        {
                            new Product()
                            {
                                 Id= 3,
                                 Description = "Muçarela",
                                 Price = 20.00M
                            }
                        }
                    }
                },
                OrderDateTime = DateTime.Now,
                PaymentType = "Cartão de Débito"
            },
            new Order()
            {
                 Id = 1,
                 OrdersDetail = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                         Id =4,
                         OrderId =4 ,
                         Products = new List<Product>()
                        {
                            new Product()
                            {
                                 Id= 4,
                                 Description = "Calabreza",
                                 Price = 30.00M
                            },
                            new Product()
                            {
                                Id = 2,
                                Description = "Portuguesa",
                                Price = 49.00M
                            }
                        }
                    }
                },
                OrderDateTime = DateTime.Now,
                PaymentType = "Dinheiro"
            }
        };
    }
}