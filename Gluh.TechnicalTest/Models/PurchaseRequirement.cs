using System;
using System.Collections.Generic;
using System.Text;
using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseRequirement
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
