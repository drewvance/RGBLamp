using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace RGBLamp.Classes
{
    internal class KeyboardUpdateHandler:UpdateHandler
    {
        private static KeyboardUpdateHandler _instance;
        ApplicationState _state;
        ArduinoCommand _commander;
        DispatcherTimer _dispatcherTimer;
        int _hitCount;
        internal KeyboardUpdateHandler(ApplicationState state)
        {
            // Listen for name change changes across all processes/threads on current desktop...
            IntPtr hhook = SetWinEventHook(EVENT_OBJECT_NAMECHANGE, EVENT_OBJECT_NAMECHANGE, IntPtr.Zero,
                    procDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);

            _state = state;
            _instance = this;
            _commander = new ArduinoCommand();
            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0,50);
            _dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_hitCount == 0)
            {
                _state.Red++;
                _state.Green--;
                _state.Blue = 255;
                    
                _commander.UpdateColorValue(ArduinoCommand.Colors.Red, _state.Red );
                _commander.UpdateColorValue(ArduinoCommand.Colors.Green, _state.Green);
                _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, _state.Blue);
            }

            _hitCount = 0;
        }

        private void Hit()
        {
            SetRed();
            _hitCount++;
        }

        private void SetRed()
        {
            _state.Red = 0;
            _state.Green = 255;
            _state.Blue = 255;
                    

            _commander.UpdateColorValue(ArduinoCommand.Colors.Red, 0);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Green, 255);
            _commander.UpdateColorValue(ArduinoCommand.Colors.Blue, 255);
        }

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
       IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr
           hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess,
           uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        const uint EVENT_OBJECT_NAMECHANGE = 0x800C;
        const uint WINEVENT_OUTOFCONTEXT = 0;

        // Need to ensure delegate is not collected while we're using it,
        // storing it in a class field is simplest way to do this.
        static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);

        static void WinEventProc(IntPtr hWinEventHook, uint eventType,
            IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType == 32780 && _instance != null)
            {
                _instance.Hit();
            }
        }

        public override void Dispose()
        {
            _instance = null;
            _dispatcherTimer.Stop();
        }
    }
}
