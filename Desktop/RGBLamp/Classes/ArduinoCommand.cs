using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Threading;

namespace RGBLamp
{
    public class ArduinoCommand: IDisposable
    {
        private SerialPort _port;
        public enum Colors
        {
            Red = 114, // char val for 'r'
            Green = 103, // char val for 'g'
            Blue = 98 // char val for 'b'
        }

        /// <summary>
        /// Sends command to arduino to change color value
        /// </summary>
        /// <param name="color">Color to be altered</param>
        /// <param name="value">New value, should be between 0 and 255</param>
        public void UpdateColorValue(Colors color, double value)
        {            
            string[] portlist = SerialPort.GetPortNames();
            
            foreach (String s in portlist)
            {
                try
                {                    
                    if (_port == null || !_port.IsOpen)
                    {
                        _port = new SerialPort(s, 9600, Parity.None, 8, StopBits.One);
                        _port.DataReceived += OnReceived;

                        _port.Open();
                    }

                    // Todo: Before sending data, we should do some sort of handshake to make sure it's actually the our program
                    
                    
                    string toSend = string.Format("{0};{1}", 
                        (char)((int)color),
                        ((int)value).ToString()
                    );
                                        
                    
                    // Serialze the data for transmission
                    byte[] data = CommandMessage(toSend);
                    _port.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    // todo error handling
                }                
            }
        }

        //This takes in a string and converts it to a byte array ready to be sent over serial
        private byte[] CommandMessage(string CMD)
        {            
            byte[] message = new byte[CMD.Length + 2]; //Add 2 for the terminal chars
            message[0] = BitConverter.GetBytes('~')[0]; //Add Start terminal char
            for (int i = 1; i < message.Length - 1; i++)
            {
                //loop through command to be sent
                message[i] = BitConverter.GetBytes(CMD[i - 1])[0];
            }
            message[message.Length - 1] = BitConverter.GetBytes('~')[0]; //Add end terminal char

            return message;
        }

        private void OnReceived(object sender, SerialDataReceivedEventArgs c)
        {
            // ToDo: handle incoming messages

            //string current = _port.ReadExisting();
            //Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            //{
            //    try
            //    {
            //        current = new String((from ch in current where ch != '?' select ch).ToArray());
            //        if (!string.IsNullOrWhiteSpace(current))
            //        {
            //            text.Text = current;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //            // todo error handling
            //    }
            //}));

        }

        public void Dispose()
        {
            if (_port != null)
            {
                _port.Close();
                _port.Dispose();
            }
        }
    }
}
