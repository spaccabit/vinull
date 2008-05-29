using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.IO;

[WebService(Namespace = "http://SilverLight2Demmo/FileService")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class FileService : System.Web.Services.WebService {

    private static HashAlgorithm hasher = new SHA1Managed();

    [WebMethod(EnableSession = true)]
    public string GenerateClientID() {
        return HttpContext.Current.Session.SessionID;
    }

    [WebMethod]
    public Boolean SaveFileChunk(String ClientID, String FileName, Byte[] Data, Int32 Offset, String CheckSum) {
        String hash = Convert.ToBase64String(hasher.ComputeHash(Data));
        if (!hash.Equals(CheckSum))
            return false;

        String saveName = Path.Combine(HttpContext.Current.Request.MapPath("~/FileUploader/Incoming"),
                                       ClientID + FileName);

        try {
            using (FileStream fs = File.Open(saveName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) {
                fs.Seek(Offset, SeekOrigin.Begin);
                fs.Write(Data, 0, Data.Length);
                fs.Close();
            }
        }
        catch (Exception ex) {
            return false;
        }
        return true;
    }

    [WebMethod]
    public Boolean ProcessFile(String ClientID, String FileName) {
        String saveName = Path.Combine(HttpContext.Current.Request.MapPath("~/FileUploader/Incoming"),
                                       ClientID + FileName);
        try {
            File.Delete(saveName);
        }
        catch {
            return false;
        }
        return true;
    }
}

