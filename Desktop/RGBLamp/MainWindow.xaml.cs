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
using System.IO.Ports;
using System.Windows.Threading;
using System.Threading;

namespace RGBLamp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArduinoCommand _commandSender;

        public MainWindow()
        {
            _commandSender = new ArduinoCommand();
            InitializeComponent();

            // set values on start
            _commandSender.UpdateColorValue(ArduinoCommand.Colors.Red, red.Value);
            _commandSender.UpdateColorValue(ArduinoCommand.Colors.Green, blue.Value);
            _commandSender.UpdateColorValue(ArduinoCommand.Colors.Blue, green.Value);

            
        }

        private void red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _commandSender.UpdateColorValue(ArduinoCommand.Colors.Red, e.NewValue);
        }

        private void blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _commandSender.UpdateColorValue(ArduinoCommand.Colors.Blue, e.NewValue);
        }

        private void green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _commandSender.UpdateColorValue(ArduinoCommand.Colors.Green, e.NewValue);
        }
    }
}
