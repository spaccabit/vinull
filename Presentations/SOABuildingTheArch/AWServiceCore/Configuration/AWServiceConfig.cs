using System;
using System.Configuration;

namespace AWServiceCore.Configuration {

    public class AWServiceSection : ConfigurationSection {

        [ConfigurationProperty("connectionStringName", IsRequired = true)]
        public String ConnectionStringName {
            get { return (string)base["connectionStringName"]; }
            set { base["connectionStringName"] = value; }
        }
    }
}
