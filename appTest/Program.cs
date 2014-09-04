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
    public partial class Program
    {
        static ActionDevice button;
        static OutputDevice led;
        static HAL.Analog.InputDevice lightSensor;

        public static void Main()
        {
            Initialization();

            while (true)
            {
                MainLoop();
            }
        }

        public static void Initialization()
        {

            button = new ActionDevice(Pins.ONBOARD_SW1, true);
            led = new OutputDevice(Pins.ONBOARD_LED, false);
            lightSensor = new HAL.Analog.InputDevice(Pins.GPIO_PIN_A0, "LightSensor", true, 5, 3f, 150f);
            lightSensor.PercentageNotification = 0.5f;
            lightSensor.Debug = false;

            button.onAction += (d1, d2, time) =>
            {
                lightSensor.Debug = !lightSensor.Debug;
                led.Enabled = !led.Enabled;
            };

            lightSensor.onAnalogValueChange += (oldValue, newValue) => 
            {
                Debugging.Log.Info("Over", lightSensor.PercentageNotification * 10, " percent change detected in light. Old Value =", oldValue, "| NewValue =", newValue);
            };
        }

        

        public static void MainLoop()
        {
            lightSensor.Read();
        }

    }
}
