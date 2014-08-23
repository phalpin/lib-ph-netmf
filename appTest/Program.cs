using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using appTest.HAL.Digital;
using appTest.HAL;

namespace appTest
{
    public class Program
    {
        public static void Main()
        {
            ActionDevice button = new ActionDevice(Pins.ONBOARD_SW1, true);
            OutputDevice led = new OutputDevice(Pins.ONBOARD_LED, false);
            button.onAction += (d1,d2,time) =>
            {
                Debugging.Log.Info("D1:", d1, "D2:", d2, "- At Time:", time);
                Debugging.Log.Info("Turning light", led.Enabled ? "off" : "on");
                led.Enabled = !led.Enabled;
            };
            Thread.Sleep(Timeout.Infinite);
        }

    }
}
