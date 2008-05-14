using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace _7_AttachProp {
    class AttachPropApp : Window {

        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new AttachPropApp());
        }

        public AttachPropApp() {
            Title = "Hold on, I'm Comming!";

            DockPanel dpMain = new DockPanel();
            Content = dpMain;

            Menu m = new Menu();
            MenuItem mi = new MenuItem();
            MenuItem miNew = new MenuItem();
            mi.Header = "_Menu";
            miNew.Header = "_New";
            m.Items.Add(mi);
            mi.Items.Add(miNew);
            DockPanel.SetDock(m, Dock.Top);
            dpMain.Children.Add(m);

            ToolBar tb = new ToolBar();
            tb.Header = "Toolbar";
            tb.Margin = new Thickness(0, 5, 0, 0);
            DockPanel.SetDock(tb, Dock.Top);
            dpMain.Children.Add(tb);

            StatusBar sb = new StatusBar();
            StatusBarItem sbi = new StatusBarItem();
            sbi.Content = "Status";
            sb.Items.Add(sbi);
            DockPanel.SetDock(sb, Dock.Bottom);
            dpMain.Children.Add(sb);

            ListBox lb = new ListBox();
            ListBoxItem lbi = new ListBoxItem();
            lbi.Content = "List Box Item";
            lb.Items.Add(lbi);

            TextBox txtb = new TextBox();
            txtb.AcceptsReturn = true;
            txtb.Margin = new Thickness(6, 0, 0, 0);

            Grid gBody = new Grid();
            gBody.RowDefinitions.Add(new RowDefinition());
            gBody.ColumnDefinitions.Add(new ColumnDefinition());
            gBody.ColumnDefinitions.Add(new ColumnDefinition());

            gBody.ColumnDefinitions[0].Width = GridLength.Auto;

            GridSplitter spliter = new GridSplitter();
            spliter.Width = 6;
            spliter.HorizontalAlignment = HorizontalAlignment.Left;

            gBody.Children.Add(lb);
            gBody.Children.Add(spliter);
            gBody.Children.Add(txtb);

            Grid.SetColumn(txtb, 1);
            Grid.SetColumn(spliter, 1);
            dpMain.Children.Add(gBody);

            txtb.Focus();
        }
    }
}
