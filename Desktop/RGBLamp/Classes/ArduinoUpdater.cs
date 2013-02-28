using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows;

namespace RGBLamp.Classes
{
    /// <summary>
    /// Listens for events from application state and sends commands to arduino based on those event
    /// </summary>
    public class ArduinoUpdater
    {
        ApplicationState _state;        
        UpdateHandler _handler;

        public ArduinoUpdater(ApplicationState state)
        {
            _state = state;
            _state.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(state_PropertyChanged);
        }

        void state_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentMode")
            {
                if (_handler != null)
                {
                    _handler.Dispose();
                }
                _handler = UpdateHandlerFactory.GetHandler(_state);
            }                                    
        }
    }
}
