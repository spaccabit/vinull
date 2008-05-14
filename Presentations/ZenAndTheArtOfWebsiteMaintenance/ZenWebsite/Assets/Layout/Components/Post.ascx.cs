using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

[ParseChildren(ChildrenAsProperties=true)]
public partial class Assets_Layout_Components_Post : System.Web.UI.UserControl, INamingContainer {

    private ITemplate _Title;
    public ITemplate Title {
        get { return _Title; }
        set { _Title = value; }
    }

    private ITemplate _body;
    public ITemplate Body {
        get { return _body; }
        set { _body = value; }
    }
	
    public String Author {
        get { return lAuthor.Text; }
        set { lAuthor.Text = value; }
    }

    private DateTime _postDate;
    public DateTime PostDate {
        get { return _postDate; }
        set {
            _postDate = value;
            lDate.Text = _postDate.ToLongDateString();
        }
    }

    protected void Page_Init(object sender, EventArgs e) {
        Body.InstantiateIn(phBody);
        Title.InstantiateIn(phTitle);
    }
}
