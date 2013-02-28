using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGBLamp.Classes
{
    class StaticUpdateHandler : UpdateHandler
    {
        ApplicationState _state;
        ArduinoCommand _commander;
        internal StaticUpdateHandler(ApplicationState state)
        {
            _state = state;
            _commander = new ArduinoCommand();
            _commander.UpdateColorValue(ArduinoCommand.Colors.Red, _state.Red);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Green, _state.Green);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, _state.Blue);

            _state.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(state_PropertyChanged);
        }

        void state_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
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



        public override void Dispose()
        {
         
        }
    }
}
