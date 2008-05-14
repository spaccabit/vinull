using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using _02___TypedDataSet;

/// <summary>
/// Summary description for Metrics
/// </summary>
public class Metrics {

    public static void AddExpectedProfit(AdventureWorksDS.ProductDataTable dt) {

        dt.Columns.Add("ExpectedProfit", typeof(Decimal));

        foreach (AdventureWorksDS.ProductRow row in dt) {
            row["ExpectedProfit"] = CalcExpectedProfit(row.StandardCost, row.ListPrice, 5M);
        }

    }

    public static Decimal CalcExpectedProfit(Decimal Cost, Decimal Price, Decimal Overhead) {
        return Price - (Cost + Overhead);
    }
}
