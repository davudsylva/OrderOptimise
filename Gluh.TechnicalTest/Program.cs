using System;
using System.Collections.Generic;


namespace Gluh.TechnicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var purchaseRequirements = new TestData().Create();
            var purchaseOptimizer = new PurchaseOptimizer();

            purchaseOptimizer.Optimize(purchaseRequirements);
        }
    }
}
