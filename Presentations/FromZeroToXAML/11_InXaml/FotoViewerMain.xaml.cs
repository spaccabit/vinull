using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.Windows.Threading;

namespace _11_InXaml {
    /// <summary>
    /// Interaction logic for FotoViewerMain.xaml
    /// </summary>
    public partial class FotoViewerMain : Window {

        public static RoutedUICommand Exit = new RoutedUICommand("E_xit", "Exit", typeof(Window));

        public FotoViewerMain() {
            Exit.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));
            InitializeComponent();
            FillTree();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (e.Command.Equals(Exit)) {
                if (MessageBoxResult.Yes == MessageBox.Show("Really Exit?",
                    Title, MessageBoxButton.YesNo, MessageBoxImage.Question))
                    Close();
            }
        }

        private void FillTree() {
            Dictionary<string, string[]> sites = new Dictionary<string, string[]>();
            sites.Add("I Can Has Cheezeburger?", new string[] { 
                "notwantdecaf.jpg",
                "surrender.jpg",
                "no-payn.jpg",
                "lolcat_this_is_mah_job.jpg"
            });
            sites.Add("ViNull Photos", new string[] {
                "233396598_3d60aae5e7_b.jpg",
                "233443569_6c1ea21bb2_b.jpg",
                "232827160_0b2d295f9a_b.jpg"
            });
            sites.Add("Charles Petzold", new string[] {
                "PetzoldTattoo.jpg",
                "wpf.png"
            });

            foreach (string site in sites.Keys) {
                TreeViewItem tviSite = new TreeViewItem();
                tviSite.Header = site;
                tviSite.IsExpanded = true;
                tvPhotos.Items.Add(tviSite);

                foreach (string link in sites[site]) {
                    TreeViewItem tviImage = new TreeViewItem();
                    tviImage.Tag = link;
                    tviImage.Header = link;
                    tviSite.Items.Add(tviImage);
                }
            }
        }

        String selectedLink = String.Empty;
        void tvPhotos_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            TreeViewItem item = e.NewValue as TreeViewItem;
            if (item != null) {
                String link = item.Tag as String;
                if (link != null) {
                    selectedLink = link;
                    sbiMessage.Content = "Loading: " + link;
                    LoadImageHandler limg = new LoadImageHandler(LoadImage);
                    limg.BeginInvoke(link, null, null);
                }
            }
        }

        delegate void LoadImageHandler(String link);
        void LoadImage(String link) {
            Thread.Sleep(3000);

            MemoryStream memory = new MemoryStream();
            Stream imgData = File.OpenRead(link);

            //Uri uri = new Uri(link);
            //WebClient web = new WebClient();
            //Stream imgData = web.OpenRead(uri);

            int data = imgData.ReadByte();
            while (data != -1) {
                memory.WriteByte((byte)data);
                data = imgData.ReadByte();
            }
            memory.Seek(0, SeekOrigin.Begin);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = memory;
            bi.EndInit();
            bi.Freeze();

            UpdateImageUIHandler upUI = new UpdateImageUIHandler(UpdateImageUI);
            object[] args = { link };
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, upUI, bi, args);

            //web.Dispose();
        }

        delegate void UpdateImageUIHandler(BitmapImage imgData, String link);
        void UpdateImageUI(BitmapImage imgData, String link) {
            if (link.Equals(selectedLink)) {
                imgDisplayed.Source = imgData;
                sbiMessage.Content = link;
            }
        }
    }
}
