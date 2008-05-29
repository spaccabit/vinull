using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web;
using System.Web.Configuration;

namespace ViNull.Blog {
    public class SqlBlogProvider : BlogProvider {

        private string _applicationName;

        public override string ApplicationName {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        private string _connectionStringName;
        public string ConnectionStringName {
            get { return _connectionStringName; }
        }

        public override void Initialize(string name, NameValueCollection config) {

            if (config == null)
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "SqlBlogProvider";

            if (String.IsNullOrEmpty(config["description"])) {
                config.Remove("description");
                config.Add("description", "ViNull SQL Blog Provider");
            }

            base.Initialize(name, config);

            _applicationName = config["applicationName"];
            if (string.IsNullOrEmpty(_applicationName))
                _applicationName = String.IsNullOrEmpty(HttpContext.Current.Request.ApplicationPath) ? "/" : HttpContext.Current.Request.ApplicationPath;
            config.Remove("applicationName");

            _connectionStringName = config["connectionStringName"];
            if (string.IsNullOrEmpty(_connectionStringName))
                throw new ArgumentNullException("connectionStringName");
            config.Remove("connectionStringName");
            
            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0) {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }

        public override void SavePost(Post post) {
            BlogDataSetTableAdapters.PostTableAdapter taPost = new BlogDataSetTableAdapters.PostTableAdapter();
            taPost.Connection.ConnectionString = WebConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;

            if (post.PostID == Guid.Empty || post.PostID == null) {
                post.PostID = Guid.NewGuid();
                taPost.Insert(post.PostID, post.Title, post.Preview, post.Body, post.Author, post.AuthorUsername,
                              post.AuthotEmail, post.PostedOn, post.PostedOn, post.Draft, post.Link);
            }
            else {
                Post origional = GetPost(post.PostID);
                if (origional != null) {
                    taPost.Update(post.Title, post.Preview, post.Body, post.Author, post.AuthorUsername, post.AuthotEmail,
                                  post.PostedOn, post.LastModified, post.Draft, post.Link, origional.PostID, origional.PostedOn,
                                  origional.LastModified, origional.Draft);
                }
            }
        }

        public override void DeletePost(Guid postID) {
            throw new NotImplementedException();
        }

        public override Post GetPost(Guid postID) {
            throw new NotImplementedException();
        }

        public override Post GetPost(string link) {
            throw new NotImplementedException();
        }

        public override PostCollection GetPosts(int Limit, int Offset) {
            return new PostCollection();
        }

        public override PostCollection GetPostsByBlog(Guid blogID, int Limit, int Offset) {
            throw new NotImplementedException();
        }

        public override PostCollection GetPostsByCategory(Guid categoryID, int Limit, int Offset) {
            throw new NotImplementedException();
        }

        public override PostCollection GetPostsByTag(string tag, int Limit, int Offset) {
            throw new NotImplementedException();
        }

        public override void SaveBlog(Blog blog) {
            throw new NotImplementedException();
        }

        public override void DeleteBlog(Guid blogID) {
            throw new NotImplementedException();
        }

        public override Blog GetBlog(Guid blogID) {
            throw new NotImplementedException();
        }

        public override BlogCollection GetBlogs(int Limit, int Offset) {
            throw new NotImplementedException();
        }

        public override void SaveCategory(Category category) {
            throw new NotImplementedException();
        }

        public override void DeleteCategory(Guid categoryID) {
            throw new NotImplementedException();
        }

        public override Category GetCategory(Guid categoryID) {
            throw new NotImplementedException();
        }

        public override CategoryCollection GetCategories(int Limit, int Offset) {
            throw new NotImplementedException();
        }
    }
}
