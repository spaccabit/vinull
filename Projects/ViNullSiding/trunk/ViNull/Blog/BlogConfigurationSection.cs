using System;
using System.Configuration;

namespace ViNull.Blog {
    public class BlogConfigurationSection : ConfigurationSection {
        
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "SqlBlogProvider")]
        public string DefaultProvider {
            get { return (string)base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }
    }
}
