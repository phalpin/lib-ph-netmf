using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace appTest.HAL.Digital.DistanceSensors
{
    public delegate void MeasurementReceived(float oldValue, float newValue);

    public class HC_SR04 : DigitalDevice
    {

        public static float TICKS_PER_CM = 492.5F;
        public static float STARTING_TICKS = 7100.0F;

        public float Distance { get; private set; }
        public event MeasurementReceived onMeasurementReceived;
        protected bool WaitingForReading { get; set; }
        protected DateTime PingSentTime { get; set; }
        

        protected long SumMeasurements { get; set; }
        protected long LastMeasurement { get; set; }
        protected float MeasurementTolerance { get; set; }
        protected int CurrentMeasurement { get; set; }
        protected int MeasurementsToAverage { get; set; }
        

        public HC_SR04(Cpu.Pin trigPin, Cpu.Pin echoPin, bool initialState, int sampleAmount, float faultTol) : base(trigPin, PortType.Output, initialState)
        {
            _outputPort = new OutputPort(trigPin, false);
            _inputPort = new InterruptPort(echoPin, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeLow);
            _inputPort.OnInterrupt += new NativeEventHandler(ResponseReceived);
            MeasurementsToAverage = sampleAmount;
            MeasurementTolerance = 2.5F;
            CurrentMeasurement = 1;
        }

        public void ResponseReceived(uint d1, uint d2, DateTime rec)
        {
            long ticks = (rec - PingSentTime).Ticks;

            if (ticks >= STARTING_TICKS)
            {
                //First Measurement Case.
                if (CurrentMeasurement == 1)
                {
                    SumMeasurements = ticks;
                    LastMeasurement = ticks;
                    CurrentMeasurement++;
                }
                else
                {
                    //Determine Fault Tolerance.
                    if (IsValidReading(ticks))
                    {
                        SumMeasurements += ticks;
                        LastMeasurement = ticks;
                        CurrentMeasurement++;
                    }
                }

                //Commit Logic.
                if (CurrentMeasurement == MeasurementsToAverage)
                {
                    CurrentMeasurement = 1;
                    float newAmt = GetDistance(SumMeasurements / MeasurementsToAverage);
                    SumMeasurements = 0;
                    MeasurementReceivedNotify(newAmt);
                    Distance = newAmt;
                    if (Debug)
                    {
                        Debugging.Log.Info("Received new measurement: ", ticks);
                    }
                }
            }


            WaitingForReading = false;
        }

        protected bool IsValidReading(long meas)
        {
            return (SumMeasurements / CurrentMeasurement) * MeasurementTolerance >= meas;
        }

        protected void MeasurementReceivedNotify(float newValue)
        {
            if (onMeasurementReceived != null)
            {
                onMeasurementReceived(Distance, newValue);
            }
        }

        protected float GetDistance(float ticks)
        {
            return (ticks - STARTING_TICKS) / TICKS_PER_CM;
        }

        public void Render()
        {
            if (Enabled && !WaitingForReading)
            {
                PingSentTime = DateTime.Now;
                _outputPort.Write(true);
                ThreadExt.SleepMicro(2);
                _outputPort.Write(false);
                WaitingForReading = true;
            }
            else
            {
                //Give the micro some breathing room.
                Thread.Sleep(2);
            }
        }
    }
}
