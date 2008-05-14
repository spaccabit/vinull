using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.IO;

namespace Zen.Data {

    public class PhotoList : List<Photo> {

        public void AddPhoto(String FullPath, String FileName, String Name, String Url, String ThumbUrl, String PageUrl) {
            Photo photo = new Photo();
            photo.FileName = FileName;
            photo.Name = Name;
            photo.Url = Url;
            photo.ThumbUrl = ThumbUrl;
            photo.FullPath = FullPath;
            photo.PageUrl = PageUrl;
            this.Add(photo);
        }
    }

    public class Photo {

        [DataObjectField(true)]
        public String FullPath { get; set; }

        public String FileName { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String ThumbUrl { get; set; }
        public String PageUrl { get; set; }
	
    }
}