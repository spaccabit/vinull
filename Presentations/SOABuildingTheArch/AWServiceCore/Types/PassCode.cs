using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace AWServiceCore.Types {
    [XmlRoot(ElementName = "PassCodes")]
    public class PassCodeCollection : List<PassCode> { }

    public class PassCode {

        private string _key;
        private string _user;

        [XmlAttribute()]
        public string Key {
            get { return _key; }
            set { _key = value; }
        }

        [XmlAttribute()]
        public string User {
            get { return _user; }
            set { _user = value; }
        }

        public string Code {
            get {
                Char[] chars = _key.ToCharArray();
                Array.Reverse(chars);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                ASCIIEncoding encoder = new ASCIIEncoding();
                return Convert.ToBase64String(md5.ComputeHash(encoder.GetBytes(chars))).Replace("=", "");
            }
        }
    }
}
