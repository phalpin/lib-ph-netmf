using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace appTest.HAL.Analog
{
    public abstract class AnalogDevice : Device
    {

        protected SecretLabs.NETMF.Hardware.AnalogInput _inputPort = null;

        public string Name { get; set; }
        public float CurrentValue { get; set; }

        public AnalogDevice(Cpu.Pin pin, PortType type, bool initialState, string name)
        {
            Pin = pin;
            Mode = type;
            Enabled = initialState;
            Name = name;
            _type = DeviceType.Analog;
        }

        protected void DisposePorts()
        {
            if (_inputPort != null) _inputPort.Dispose();
        }

        public virtual void Read()
        {
            if (Debug)
            {
                Debugging.Log.Info(Name, "current Value:", CurrentValue);
                Debugging.Log.Info("Additional Info:", AdditionalInfo());
            }
        }

        protected virtual string AdditionalInfo()
        {
            return "N/A";
        }
    }
}
