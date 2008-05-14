using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zen.Data;

namespace Zen.Providers {
    public class PhotoSiteMapProvider : StaticSiteMapProvider {

        private String rootUrl;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes) {
            base.Initialize(name, attributes);

            rootUrl = attributes["rootUrl"];
        }

        public override SiteMapNode BuildSiteMap() {
            SiteMapNode root = new SiteMapNode(this, "root", rootUrl, "Photos");
            lock (this) {
                this.Clear();

                PhotoManager photoMgr = new PhotoManager();
                PhotoList photos = photoMgr.GetPhotos();

                this.AddNode(root);
                foreach (Photo photo in photos) {
                    SiteMapNode photoNode = new SiteMapNode(this, photo.FileName, photo.PageUrl, photo.Name);
                    this.AddNode(photoNode, root);
                }
            }
            return root;
        }

        protected override SiteMapNode GetRootNodeCore() {
            return new SiteMapNode(this, "root", rootUrl, "Photos");
        }
    }
}