using System;
using System.Collections.Generic;
using System.Text;

namespace Gluh.TechnicalTest.Database
{
    public class ProductStock
    {
        public int ID { get; set; }

        public Product Product { get; set; }

        public Supplier Supplier { get; set; }

        public int StockOnHand { get; set; }

        public decimal Cost { get; set; }
    }
}
