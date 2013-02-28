using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace RGBLamp.Classes
{
    class RandomUpdateHandler : UpdateHandler
    {
        ApplicationState _state;
        ArduinoCommand _commander;
        DispatcherTimer _dispatcherTimer;
        double _targetRed;
        double _targetGreen;
        double _targetBlue;
        Random _rand = new Random();

        internal RandomUpdateHandler(ApplicationState state)
        {
            _state = state;
            _commander = new ArduinoCommand();
            _commander.UpdateColorValue(ArduinoCommand.Colors.Red, _state.Red);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Green, _state.Green);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, _state.Blue);


            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
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

        private bool ApproxEq(double val1, double val2)
        {
            double diff = val1 - val2;
            return Math.Abs(diff) < 25;
        }

        public override void Dispose()
        {
            _dispatcherTimer.Stop();
        }
    }
}
