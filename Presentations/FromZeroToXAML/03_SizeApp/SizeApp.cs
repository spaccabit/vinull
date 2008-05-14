using System;
using System.Windows;
using System.Windows.Controls;

namespace _3_SizeApp {
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

            Button bSolo = new Button();
            bSolo.Content = "The Only Button";

            Content = bSolo;
        }

    }
}
