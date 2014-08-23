using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace appTest.HAL.Digital
{
    public class OutputDevice : DigitalDevice
    {
        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                if(_outputPort != null) _outputPort.Write(value);
                base.Enabled = value;
            }
        }

        public OutputDevice(Cpu.Pin pin, bool initialState) : base(pin, PortType.Output, initialState)
        {
            _outputPort = new OutputPort(pin, initialState);
        }
    }
}