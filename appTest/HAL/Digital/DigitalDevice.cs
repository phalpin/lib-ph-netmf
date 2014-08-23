using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using SecretLabs.NETMF.Hardware;

namespace appTest.HAL.Digital
{
    public abstract class DigitalDevice : Device
    {
        protected OutputPort _outputPort = null;
        protected InputPort _inputPort = null;

        public DigitalDevice(Cpu.Pin pin, PortType type, bool initialState)
        {
            Pin = pin;
            Mode = type;
            Enabled = initialState;
        }

        protected void DisposePorts()
        {
            if (_outputPort != null) _outputPort.Dispose();
            if (_inputPort != null) _inputPort.Dispose();
        }
    }
}
