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
using RGBLamp.Classes;
using System.ComponentModel;

namespace RGBLamp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArduinoCommand _commandSender;
        private ArduinoUpdater _listener; 

        public MainWindow()
        {
            _commandSender = new ArduinoCommand();
            InitializeComponent();
            ApplicationState state = new ApplicationState();
            this.DataContext = state;
            _listener = new ArduinoUpdater(state);
        }
    }
}
