<%@ WebHandler Language="C#" Class="Thumbnail" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class Thumbnail : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.Cache.SetExpires(DateTime.Now);
        context.Response.ContentType = "image/jpeg";

        Image fullSize = Image.FromFile(context.Request.MapPath("~/App_Data/Photos/" + context.Request.QueryString));
        Image thumbnail = fullSize.GetThumbnailImage(200, 200 * fullSize.Height / fullSize.Width, null, IntPtr.Zero);

        thumbnail.Save(context.Response.OutputStream, ImageFormat.Jpeg);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}