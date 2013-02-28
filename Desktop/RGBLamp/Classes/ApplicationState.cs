using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RGBLamp.Classes
{
    public class ApplicationState : INotifyPropertyChanged
    {

        public enum Mode
        {
            Random,
            Keyboard,
            Static
        }

        private Mode _currentMode;
        public Mode CurrentMode
        {
            get
            {
                return _currentMode;
            }
            set
            {
                _currentMode = value;
                RaisePropertyChanged("CurrentMode");
            }
        }

        private double _red;
        public double Red { get { return _red; } set { _red = value; RaisePropertyChanged("Red"); } }

        private double _green;
        public double Green { get { return _green; } set { _green = value; RaisePropertyChanged("Green"); } }

        private double _blue;
        public double Blue { get { return _blue; } set { _blue = value; RaisePropertyChanged("Blue"); } }

        private double _speed;
        public double Speed { get { return _speed; } set { _speed = value; RaisePropertyChanged("Speed"); } }


        public event PropertyChangedEventHandler PropertyChanged;


        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
