using System;
using System.Windows;
using System.Windows.Controls;

namespace _6_DepProp {
    class DepPropApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new DepPropApp());
        }

        public DepPropApp() {
            Title = "Lean on Me";
            SizeToContent = SizeToContent.WidthAndHeight;
            FontWeight = FontWeights.Bold;
            FontSize = 16;

            StackPanel spMain = new StackPanel();
            Content = spMain;

            double[] fontsizes = { 16, 32 };
            foreach (double fontsize in fontsizes) {
                Button bWin = new Button();
                bWin.Content = "Window " + fontsize.ToString();
                bWin.Padding = new Thickness(12);
                bWin.Margin = new Thickness(48);
                bWin.Tag = fontsize;
                bWin.Click += new RoutedEventHandler(bClickWin);
                spMain.Children.Add(bWin);

                Button bButton = new Button();
                bButton.Content = "Button " + fontsize.ToString();
                bButton.Padding = new Thickness(12);
                bButton.Margin = new Thickness(48);
                bButton.Tag = fontsize;
                bButton.Click += new RoutedEventHandler(bClickButton);
                spMain.Children.Add(bButton);
            }
        }

        void bClickWin(object sender, RoutedEventArgs e) {
            Button b = e.Source as Button;
            FontSize = (double)b.Tag;
        }

        void bClickButton(object sender, RoutedEventArgs e) {
            Button b = e.Source as Button;
            b.FontSize = (double)b.Tag;
        }
    }
}
