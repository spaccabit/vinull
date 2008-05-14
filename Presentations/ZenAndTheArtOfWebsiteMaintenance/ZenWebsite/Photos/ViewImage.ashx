<%@ WebHandler Language="C#" Class="ViewImage" %>

using System;
using System.Web;

public class ViewImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.WriteFile(context.Request.MapPath("~/App_Data/Photos/" + context.Request.QueryString));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}