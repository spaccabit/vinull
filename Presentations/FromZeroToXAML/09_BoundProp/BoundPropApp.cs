using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace _9_BoundProp {
    class BoundPropApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new BoundPropApp());
        }

        public BoundPropApp() {
            Title = "I can't quit you...";
            FontSize = 24;
            FontWeight = FontWeights.Bold;
            SizeToContent = SizeToContent.WidthAndHeight;
            StackPanel spMain = new StackPanel();
            Content = spMain;

            Button b = new Button();
            CheckBox cb = new CheckBox();
            spMain.Children.Add(cb);
            spMain.Children.Add(b);

            cb.Content = "Is Locked";
            cb.Padding = new Thickness(12);
            cb.Margin = new Thickness(24);
            cb.SetBinding(CheckBox.IsCheckedProperty, "Topmost");
            cb.DataContext = this;

            b.Content = "Lock On _Top";
            b.Padding = new Thickness(12);
            b.Margin = new Thickness(24);
            b.Click += new RoutedEventHandler(b_Click);
        }

        void b_Click(object sender, RoutedEventArgs e) {
            Topmost = Topmost ? false : true;
        }
    }
}
