using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _5_Brushes {
    class BrushesApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new BrushesApp());
        }

        public BrushesApp() {
            Title = "Brush Your Shoulders Off";
            FontSize = 24;
            FontWeight = FontWeights.Bold;
            SizeToContent = SizeToContent.WidthAndHeight;

            Background = new LinearGradientBrush(Colors.PaleGreen, Colors.DarkGreen, 90);

            StackPanel spMain = new StackPanel();
            Content = spMain;

            Button bFirst = new Button();
            bFirst.Content = "Button Uno";
            bFirst.Margin = new Thickness(48);
            bFirst.Padding = new Thickness(12);
            bFirst.Foreground = Brushes.RoyalBlue;
            bFirst.Background = Brushes.RosyBrown;
            spMain.Children.Add(bFirst);

            Button bSecond = new Button();
            bSecond.Content = "Button Deux";
            bSecond.Margin = new Thickness(48);
            bSecond.Padding = new Thickness(12);
            bSecond.Background = new RadialGradientBrush(Colors.Goldenrod, Colors.Red);
            spMain.Children.Add(bSecond);

            Button bThird = new Button();
            bThird.Content = "Button Tri";
            bThird.Margin = new Thickness(48);
            bThird.Padding = new Thickness(12);
            bThird.Background = new VisualBrush(bSecond);
            spMain.Children.Add(bThird);

        }

    }
}
