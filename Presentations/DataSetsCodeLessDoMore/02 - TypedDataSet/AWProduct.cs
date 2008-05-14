using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _02___TypedDataSet {
    public class AWProductCollection : List<AWProduct> { }

    public class AWProduct {

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductSKU { get; set; }
        public string Color { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Cost { get; set; }

    }
}
