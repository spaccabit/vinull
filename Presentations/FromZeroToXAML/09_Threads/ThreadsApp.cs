using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Net;
using System.IO;
using System.Windows.Threading;
using System.Threading;

namespace FotoViewer {
    class ThreadsApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new ThreadsApp());
        }

        StatusBarItem sbiMessage;
        Image imgDisplayed;

        public ThreadsApp() {
            Title = "FotoViewer";

            InputGestureCollection gesExit = new InputGestureCollection();
            gesExit.Add(new KeyGesture(Key.X, ModifierKeys.Control));
            RoutedUICommand comExit = new RoutedUICommand("E_xit", "Exit", GetType(), gesExit);
            CommandBindings.Add(new CommandBinding(comExit, CloseApp));

            Menu menu = new Menu();
            MenuItem fileMenu = new MenuItem();
            fileMenu.Header = "_File";
            MenuItem fileExitItem = new MenuItem();
            fileExitItem.Command = comExit;
            fileMenu.Items.Add(fileExitItem);
            menu.Items.Add(fileMenu);

            StatusBar statusBar = new StatusBar();
            sbiMessage = new StatusBarItem();
            sbiMessage.Content = "FotoViewer Ready";
            sbiMessage.HorizontalAlignment = HorizontalAlignment.Left;
            statusBar.Items.Add(sbiMessage);

            Grid gBody = new Grid();
            gBody.ColumnDefinitions.Add(new ColumnDefinition());
            gBody.ColumnDefinitions.Add(new ColumnDefinition());
            gBody.ColumnDefinitions[0].Width = GridLength.Auto;
            gBody.ColumnDefinitions[1].Width = new GridLength(100, GridUnitType.Star);

            TreeView tvPhotos = new TreeView();
            tvPhotos.Padding = new Thickness(0, 0, 5, 0);
            tvPhotos.SelectedItemChanged += tvPhotos_SelectedItemChanged;
            FillTree(tvPhotos);

            imgDisplayed = new Image();
            imgDisplayed.HorizontalAlignment = HorizontalAlignment.Center;
            imgDisplayed.VerticalAlignment = VerticalAlignment.Center;
            imgDisplayed.Margin = new Thickness(15);
            imgDisplayed.BitmapEffect = new DropShadowBitmapEffect();

            Grid.SetColumn(imgDisplayed, 1);
            gBody.Children.Add(tvPhotos);
            gBody.Children.Add(imgDisplayed);

            DockPanel dpMain = new DockPanel();
            DockPanel.SetDock(menu, Dock.Top);
            DockPanel.SetDock(statusBar, Dock.Bottom);
            dpMain.Children.Add(menu);
            dpMain.Children.Add(statusBar);
            dpMain.Children.Add(gBody);
            Content = dpMain;

            tvPhotos.Focus();
        }

        void CloseApp(Object sender, ExecutedRoutedEventArgs args) {
            if (MessageBoxResult.Yes ==
                MessageBox.Show("Really Exit?",
                                Title,
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question)
               ) Close();
        }

        private void FillTree(TreeView tvPhotos) {
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
