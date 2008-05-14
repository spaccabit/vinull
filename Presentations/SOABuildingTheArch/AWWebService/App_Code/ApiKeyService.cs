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
using System.Web.Services;

public class ApiKeyService : WebService {

    protected String GetUsername(String apiKey, HttpContext context) {

        AWServiceCore.Types.PassCodeCollection codes 
            = AWServiceCore.ServiceBase.LoadPassCodes(context.Request.MapPath("~/App_Data/passcodes.xml"));
        foreach (AWServiceCore.Types.PassCode code in codes)
            if (code.Code.Equals(apiKey))
                return code.User;

        if (apiKey != null && !apiKey.Equals(String.Empty))
            return null;

        if (HttpContext.Current.User.Identity.IsAuthenticated)
            return HttpContext.Current.User.Identity.Name;

        return null;
    }
}


