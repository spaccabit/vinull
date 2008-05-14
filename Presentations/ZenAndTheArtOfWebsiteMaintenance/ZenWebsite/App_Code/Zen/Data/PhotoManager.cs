using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI;

namespace Zen.Data {
    [DataObject(true)]
    public class PhotoManager {

        public PhotoManager() { }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public PhotoList GetPhotos() {
            String dir = HttpContext.Current.Request.MapPath("~/App_Data/Photos/");
            String[] files = Directory.GetFiles(dir, "*.jpg", SearchOption.TopDirectoryOnly);
            PhotoList results = new PhotoList();

            Control crtl = new Control();

            foreach (String file in files) {
                results.AddPhoto(file, Path.GetFileName(file), Path.GetFileNameWithoutExtension(file).Replace("_"," "),
                  crtl.ResolveUrl("~/Photos/ViewImage.ashx?" + Path.GetFileName(file)),
                  crtl.ResolveUrl("~/Photos/Thumbnail.ashx?" + Path.GetFileName(file)),
                  crtl.ResolveUrl("~/Photos/View.aspx?" + Path.GetFileName(file))
                );
            }

            return results;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Photo GetPhotoByFileName(String FileName) {
            Control crtl = new Control();
            Photo photo = new Photo();

            photo.FileName = FileName;
            photo.Name = Path.GetFileNameWithoutExtension(FileName);
            photo.FullPath = HttpContext.Current.Request.MapPath("~/App_Data/Photos/" + FileName);
            photo.Url = crtl.ResolveUrl("~/Photos/ViewImage.ashx?" + Path.GetFileName(FileName));
            photo.ThumbUrl = crtl.ResolveUrl("~/Photos/Thumbnail.ashx?" + Path.GetFileName(FileName));
            photo.PageUrl = crtl.ResolveUrl("~/Photos/View.aspx?" + Path.GetFileName(FileName));

            return photo;
        }
    }
}