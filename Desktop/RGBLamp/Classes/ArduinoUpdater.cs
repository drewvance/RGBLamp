using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace RGBLamp.Classes
{
    /// <summary>
    /// Listens for events from application state and sends commands to arduino based on those event
    /// </summary>
    public class ArduinoUpdater
    {
        ApplicationState _state; 
        ArduinoCommand _commander;

        // for random
        DispatcherTimer _dispatcherTimer;
        double _targetRed;
        double _targetGreen;
        double _targetBlue;
        Random _rand = new Random();

        public ArduinoUpdater(ApplicationState state)
        {
            _commander = new ArduinoCommand();
            _state = state;
            _state.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(state_PropertyChanged);
        }

        void state_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (_state.CurrentMode)
            { 
                case ApplicationState.Mode.Random:
                    if (e.PropertyName == "CurrentMode")
                        InitRandom();
                    else
                        HandleRandom(e.PropertyName);
                    break;
                case ApplicationState.Mode.Static:
                    if (e.PropertyName == "CurrentMode")
                        InitStatic();
                    else
                        HandleStatic(e.PropertyName);
                    break;
                case ApplicationState.Mode.WarpCore:
                    if (e.PropertyName == "CurrentMode")
                        InitWarpCore();
                    else
                        HandleWarpCore(e.PropertyName);
                    break;
            }
        }

        private void InitWarpCore()
        {
            throw new NotImplementedException();
        }

        private void HandleWarpCore(string property)
        {
            throw new NotImplementedException();
        }

        private void InitRandom()
        {
            _commander.UpdateColorValue(ArduinoCommand.Colors.Red, _state.Red);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Green, _state.Green);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, _state.Blue);


            _dispatcherTimer= new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0,0,0,0,50);
            _dispatcherTimer.Start();


            _targetRed = _rand.Next(0, 256);
            _targetBlue = _rand.Next(0, 256);
            _targetGreen = _rand.Next(0, 256);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (ApproxEq(_targetRed, _state.Red))
            {
                _targetRed = _rand.Next(0, 256);   
            }
            _state.Red += (_targetRed - _state.Red) * _state.Speed;
            _commander.UpdateColorValue(ArduinoCommand.Colors.Red, _state.Red);

            if (ApproxEq(_targetBlue, _state.Blue))
            {
                _targetBlue = _rand.Next(0, 256);
            }
            _state.Blue += (_targetBlue - _state.Blue) * _state.Speed;
            _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, _state.Blue);

            if (ApproxEq(_targetGreen, _state.Green))
            {
                _targetGreen = _rand.Next(0, 256);
            }
            _state.Green += (_targetGreen - _state.Green) * _state.Speed;
            _commander.UpdateColorValue(ArduinoCommand.Colors.Green, _state.Green);
        }

        private void HandleRandom(string property)
        {
            
        }

        


        private void InitStatic()
        {
            _commander.UpdateColorValue(ArduinoCommand.Colors.Red, _state.Red);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Green, _state.Green);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, _state.Blue);

            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
            }
            
        }

        private void HandleStatic(string property)
        {
            switch (property)
            {
                case "Red":
                    _commander.UpdateColorValue(ArduinoCommand.Colors.Red, _state.Red);
                    break;
                case "Green":
                    _commander.UpdateColorValue(ArduinoCommand.Colors.Green, _state.Green);
                    break;
                case "Blue":
                    _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, _state.Blue);
                    break;
            }
        }

        private bool ApproxEq(double val1, double val2)
        {
            double diff = val1 - val2;
            return Math.Abs(diff) < 25;         
        }
    }
}
