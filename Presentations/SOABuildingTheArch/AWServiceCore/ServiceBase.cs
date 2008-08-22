using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Threading;
using System.Configuration;

namespace AWServiceCore {

    public class ServiceBase {

        public static void SerializeObject(String FileName, Type ObjectType, Object Data) {
            Double timeout = 5000;

            StringBuilder xmlData = new StringBuilder();
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(String.Empty, String.Empty);

            StringWriterUTF8 stringWriter = new StringWriterUTF8(xmlData);
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
            XmlSerializer xmlSerial = new XmlSerializer(ObjectType);
            xmlSerial.Serialize(xmlWriter, Data, xsn);
            xmlWriter.Close();

            DateTime start = DateTime.Now;
            Exception lastEx = null;
            Boolean success = false;

            while (DateTime.Now < start.AddMilliseconds(timeout)) {
                try {
                    using (FileStream fs = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
                        StreamWriter writer = new StreamWriter(fs);
                        writer.Write(xmlData.ToString());
                        writer.Close();
                        success = true;
                        break;
                    }
                }
                catch (Exception ex) {
                    lastEx = ex;
                    Thread.Sleep(10);
                }
            }

            if (!success) throw lastEx;
        }

        public static object DeserializeObject(String FileName, Type ObjectType) {
            Double timeout = 5000;
            String data = String.Empty;

            DateTime start = DateTime.Now;
            Exception lastEx = null;
            Boolean success = false;

            while (DateTime.Now < start.AddMilliseconds(timeout)) {
                try {
                    using (FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                        StreamReader reader = new StreamReader(fs);
                        data = reader.ReadToEnd();
                        fs.Close();
                        success = true;
                        break;
                    }
                }
                catch (Exception ex) {
                    lastEx = ex;
                    Thread.Sleep(10);
                }
            }

            if (!success) throw lastEx;

            StringReader stringReader = new StringReader(data);
            XmlTextReader xmlReader = new XmlTextReader(stringReader);
            XmlSerializer xmlSerial = new XmlSerializer(ObjectType);
            return xmlSerial.Deserialize(xmlReader);
        }

        protected class StringWriterUTF8 : StringWriter {
            public override Encoding Encoding {
                get { return Encoding.UTF8; }
            }
            public StringWriterUTF8() : base() { }
            public StringWriterUTF8(StringBuilder sb) : base(sb) { }
        }

        public static Types.PassCodeCollection LoadPassCodes(String passcodeFile) {
            Types.PassCodeCollection codes = null;
            try {
                codes = (Types.PassCodeCollection)ServiceBase.DeserializeObject(passcodeFile, typeof(Types.PassCodeCollection));
            }
            catch (Exception ex) {
                throw new Exception("Error loading passcodes: " + ex.Message, ex);
            }
            return codes;
        }

        protected static String ConnectionString {

            get {
                Configuration.AWServiceSection config = ConfigurationManager.GetSection("AWServiceSection") as Configuration.AWServiceSection;
                if (config == null)
                    throw new Exception("AWServiceSection section not found in config.");
                return ConfigurationManager.ConnectionStrings[config.ConnectionStringName].ConnectionString;
            }
        }

    }
}
