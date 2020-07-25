using System;
using System.Collections.Generic;
using System.Text;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest
{
    public class TestData
    {
        public List<PurchaseRequirement> Create()
        {
            var result = new List<PurchaseRequirement>();

            var supplier1 = new Supplier
            {
                ID = 1,
                Name = "Synnex",
                ShippingCost = 10,
                ShippingCostMinOrderValue = 150,
                ShippingCostMaxOrderValue = 1000
            };

            var supplier2 = new Supplier
            {
                ID = 2,
                Name = "Ingram Micro",
                ShippingCost = 0,
                ShippingCostMinOrderValue = 0,
                ShippingCostMaxOrderValue = 0
            };

            var supplier3 = new Supplier
            {
                ID = 3,
                Name = "Tech Data",
                ShippingCost = 10,
                ShippingCostMinOrderValue = 150,
                ShippingCostMaxOrderValue = 1000
            };

            var supplier4 = new Supplier
            {
                ID = 4,
                Name = "Multimedia Technology",
                ShippingCost = 10,
                ShippingCostMinOrderValue = 150,
                ShippingCostMaxOrderValue = 1000
            };

            var supplier5 = new Supplier
            {
                ID = 5,
                Name = "Dicker Data",
                ShippingCost = 9,
                ShippingCostMinOrderValue = 550,
                ShippingCostMaxOrderValue = 99999
            };

            var supplier6 = new Supplier
            {
                ID = 6,
                Name = "Leader",
                ShippingCost = 20,
                ShippingCostMinOrderValue = 0,
                ShippingCostMaxOrderValue = 500
            };

            var product1 = new Product
            {
                ID = 1,
                Name = "Google Pixel 3 Phone",
                Type = ProductType.Physical,
            };

            var product2 = new Product
            {
                ID = 2,
                Name = "Lenovo X1 Carbon Laptop",
                Type = ProductType.Physical
            };

            var product3 = new Product
            {
                ID = 3,
                Name = "Microsoft Office 365 Business Premium",
                Type = ProductType.NonPhysical
            };

            var product4 = new Product
            {
                ID = 4,
                Name = "Professional Services - 1 hour",
                Type = ProductType.Service
            };

            var product5 = new Product
            {
                ID = 5,
                Name = "Logitech MK450 Wireless Keyboard and Mouse",
                Type = ProductType.Physical
            };

            var product6 = new Product
            {
                ID = 6,
                Name = "HP 27\" LCD LED Professional Series Monitor",
                Type = ProductType.Physical
            };

            var product7 = new Product
            {
                ID = 7,
                Name = "Symantec Antivius Pro Plus Corporate Edition",
                Type = ProductType.NonPhysical
            };

            var product8 = new Product
            {
                ID = 8,
                Name = "Netgear Nighthawk NH100X Wireless Router",
                Type = ProductType.Physical
            };

            product1.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 1,
                    Cost = 940.56m,
                    StockOnHand = 105,
                    Supplier = supplier1,
                    Product = product1
                },
                new ProductStock
                {
                    ID = 2,
                    Cost = 918.10m,
                    StockOnHand = 2,
                    Supplier = supplier2,
                    Product = product1
                },
                new ProductStock
                {
                    ID = 3,
                    Cost = 935.40m,
                    StockOnHand = 15,
                    Supplier = supplier4,
                    Product = product1
                }
            };

            product2.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 4,
                    Cost = 1509.49m,
                    StockOnHand = 1,
                    Supplier = supplier2,
                    Product = product2
                },
                new ProductStock
                {
                    ID = 5,
                    Cost = 1489.80m,
                    StockOnHand = 13,
                    Supplier = supplier4,
                    Product = product2
                },
                new ProductStock
                {
                    ID = 6,
                    Cost = 1492.50m,
                    StockOnHand = 15,
                    Supplier = supplier5,
                    Product = product2
                }
            };

            product3.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 7,
                    Cost = 89.91m,
                    StockOnHand = 0,
                    Supplier = supplier1,
                    Product = product3
                },
                new ProductStock
                {
                    ID = 8,
                    Cost = 80.42m,
                    StockOnHand = 0,
                    Supplier = supplier3,
                    Product = product3
                },
                new ProductStock
                {
                    ID = 9,
                    Cost = 71.56m,
                    StockOnHand = 0,
                    Supplier = supplier6,
                    Product = product3
                }
            };

            product4.Stock = new List<ProductStock>
            {

            };

            product5.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 10,
                    Cost = 45.49m,
                    StockOnHand = 1405,
                    Supplier = supplier3,
                    Product = product5
                },
                new ProductStock
                {
                    ID = 11,
                    Cost = 46.50m,
                    StockOnHand = 120,
                    Supplier = supplier1,
                    Product = product5
                },
                new ProductStock
                {
                    ID = 12,
                    Cost = 44.50m,
                    StockOnHand = 2,
                    Supplier = supplier6,
                    Product = product5
                },
                new ProductStock
                {
                    ID = 13,
                    Cost = 45.40m,
                    StockOnHand = 130,
                    Supplier = supplier4,
                    Product = product5
                }
            };

            product6.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 14,
                    Cost = 242.75m,
                    StockOnHand = 62,
                    Supplier = supplier2,
                    Product = product6
                },
                new ProductStock
                {
                    ID = 15,
                    Cost = 240.69m,
                    StockOnHand = 18,
                    Supplier = supplier4,
                    Product = product6
                },
                new ProductStock
                {
                    ID = 16,
                    Cost = 201.42m,
                    StockOnHand = 2,
                    Supplier = supplier5,
                    Product = product6
                },
                new ProductStock
                {
                    ID = 17,
                    Cost = 243.19m,
                    StockOnHand = 108,
                    Supplier = supplier6,
                    Product = product6
                }
            };

            product7.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 18,
                    Cost = 67.10m,
                    StockOnHand = 0,
                    Supplier = supplier3,
                    Product = product7
                },
                new ProductStock
                {
                    ID = 19,
                    Cost = 68.42m,
                    StockOnHand = 0,
                    Supplier = supplier1,
                    Product = product7
                },
                new ProductStock
                {
                    ID = 20,
                    Cost = 101.42m,
                    StockOnHand = 9999,
                    Supplier = supplier6,
                    Product = product7
                },
                new ProductStock
                {
                    ID = 21,
                    Cost = 70.19m,
                    StockOnHand = 108,
                    Supplier = supplier4,
                    Product = product7
                }
            };

            product8.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 19,
                    Cost = 340.91m,
                    StockOnHand = 130,
                    Supplier = supplier1,
                    Product = product8
                },
                new ProductStock
                {
                    ID = 29,
                    Cost = 329.14m,
                    StockOnHand = 10,
                    Supplier = supplier3,
                    Product = product8
                },
                new ProductStock
                {
                    ID = 21,
                    Cost = 301.42m,
                    StockOnHand = 5,
                    Supplier = supplier4,
                    Product = product8
                },
                new ProductStock
                {
                    ID = 22,
                    Cost = 319.00m,
                    StockOnHand = 409,
                    Supplier = supplier5,
                    Product = product8
                }
            };

            result.AddRange(new List<Models.PurchaseRequirement> {
                new PurchaseRequirement
                {
                    Product = product1,
                    Quantity = 4
                },
                new PurchaseRequirement
                {
                    Product = product2,
                    Quantity = 8
                },
                new PurchaseRequirement
                {
                    Product = product3,
                    Quantity = 50
                },
                new PurchaseRequirement
                {
                    Product = product4,
                    Quantity = 40 // 40
                },
                new PurchaseRequirement
                {
                    Product = product5,
                    Quantity = 5  //50
                },
                new PurchaseRequirement
                {
                    Product = product6,
                    Quantity = 2 // 25
                },
                new PurchaseRequirement
                {
                    Product = product7,
                    Quantity = 10
                },
                new PurchaseRequirement
                {
                    Product = product8,
                    Quantity = 4
                }
            });

            return result;
        }
    }
}
