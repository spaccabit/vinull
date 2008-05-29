﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace FileUploader {
    public partial class App : Application {

        public App() {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e) {
            // Load the main control
            this.RootVisual = new Page();
        }

        private void Application_Exit(object sender, EventArgs e) {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {

        }
    }

    public class NumberFormatter : IValueConverter {
        public object Convert(object value, Type targetType, object
            parameter, System.Globalization.CultureInfo culture) {
            try {
                return System.Convert.ToDecimal(value).ToString(parameter as string);
            }
            catch { }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) {
            return null;
        }
    }
}
