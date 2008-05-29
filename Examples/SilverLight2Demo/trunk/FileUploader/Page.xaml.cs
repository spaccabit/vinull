using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Security.Cryptography;
using System.IO;

namespace FileUploader {
    public partial class Page : UserControl {
        public static Dictionary<String, FileData> Files = new Dictionary<String, FileData>();
        public static String ClientID = null;
        private static HashAlgorithm hasher = new SHA1Managed();
        private static LocalService.FileServiceSoapClient svc = new LocalService.FileServiceSoapClient();
        private static Int32 chunkSize = 1024 * 128;

        public Page() {
            InitializeComponent();
            svc.SaveFileChunkCompleted += new EventHandler<FileUploader.LocalService.SaveFileChunkCompletedEventArgs>(svc_SaveFileChunkCompleted);
            svc.ProcessFileCompleted += new EventHandler<FileUploader.LocalService.ProcessFileCompletedEventArgs>(svc_ProcessFileCompleted);
            svc.GenerateClientIDCompleted += new EventHandler<FileUploader.LocalService.GenerateClientIDCompletedEventArgs>(svc_GenerateClientIDCompleted);

            svc.GenerateClientIDAsync();
        }

        void svc_GenerateClientIDCompleted(object sender, FileUploader.LocalService.GenerateClientIDCompletedEventArgs e) {
            ClientID = e.Result;
        }

        private void bChoose_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.EnableMultipleSelection = true;
            dlg.Filter = "All video files |*.avi;*.mov;*.wmv;*.mpg|AVI files (*.avi) |*.avi|All files |*.*";
            if (dlg.ShowDialog() == DialogResult.OK) {
                foreach (FileDialogFileInfo file in dlg.SelectedFiles) {

                    if (Files.ContainsKey(file.Name))
                        continue;

                    ContentControl item = new ContentControl();
                    Stream f = file.OpenRead();
                    FileData fdata = new FileData(file, item, f.Length, 0);
                    f.Close();
                    Files.Add(file.Name, fdata);
                    item.Style = (Style)Application.Current.Resources["FileControl"];
                    item.Content = fdata;
                    spFileList.Children.Add(item);
                }
            }
        }

        private void bClear_Click(object sender, RoutedEventArgs e) {
            spFileList.Children.Clear();
            Files.Clear();
        }

        private void bUpload_Click(object sender, RoutedEventArgs e) {

            foreach (KeyValuePair<String, FileData> f in Files) {
                if (!f.Value.InProgress) {
                    f.Value.InProgress = true;
                    f.Value.Status = "Sending";
                    f.Value.Refresh();
                    Dispatcher.BeginInvoke(new UploadFileStartHanler(UploadFileStart), new object[] { f.Value });
                    //Thread t = new Thread(new ParameterizedThreadStart(UploadFileStart));
                    //t.Start(f.Value);
                }
            }
        }

        private delegate void UploadFileStartHanler(object data);
        private void UploadFileStart(object data) {
            FileData f = data as FileData;
            Byte[] buffer = new Byte[chunkSize];

            using (Stream s = f.file.OpenRead()) {

                if (f.length <= chunkSize) {
                    s.Read(buffer, 0, Convert.ToInt32(f.length));
                    f.offset = Convert.ToInt32(f.length);
                }
                else {
                    s.Read(buffer, 0, chunkSize);
                    f.offset = chunkSize;
                }
                s.Close();
            }

            String hash = Convert.ToBase64String(hasher.ComputeHash(buffer));

            try {
                svc.SaveFileChunkAsync(ClientID, f.file.Name, buffer, 0, hash, f);
            }
            catch (Exception ex) {
                f.Status = ex.Message;
                f.content.Dispatcher.BeginInvoke(() => f.content.Content = f);
            }
        }

        void svc_SaveFileChunkCompleted(object sender, FileUploader.LocalService.SaveFileChunkCompletedEventArgs e) {
            FileData f = e.UserState as FileData;
            Int32 offset = f.offset;
            Byte[] buffer = new Byte[chunkSize];

            f.Progress = Math.Round(Convert.ToDecimal(f.offset) / Convert.ToDecimal(f.length), 2);

            if (e.Result && f.length > f.offset) {
                f.Status = "Sending";
                f.Refresh();
                using (Stream s = f.file.OpenRead()) {

                    s.Seek(offset, SeekOrigin.Begin);

                    if (f.length <= offset + chunkSize) {
                        s.Read(buffer, 0, (Convert.ToInt32(f.length - offset)));
                        f.offset = Convert.ToInt32(f.length);
                    }
                    else {
                        s.Read(buffer, 0, chunkSize);
                        f.offset = offset + chunkSize;
                    }
                    s.Close();
                }

                String hash = Convert.ToBase64String(hasher.ComputeHash(buffer));
                try {
                    svc.SaveFileChunkAsync(ClientID, f.file.Name, buffer, offset, hash, f);
                }
                catch (Exception ex) {
                    f.Status = ex.Message;
                    f.content.Dispatcher.BeginInvoke(() => f.content.Content = f);
                }

            }
            else if (!e.Result) {
                f.Status = "Resend";
                f.Refresh();

                using (Stream s = f.file.OpenRead()) {
                    f.failCount++;

                    // resend last chunk
                    offset = offset - chunkSize > 0 ? offset - chunkSize : 0;

                    s.Seek(offset, SeekOrigin.Begin);

                    if (f.length <= offset + chunkSize) {
                        s.Read(buffer, 0, (Convert.ToInt32(f.length - offset)));
                        f.offset = Convert.ToInt32(f.length);
                    }
                    else {
                        s.Read(buffer, 0, chunkSize);
                        f.offset = offset + chunkSize;
                    }
                    s.Close();
                }

                String hash = Convert.ToBase64String(hasher.ComputeHash(buffer));
                try {
                    svc.SaveFileChunkAsync(ClientID, f.file.Name, buffer, offset, hash, f);
                }
                catch (Exception ex) {
                    f.Status = ex.Message;
                    f.content.Dispatcher.BeginInvoke(() => f.content.Content = f);
                }
            }
            else {
                f.Status = "Processing";
                f.Refresh();
                svc.ProcessFileAsync(ClientID, f.Name, f);
            }
        }

        void svc_ProcessFileCompleted(object sender, FileUploader.LocalService.ProcessFileCompletedEventArgs e) {
            FileData f = e.UserState as FileData;
            if (e.Result) {
                f.Status = "Completed";
                f.Refresh();
            }
            else {
                f.Status = "Failed";
                f.Refresh();
            }
        }

        public class FileData : INotifyPropertyChanged {
            public FileDialogFileInfo file { get; set; }
            public ContentControl content { get; set; }
            public long length { get; set; }
            public Int32 offset { get; set; }
            public Int32 failCount { get; set; }
            public Boolean InProgress { get; set; }
            public String Name { get { return file.Name; } }
            public Decimal Progress { get; set; }
            public String Status { get; set; }

            public FileData(FileDialogFileInfo f, ContentControl c, long l, Int32 o) {
                file = f;
                content = c;
                length = l;
                offset = o;
                failCount = 0;
                Progress = 0M;
                InProgress = false;
                Status = String.Empty;
            }

            public void Refresh() {
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Progress"));
                }
            }

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion
        }
    }
}
