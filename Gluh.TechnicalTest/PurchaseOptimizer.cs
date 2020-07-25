using System;
using System.Collections.Generic;
using System.Text;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Database;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Gluh.TechnicalTest
{
    // Assumptions: 
    // * Each supplier should have a unique ID - there were some duplicates in the data file so I fixed them
    // * The solution should e generic, handling a $1 delivery on a $100 phone or a $10 delivery on a $1 pencil
    // * If an order cannot be fulfilled, the algorthm should maximise the number of items delivered regardless of cost
    // * Non physical items still have stock balances (Since there are varying balances for the Anti-virus product,
    //   I am assuming that means the shop only owns a certain number of licences. MS Office has zero stock)
    public class PurchaseOptimizer
    {
        /// <summary>
        /// Calculates the optimal set of supplier to purchase products from.
        /// ### Complete this method
        /// </summary>
        public void Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            var orders = PurchaseOptimizerPermutation.Optimize(purchaseRequirements);

            var totalCost = 0.0M;
            foreach (var order in orders.SupplierOrders)
            {
                var supplierId = $"Supplier {order.Supplier.ID}";
                foreach (var lineItem in order.LineItems)
                {
                    var line = $"{supplierId} {lineItem.Quantity,2} x {lineItem.Product.Name}  @ ${lineItem.SupplierItemCost}";
                    Console.WriteLine(line);
                    supplierId = new String(' ', supplierId.Length);
                }
                var supplierCosts = order.CalculateCost();
                Console.WriteLine($"Shipping: ${supplierCosts.shippingCost} ");
                Console.WriteLine($"Subtotal: ${supplierCosts.totalOrderCost} ");
                totalCost += supplierCosts.totalOrderCost;
            }
            Console.WriteLine($"Total: ${totalCost}");

            if (orders.Unfulfilled.Count > 0)
            {
                Console.WriteLine($"Unfulfiled order components:");
                foreach (var unfulfilled in orders.Unfulfilled)
                {
                    Console.WriteLine($"* Product {unfulfilled.Product.Name} has {unfulfilled.Quantity} shortfall");
                }
            }
        }
    }



    public class OverallOrder
    {
        public List<SupplierOrder> SupplierOrders { get; set; }
        public List<PurchaseRequirement> Unfulfilled { get; set; }


        public decimal CalculateCost()
        {
            return SupplierOrders.Sum(x => x.CalculateCost().totalOrderCost);
        }
    }

    public class SupplierOrder
    {
        public Supplier Supplier { get; set; }
        public List<LineItem> LineItems { get; set; }

        public SupplierOrder(Supplier supplier)
        {
            this.Supplier = supplier;
            LineItems = new List<LineItem>();
        }

        public (decimal totalOrderCost, decimal shippingCost) CalculateCost()
        {
            var cost = LineItems.Sum(x => x.SupplierItemCost * x.Quantity);
            var shippingCost = (cost >= Supplier.ShippingCostMinOrderValue && cost <= Supplier.ShippingCostMaxOrderValue) ? Supplier.ShippingCost : 0;

            return (cost + shippingCost, shippingCost);
        }
    }

    public class LineItem
    {
        public Product Product { get; set; }
        public decimal SupplierItemCost { get; set; }
        public int Quantity { get; set; }
    }
}
