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
    // * The solution should be generic, handling a $1 delivery on a $500 phone or a $10 delivery on a $1 pencil
    // * If an order cannot be fulfilled, the algorthm should maximise the number of items delivered 
    public class PurchaseOptimizerPermutation
    {
        public static OverallOrder Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            var productOrderOptions = new Dictionary<int, List<Dictionary<int, int>>>();

            // For each product, get all the order options that will satisfy them
            foreach (var requirement in purchaseRequirements)
            {
                var supplierStock = requirement
                    .Product
                    .Stock
                    .Where(x => x.StockOnHand > 0)
                    .ToList();

                var maxAvailable = supplierStock.Sum(x => x.StockOnHand);

                if (supplierStock.Count > 0)
                {
                    var permutations = CreateProductSupplyPermutations(Math.Min(requirement.Quantity, maxAvailable),
                                                                       true,
                                                                       supplierStock,
                                                                       supplierStock.Count - 1);
                    productOrderOptions.Add(requirement.Product.ID, permutations);
                }
            }

            // The order from one product can affect the price of another by changing the shipping cost. Try each variation
            var productIds = productOrderOptions.Keys.ToArray();
            var running = new Dictionary<int, Dictionary<int, int>>();
            var results = FindBestCombination(productIds,
                                              0,
                                              running,
                                              purchaseRequirements,
                                              productOrderOptions);

            return results.bestOrder;
        }

        private static (OverallOrder bestOrder, decimal bestPrice) FindBestCombination(int[] productIds, 
            int productIdIdx, 
            Dictionary<int, Dictionary<int, int>> running,
            List<PurchaseRequirement> purchaseRequirements,
            Dictionary<int, List<Dictionary<int, int>>> productOrderOptions)
        {
            // For each product, we have a dictionary of potential supplier options. The cost of the supplier 
            // will depend upon the combination of products since it affects shipping costs. Adding a single
            // item to a supplier order could either add shipping or remove shipping, thus changing the overall cost.
            if (productIdIdx == productIds.Count() - 1)
            {
                // At the end - we can now check the actual price of the last product's combinations.
                OverallOrder bestOrder = null;
                decimal bestPrice = 0.0M;
                foreach (var supplierOptions in productOrderOptions[productIds[productIdIdx]])
                {
                    var thisRunning = new Dictionary<int, Dictionary<int, int>>(running);
                    thisRunning.Add(productIds[productIdIdx], supplierOptions);

                    var order = CreateOrders(purchaseRequirements, thisRunning);
                    var price = order.CalculateCost();
                    if (bestOrder == null || price < bestPrice)
                    {
                        bestOrder = order;
                        bestPrice = price;
                    }
                }
                return (bestOrder, bestPrice);
            }
            else
            {
                // We use recursion to check permutaions of an arbtrary set of products. Running configurations
                // are passed down to accumulate the order configurations for each product. We do not know the 
                // overall price until we have all products.
                OverallOrder bestOrder = null;
                decimal bestPrice = 0.0M;
                foreach (var supplierOptions in productOrderOptions[productIds[productIdIdx]])
                {
                    var thisRunning = new Dictionary<int, Dictionary<int, int>>(running);
                    thisRunning.Add(productIds[productIdIdx], supplierOptions);
                    var results = FindBestCombination(productIds, 
                                                      productIdIdx + 1, 
                                                      thisRunning, 
                                                      purchaseRequirements, 
                                                      productOrderOptions);

                    if (bestOrder == null || results.bestPrice < bestPrice)
                    {
                        bestOrder = results.bestOrder;
                        bestPrice = results.bestPrice;
                    }
                }
                return (bestOrder, bestPrice);
            }
        }

        private static OverallOrder CreateOrders(List<PurchaseRequirement> purchaseRequirements, Dictionary<int, Dictionary<int, int>> productAndSuppliers)
        {
            var supplierOrders = new Dictionary<int, SupplierOrder>();

            foreach (var (productId, suppliers) in productAndSuppliers)
            {
                var product = purchaseRequirements.Select(x => x.Product).Where(x => x.ID == productId).FirstOrDefault();

                foreach (var (supplierId, quantity) in suppliers)
                {
                    var supplierStock = product.Stock.Where(x => x.Supplier.ID == supplierId).FirstOrDefault();

                    if (!supplierOrders.ContainsKey(supplierId))
                    {
                        supplierOrders.Add(supplierId, new SupplierOrder(supplierStock.Supplier));
                    }

                    supplierOrders[supplierId].LineItems.Add(new LineItem() { Product = product, Quantity = quantity, SupplierItemCost = supplierStock.Cost });
                }


            }

            var unfulfilled = new List<PurchaseRequirement>();
            foreach (var requirement in purchaseRequirements)
            {
                var productId = requirement.Product.ID;
                if (!productAndSuppliers.ContainsKey(productId))
                {
                    unfulfilled.Add(new PurchaseRequirement() { Product = requirement.Product, Quantity = requirement.Quantity });
                }
                else
                {
                    var actualOrder = productAndSuppliers[productId]
                        .Sum(x => x.Value);
                    if (actualOrder < requirement.Quantity) {
                        unfulfilled.Add(new PurchaseRequirement() { Product = requirement.Product, Quantity = requirement.Quantity - actualOrder });
                    }

                }

            }

            return new OverallOrder() { SupplierOrders = supplierOrders.Values.ToList(),  Unfulfilled= unfulfilled };
        }

        private static List<Dictionary<int, int>> CreateProductSupplyPermutations(int required, bool isTopLevel, List<ProductStock> supplierStock, int supplierIndex)
        {
            var result = new List<Dictionary<int, int>>();

            if (supplierIndex < 0)
            {
                throw new ArgumentOutOfRangeException();

            }
            else if (supplierIndex == 0)
            {
                for (var count = 1; count <= required; count++)
                {
                    if (supplierStock[supplierIndex].StockOnHand >= count)
                    {
                        var combination = new Dictionary<int, int>();
                        combination.Add(supplierStock[supplierIndex].Supplier.ID, count);
                        result.Add(combination);
                    }
                }
            }
            else
            {
                var subset = CreateProductSupplyPermutations(required, false, supplierStock, supplierIndex - 1);
                foreach (var option in subset)
                {
                    var totalSoFar = option.Sum(x => x.Value);
                    if (required > totalSoFar)
                    {
                        for (var count = 1; count <= required - totalSoFar; count++)
                        {
                            if (supplierStock[supplierIndex].StockOnHand >= count)
                            {
                                var combination = new Dictionary<int, int>(option);
                                combination.Add(supplierStock[supplierIndex].Supplier.ID, count);
                                var totalForThisOption = combination.Sum(x => x.Value);
                                if (!isTopLevel || totalForThisOption == required) {
                                    result.Add(combination);
                                }
                            }
                        }
                    }
                    else
                    {
                        result.Add(option);
                    }
                }
            }

            return result;
        }
    }
}
