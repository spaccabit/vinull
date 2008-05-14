using System;
using System.Windows;
using System.Windows.Controls;

namespace _1_SimpleApp {

    class SimpleApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new SimpleApp());
        }

        public SimpleApp() {
            Title = "Simple WPF App";
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
            SizeToContent = SizeToContent.WidthAndHeight;

            Label lHello = new Label();
            lHello.Content = "Hello WPF!";
            lHello.Padding = new Thickness(96);
            lHello.FontSize = 24;
            lHello.FontWeight = FontWeights.Bold;
            
            Content = lHello;
        }
    }
}
