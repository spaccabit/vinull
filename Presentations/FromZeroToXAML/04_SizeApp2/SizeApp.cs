using System;
using System.Windows;
using System.Windows.Controls;

namespace _4_SizeApp2 {
    class SizeApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new SizeApp());
        }

        public SizeApp() {
            Title = "Size Does Matter";
            FontSize = 24;
            FontWeight = FontWeights.Bold;
            SizeToContent = SizeToContent.WidthAndHeight;

            Button bSolo = new Button();
            bSolo.Content = "The Only Button";
            bSolo.Margin = new Thickness(48);
            bSolo.Padding = new Thickness(12);

            Content = bSolo;
        }
    }
}
