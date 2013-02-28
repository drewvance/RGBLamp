using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGBLamp.Classes
{
    internal abstract class UpdateHandler: IDisposable
    {
        public abstract void  Dispose();        
    }

    internal static class UpdateHandlerFactory
    { 
         internal static UpdateHandler GetHandler(ApplicationState state)
         {
             switch (state.CurrentMode)
            { 
                case ApplicationState.Mode.Random:
                    return new RandomUpdateHandler(state);
                 case ApplicationState.Mode.Keyboard:
                    return new KeyboardUpdateHandler(state);
                 default:
                    return new StaticUpdateHandler(state);                
            }
         }
    }
}
