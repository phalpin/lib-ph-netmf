using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using SecretLabs.NETMF.Hardware;
using appTest.HAL.Delegates;

namespace appTest.HAL.Analog
{
    public class InputDevice : AnalogDevice
    {

        private float _percentageNotification;
        private float _deltaComparison;

        public int Samples { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        public float PercentageNotification
        {
            get
            {
                return _percentageNotification;
            }
            set
            {
                float totalRange = MaxValue - MinValue;
                _deltaComparison = (float)System.Math.Abs(totalRange * value);
                _percentageNotification = value;
            }
        }

        public event AnalogValueChanged onAnalogValueChange;

        protected void AnalogValueChanged(float newVal)
        {
            if (onAnalogValueChange != null)
            {
                try
                {
                    onAnalogValueChange(CurrentValue, newVal);
                }
                catch (Exception ex)
                {
                    Debugging.Log.Error("Exception within AnalogValueChanged Handler:", ex.Message);
                }
            }
        }

        /// <summary>
        /// The analog input device constructor.
        /// </summary>
        /// <param name="pin">Pin this item belongs on.</param>
        /// <param name="initialState">Whether or not we should be taking readings.</param>
        public InputDevice(Cpu.Pin pin, string name, bool initialState, int samples, float minValue, float maxValue) : base(pin, PortType.Input, initialState, name)
        {
            _inputPort = new SecretLabs.NETMF.Hardware.AnalogInput(pin);
            Samples = samples;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override void Read()
        {
            if(Enabled)
            {
                base.Read();
                float sum = 0;
                for(int i=0;i<Samples;i++)
                {
                     sum += _inputPort.Read();
                }

                float newValue = (sum / (float)Samples);

                if (ShouldAlert(CurrentValue, newValue))
                {
                    AnalogValueChanged(newValue);
                }

                CurrentValue = (sum / (float)Samples);
            }

        }

        protected override string AdditionalInfo()
        {
            return "Delta Comparison: " + _deltaComparison + ", Percentage Notification: " + PercentageNotification;
        }

        protected bool ShouldAlert(float oldValue, float newValue)
        {
            double oldRanking = System.Math.Floor(oldValue / _deltaComparison);
            double newRanking = System.Math.Floor(newValue / _deltaComparison);
            return newRanking != oldRanking;
        }

    }
}
