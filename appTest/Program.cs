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
using appTest.HAL.Digital.DistanceSensors;

namespace appTest
{
    public partial class Program
    {
        static ActionDevice button;
        static OutputDevice led;
        static OutputDevice speaker;
        static HC_SR04 distanceSensor;

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
            distanceSensor = new HC_SR04(Pins.GPIO_PIN_D0, Pins.GPIO_PIN_D3, true, 20, 2.0F);
            speaker = new OutputDevice(Pins.GPIO_PIN_D6, false);

            button.onAction += (d1, d2, time) =>
            {
                led.Enabled = !led.Enabled;
                distanceSensor.Debug = !distanceSensor.Debug;
            };

            distanceSensor.onMeasurementReceived += (oldVal, newVal) =>
            {
                Debugging.Log.Info("New Reading: ", newVal, "cm");
                speaker.Enabled = newVal > 20;
            };
        }

        

        public static void MainLoop()
        {
            distanceSensor.Render();
        }

    }
}
