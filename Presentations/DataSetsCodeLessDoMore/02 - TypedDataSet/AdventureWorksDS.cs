using System.ComponentModel;
using System;
using System.Data.SqlClient;
using System.Data;

namespace _02___TypedDataSet.AdventureWorksDSTableAdapters {

    public partial class ProductModelTableAdapter {

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AdventureWorksDS.ProductModelDataTable GetModelsAndProdcuts() {

            AdventureWorksDS ds = new AdventureWorksDS();
            ProductTableAdapter taProducts = new ProductTableAdapter();
            taProducts.Connection = this.Connection;

            this.Connection.Open();
            this.Fill(ds.ProductModel);
            taProducts.Fill(ds.Product);
            this.Connection.Close();

            return ds.ProductModel;
        }
    }

    public partial class ProductTableAdapter {
        private enum CommandStatements {
            SelectAll = 0,
            SelectByPrice = 1
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AWProductCollection GetAWProductsByPrice(Decimal PricePoint) {

            AWProductCollection results = new AWProductCollection();

            this.CommandCollection[(int)CommandStatements.SelectByPrice].Parameters["@PricePoint"].Value = PricePoint;
            this._connection.Open();
            SqlDataReader reader = this.CommandCollection[(int)CommandStatements.SelectByPrice].ExecuteReader();
            while (reader.Read()) {
                AWProduct p = new AWProduct();
                p.Color = Convert.ToString(reader["Color"]);
                p.Cost = Convert.ToDecimal(reader["StandardCost"]);
                p.ListPrice = Convert.ToDecimal(reader["ListPrice"]);
                p.Name = Convert.ToString(reader["Name"]);
                p.ProductID = Convert.ToInt32(reader["ProductID"]);
                p.ProductSKU = Convert.ToString(reader["ProductNumber"]);
                results.Add(p);
            }
            this._connection.Close();

            return results;
        }
    }
}
