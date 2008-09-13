using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWServiceCore.AWDataSetTableAdapters;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AWServiceCore {
    public class ProductService : ServiceBase {

        public static Types.ProductCollection GetProducts() {
            AWDataSet ds = GetDSAllProducts(ConnectionString);
            Types.ProductCollection products = ConvertToProducts(ds);
            return products;

           EventLog.WriteEntry("ProductServiceAPI", "Opps", EventLogEntryType.Warning);
        }

        public static Types.ProductCollection GetProductsByModel(Int32 ModelID) {
            AWDataSet ds = GetDSProductsByModel(ConnectionString, ModelID);
            Types.ProductCollection products = ConvertToProducts(ds);
            return products;
        }

        public static DataSet DirectQuery(String query) {
            DataSet ds = new DataSet("RESULTS");

            String check = query.ToLower();
            if (check.Contains("delete") ||
               check.Contains("insert") ||
               check.Contains("update") ||
               !check.Contains("select"))
                throw new Exception("Only SELECT statments are permitted, tyvm.");

            try {
                SqlConnection conn = new SqlConnection(ConnectionString);
                SqlCommand command = conn.CreateCommand();

                command.CommandType = CommandType.Text;
                command.CommandText = query;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable results = new DataTable("ROW");
                results.Load(reader);
                conn.Close();

                reader.Dispose();
                command.Dispose();
                conn.Dispose();

                ds.Tables.Add(results);
            }
            catch (Exception ex) {
                DataTable error = new DataTable("ERROR");
                error.Columns.Add("MESSAGE", typeof(String));
                error.Columns.Add("STACKTRACE", typeof(String));
                Object[] row = { ex.Message.Trim(), "\n" + ex.StackTrace };
                error.Rows.Add(row);
                ds.Tables.Add(error);
            }

            return ds;
        }

        private static AWDataSet GetDSAllProducts(String connString) {
            AWDataSet ds = new AWDataSet();
            ProductTableAdapter taProduct = new ProductTableAdapter();
            ProductModelTableAdapter taModel = new ProductModelTableAdapter();

            taProduct.Connection.ConnectionString = connString;
            taModel.Connection = taProduct.Connection;

            taProduct.Connection.Open();
            taProduct.FillAll(ds.Product);
            taModel.FillAll(ds.ProductModel);
            taProduct.Connection.Close();

            return ds;
        }

        private static AWDataSet GetDSProductsByModel(String connString, Int32 ModelID) {
            AWDataSet ds = new AWDataSet();
            ProductTableAdapter taProduct = new ProductTableAdapter();
            ProductModelTableAdapter taModel = new ProductModelTableAdapter();

            taProduct.Connection.ConnectionString = connString;
            taModel.Connection = taProduct.Connection;

            taProduct.Connection.Open();
            taProduct.FillByModel(ds.Product, ModelID);
            taModel.FillByID(ds.ProductModel, ModelID);
            taProduct.Connection.Close();

            return ds;
        }

        private static Types.ProductCollection ConvertToProducts(AWDataSet ds) {
            Types.ProductCollection products = new Types.ProductCollection();

            foreach (AWDataSet.ProductRow row in ds.Product)
                products.Add(row);

            return products;
        }
    }
}
