using System;
using System.Collections.Generic;
using System.Text;

namespace Gluh.TechnicalTest.Database
{
    public class Supplier
    {
        public int ID { get; set; }

        /// <summary>
        /// name of supplier
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The shipping cost that applies when purchasing products from this supplier
        /// </summary>
        public decimal ShippingCost { get; set; }

        /// <summary>
        /// Represents the minimum purchase order value for the shipping cost to apply, otherwise ShippingCost is 0
        /// </summary>
        public decimal ShippingCostMinOrderValue { get; set; }

        /// <summary>
        /// Represents the maximum purchase order value for the shipping cost to apply, otherwise ShippingCost is 0
        /// </summary>
        public decimal ShippingCostMaxOrderValue { get; set; }
    }
}
