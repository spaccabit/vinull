using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Zen.Data {

    [XmlRoot("ProductList")]
    public class ProductList : List<Product> {

        public void Add(string Name, string ProductNumber, string Model, decimal ListPrice, string Category) {
            Product item = new Product();
            item.Category = Category;
            item.ListPrice = ListPrice;
            item.Model = Model;
            item.Name = Name;
            item.ProductNumber = ProductNumber;
            this.Add(item);
        }    
    };

    public class Product {

        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Model { get; set; }
        public decimal ListPrice { get; set; }
        public string Category { get; set; }
	
    }
}