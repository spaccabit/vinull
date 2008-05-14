using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Data.SqlClient;

namespace AdventureWorks.InventoryTableAdapters {
    public partial class ProductTableAdapter {

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Inventory.ProductDataTable GetLinkedProducts(int? ProductCategoryID) {
            Inventory ds = new Inventory();

            ProductModelTableAdapter taModel = new ProductModelTableAdapter();
            taModel.Connection = this.Connection;

            this.Connection.Open();
            this.FillByProductCategoryID(ds.Product, ProductCategoryID);
            taModel.FillByProductCategoryID(ds.ProductModel, ProductCategoryID);
            this.Connection.Close();

            return ds.Product;
        }

        public Zen.Data.ProductList GetAllZen() {
            ProductCategoryTableAdapter taCat = new ProductCategoryTableAdapter();
            ProductModelTableAdapter taModel = new ProductModelTableAdapter();

            taCat.Connection = this.Connection;
            taModel.Connection = this.Connection;

            Zen.Data.ProductList results = new Zen.Data.ProductList();

            this.Connection.Open();
            Inventory.ProductModelDataTable dtModel = taModel.GetAll();
            Inventory.ProductCategoryDataTable dtCategory = taCat.GetAll();

            SqlDataReader sqlReader = this.CommandCollection[0].ExecuteReader();

            while (sqlReader.Read()) {
                results.Add(
                    Convert.ToString(sqlReader["Name"]),
                    Convert.ToString(sqlReader["ProductNumber"]),
                    dtModel.FindByProductModelID(Convert.ToInt32(sqlReader["ProductModelID"])).Name,
                    Convert.ToDecimal(sqlReader["ListPrice"]),
                    dtCategory.FindByProductCategoryID(Convert.ToInt32(sqlReader["ProductCategoryID"])).Name
                );
            }

            sqlReader.Close();
            this.Connection.Close();

            return results;
        }
    }
}