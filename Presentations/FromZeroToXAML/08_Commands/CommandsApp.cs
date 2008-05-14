using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _8_Commands {
    class CommandsApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new CommandsApp());
        }

        protected TextBox tbText = null;

        public CommandsApp() {
            Title = "Of Buttons And Commands";
            FontSize = 24;
            FontWeight = FontWeights.Bold;
            SizeToContent = SizeToContent.WidthAndHeight;
           
            StackPanel spMain = new StackPanel();
            Content = spMain;

            Button b = new Button();
            b.Command = ApplicationCommands.Paste;
            b.Content = ApplicationCommands.Paste.Text;
            b.Padding = new Thickness(12);
            b.Margin = new Thickness(12);
            spMain.Children.Add(b);

            CommandBindings.Add(new CommandBinding(
                                    ApplicationCommands.Paste,
                                    PasteOnExecute, PasteCanExecute));

            tbText = new TextBox();
            tbText.Margin = new Thickness(12);
            tbText.Height = 300;
            tbText.Width = 400;
            spMain.Children.Add(tbText);

        }

        void PasteOnExecute(object sender, ExecutedRoutedEventArgs args) {
            tbText.Text = Clipboard.GetText();
        }

        void PasteCanExecute(object sender, CanExecuteRoutedEventArgs args) {
            args.CanExecute = Clipboard.ContainsText();
        }
    }
}
