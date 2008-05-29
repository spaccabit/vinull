using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Configuration.Provider;
using System.ComponentModel;

namespace ViNull.Blog {

    [DataObject(true)]
    public class BlogService {
        private static BlogProvider _provider = null;
        private static BlogProviderCollection _providers = null;
        private static object _lock = new object();

        public BlogProvider Provider {
            get { return _provider; }
        }

        public BlogProviderCollection Providers {
            get { return _providers; }
        }

        public BlogService() {
            LoadProviders();
        }

        private static void LoadProviders() {
            if (_provider == null) {
                lock (_lock) {
                    if (_provider == null) {
                        BlogConfigurationSection section = 
                            (BlogConfigurationSection)WebConfigurationManager.GetSection("system.web/blogService");

                        _providers = new BlogProviderCollection();
                        ProvidersHelper.InstantiateProviders(section.Providers, _providers, typeof(BlogProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException("Unable to load default Blog Provider");
                    }
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void SavePost(Post post) {
            _provider.SavePost(post);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public PostCollection GetPosts(int Limit, int Offset) {
            return _provider.GetPosts(Limit, Offset);
        }

    }
}