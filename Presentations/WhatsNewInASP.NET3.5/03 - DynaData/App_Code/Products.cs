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
using System.Web.DynamicData;

/// <summary>
/// Summary description for Products
/// </summary>
public partial class Product
{
    partial void OnProductNumberChanging(string value) {
        if (!value.Contains('-'))
            throw new Exception("Product Number must contain dash (-)");
    }
}

[DisplayColumn("CompanyName")]
public partial class Customer { }

[RenderHint("OnlineOrderFlag","BooleanText")]
public partial class SalesOrderHeader { }