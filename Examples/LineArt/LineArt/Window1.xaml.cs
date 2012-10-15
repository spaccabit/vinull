using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LineArt {
    
    public partial class Window1 : Window {

        List<Point> art = new List<Point>();

        public Window1() {
            InitializeComponent();
        }

        public void DrawLineArt(Point start, Point center, Point end, Int32 steps, Canvas graph) {
            Line source = new Line() { X1 = center.X, Y1 = center.Y, X2 = start.X, Y2 = start.Y, Stroke = Brushes.Black, StrokeThickness = 1 };
            Line target = new Line() { X1 = center.X, Y1 = center.Y, X2 = end.X, Y2 = end.Y, Stroke = Brushes.Black, StrokeThickness = 1 };

            graph.Children.Add(source);
            graph.Children.Add(target);

            for (Int32 i = 1; i <= steps; i++) {
                Line connection = new Line() { Stroke = Brushes.Black, StrokeThickness = 1 };
                connection.X1 = source.X1 + (source.X2 - source.X1) / (Double)steps * i;
                connection.Y1 = source.Y1 + (source.Y2 - source.Y1) / (Double)steps * i;
                connection.X2 = target.X1 + (target.X2 - target.X1) / (Double)steps * ((Double)steps - i);
                connection.Y2 = target.Y1 + (target.Y2 - target.Y1) / (Double)steps * ((Double)steps - i);
                graphArea.Children.Add(connection);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);

            Point p = e.MouseDevice.GetPosition(graphArea);
            art.Add(p);

            if (art.Count == 3) {
                DrawLineArt(art[0], art[1], art[2], 10, graphArea);
                art.Clear();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);

            if (e.Key == Key.Escape)
                graphArea.Children.Clear();
        }
    }
}
