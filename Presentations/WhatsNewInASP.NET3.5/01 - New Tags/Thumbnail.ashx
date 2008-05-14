<%@ WebHandler Language="C#" Class="Thumbnail" %>

using System;
using System.Web;
using AWDataSetTableAdapters;

public class Thumbnail : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        if (String.IsNullOrEmpty(context.Request["pid"]))
            throw new Exception("parameter pid is required");

        ProductTableAdapter ta = new ProductTableAdapter();
        Byte[] data = ta.GetThumnailImage(Convert.ToInt32(context.Request["pid"]));

        if (data == null)
            throw new Exception("Product ID not found");
        
        context.Response.ContentType = "image/gif";
        context.Response.OutputStream.Write(data, 0, data.Length);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}