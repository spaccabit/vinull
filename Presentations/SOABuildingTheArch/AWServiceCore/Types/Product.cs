using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWServiceCore.Types {

    public class ProductCollection : List<Product> {

        internal void Add(AWDataSet.ProductRow row) {
            this.Add(new Product(row));
        }
    }

    public class Product {

        public Int32 ProductID { get; set; }
        public String ProductNumber { get; set; }
        public Int32 ProductModelID { get; set; }
        public String ModelName { get; set; }
        public Decimal ListPrice { get; set; }
        public Decimal StandardCost { get; set; }
        public String Color { get; set; }
        public String ProductName { get; set; }

        public Product() { }

        internal Product(AWDataSet.ProductRow row) {
            ProductID = row.ProductID;
            ProductNumber = row.ProductNumber;
            ProductModelID = row.ProductModelID;
            ModelName = row.ProductModelRow.Name;
            ListPrice = row.ListPrice;
            StandardCost = row.StandardCost;
            ProductName = row.Name;
            Color = row.IsColorNull() ? String.Empty : row.Color;
        }
    }
}
