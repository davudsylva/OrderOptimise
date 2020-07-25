using System;
using System.Collections.Generic;
using System.Text;

namespace Gluh.TechnicalTest.Database
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ProductType Type { get; set; }

        public List<ProductStock> Stock { get; set; }
    }

    public enum ProductType
    {
        Physical,
        NonPhysical,
        Service
    }
}
