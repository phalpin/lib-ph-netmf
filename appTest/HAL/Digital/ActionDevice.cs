using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace appTest.HAL.Digital
{
    public class ActionDevice : DigitalDevice
    {
        protected InterruptPort _interruptPort = null;

        public event NativeEventHandler onAction;

        protected void ActionOccurred(uint val1, uint val2, DateTime time)
        {
            if (onAction != null && Enabled)
            {
                try
                {
                    onAction(val1, val2, time);
                }
                catch (Exception ex)
                {
                    Debugging.Log.Error("[ActionDevice][ActionOccurred] Error Occurred:", ex.Message);
                }
            }
        }

        public ActionDevice(Cpu.Pin pin, bool initialState) : base(pin, PortType.Input, initialState)
        {
            _interruptPort = new InterruptPort(pin, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeHigh);
            _interruptPort.OnInterrupt += ActionOccurred;
        }

        


    }
}
