using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace appTest.HAL
{
    public class PinManager
    {

        /// <summary>
        /// Instance of the pin manager.
        /// </summary>
        protected static PinManager _instance;

        /// <summary>
        /// Returns the instance of the PinManager
        /// </summary>
        public PinManager Instance
        {
            get
            {
                if (_instance == null) _instance = new PinManager();
                return _instance;
            }
        }

        /// <summary>
        /// Constructor for the PinManager.
        /// </summary>
        public PinManager() { }

        public void Write(Cpu.Pin pin, bool val)
        {

        }


    }
}
