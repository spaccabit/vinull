using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;

namespace ViNull.Blog {

    public abstract class BlogProvider : ProviderBase {

        public abstract String ApplicationName { get; set; }

        public abstract void SavePost(Post post);
        public abstract void DeletePost(Guid postID);

        public abstract Post GetPost(Guid postID);
        public abstract Post GetPost(String link);

        public abstract PostCollection GetPosts(Int32 Limit, Int32 Offset);
        public abstract PostCollection GetPostsByBlog(Guid blogID, Int32 Limit, Int32 Offset);
        public abstract PostCollection GetPostsByCategory(Guid categoryID, Int32 Limit, Int32 Offset);
        public abstract PostCollection GetPostsByTag(String tag, Int32 Limit, Int32 Offset);

        public abstract void SaveBlog(Blog blog);
        public abstract void DeleteBlog(Guid blogID);
        public abstract Blog GetBlog(Guid blogID);
        public abstract BlogCollection GetBlogs(Int32 Limit, Int32 Offset);

        public abstract void SaveCategory(Category category);
        public abstract void DeleteCategory(Guid categoryID);
        public abstract Category GetCategory(Guid categoryID);
        public abstract CategoryCollection GetCategories(Int32 Limit, Int32 Offset);

    }

    public class BlogProviderCollection : ProviderCollection {
        public new BlogProvider this[string name] {
            get { return (BlogProvider)base[name]; }
        }

        public override void Add(ProviderBase provider) {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (!(provider is BlogProvider))
                throw new ArgumentException
                    ("Invalid provider type", "provider");

            base.Add(provider);
        }
    }
}
