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
        static HC_SR04 distanceSensor;
        static PWM speaker;

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
            distanceSensor = new HC_SR04(Pins.GPIO_PIN_D0, Pins.GPIO_PIN_D3, true, 40, 2.0F);
            //Pin, Freq, Duty Cycle
            speaker = new PWM(SecretLabs.NETMF.Hardware.NetduinoPlus.PWMChannels.PWM_PIN_D6, 600, 0, false);
            speaker.Start();

            button.onAction += (d1, d2, time) =>
            {
                led.Enabled = !led.Enabled;
                distanceSensor.Debug = !distanceSensor.Debug;
            };

            distanceSensor.onMeasurementReceived += (oldVal, newVal) =>
            {
                Debugging.Log.Info("New Reading: ", newVal, "cm");

                SetSpeaker(newVal);
                
                //if (newVal.InRange(0,20))
                //{
                //    SetSpeaker(300, 0.1);
                //}
                //else if (newVal.InRange(20, 40))
                //{
                //    SetSpeaker(300, 0.05);
                //}
                //else if (newVal.InRange(40, 80))
                //{
                //    SetSpeaker(300, 0.01);
                //}
                //else if (newVal.InRange(80, 100))
                //{
                //    SetSpeaker(300, 0.005);
                //}
                //else if (newVal.InRange(100, 120))
                //{
                //    SetSpeaker(300, 0.001);
                //}
                //else
                //{
                //    speaker.Stop();
                //}
            };

            
        }

        public static void SetSpeaker(float newVal){
            if (newVal >= 120)
            {
                speaker.Stop();
            }
            else if (newVal <= 1)
            {
                speaker.Start();
                speaker.DutyCycle = 0.1;
            }
            else
            {
                speaker.Start();
                speaker.DutyCycle = (120 - newVal) * 0.0008333F;
            }
        }

        

        public static void MainLoop()
        {
            distanceSensor.Render();
        }

    }
}
