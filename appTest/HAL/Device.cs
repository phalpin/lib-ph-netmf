using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace appTest.HAL
{
    public abstract class Device
    {
        protected bool _enabled;
        protected Cpu.Pin _pin;
        protected PortType _mode;
        protected DeviceType _type;

        public virtual bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        public virtual Cpu.Pin Pin
        {
            get
            {
                return _pin;
            }
            set
            {
                _pin = value;
            }
        }
        
        public virtual PortType Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }
    }
}
