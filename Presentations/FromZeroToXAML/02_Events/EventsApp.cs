using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _2_Events {
    class EventsApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new EventsApp());
        }

        protected Label lMessage = null;
        protected Key kLastKey = Key.None;

        public EventsApp() {
            Title = "Events 101 App";
            SizeToContent = SizeToContent.Manual;
            Height = 300;
            Width = 400;

            MouseDown += new MouseButtonEventHandler(EventsApp_MouseDown);

            lMessage = new Label();
            lMessage.Content = "Hit a key, or click a mouse.";
            lMessage.PreviewMouseDown += new MouseButtonEventHandler(lMessage_PreviewMouseDown);

            Content = lMessage;
        }

        protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e) {
            base.OnKeyUp(e);

            lMessage.Content = e.Device.ToString() + "\n" + e.Key.ToString();
            kLastKey = e.Key;
        }

        void EventsApp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            lMessage.Content = e.Device.ToString() + "\n" + e.ChangedButton.ToString();
        }

        void lMessage_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            lMessage.Content = "Caught!";
            Title = "Proof"; 

            if (kLastKey == Key.M) {
                e.Handled = true;
            }
            
        }

    }
}
