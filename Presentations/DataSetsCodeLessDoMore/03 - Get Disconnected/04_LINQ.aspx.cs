using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class _04_LINQ : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!Page.IsPostBack) {

            _02___TypedDataSet.AdventureWorksDSTableAdapters.CustomerTableAdapter ta = new _02___TypedDataSet.AdventureWorksDSTableAdapters.CustomerTableAdapter();

            _02___TypedDataSet.AdventureWorksDS.CustomerDataTable dt = ta.GetData();

            String[] salesPeople = new String[] {
                                @"adventure-works\jillian0",
                                @"adventure-works\pamela0",
                                @"adventure-works\david8" };

            var customers = from row in dt
                            where salesPeople.Contains(row.SalesPerson)
                            select new {
                                row.CompanyName,
                                row.CustomerID,
                                row.EmailAddress,
                                row.FirstName,
                                row.LastName,
                                row.SalesPerson
                            };


            gvProducts.DataSource = customers;
            gvProducts.DataBind();


        }

    }
}
