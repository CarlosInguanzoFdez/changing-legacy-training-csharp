using System;

namespace KataTirePressureVariation
{
    public class Alarm
    {
        private const double LowPressureThreshold = 17;
        private const double HighPressureThreshold = 21;

        private readonly Sensor sensor = new Sensor();

        private bool alarmOn = false;

        public void Check()
        {
            var psiPressureValue = GetSensorValue();

            if (psiPressureValue < LowPressureThreshold || HighPressureThreshold < psiPressureValue)
            {
                if (!IsAlarmOn())
                {
                    alarmOn = true;
                    NotifyAlarmState("Alarm activated!");
                }
            }
            else
            {
                if (IsAlarmOn())
                {
                    alarmOn = false;
                    NotifyAlarmState("Alarm deactivated!");
                }
            }
        }

        protected virtual double GetSensorValue()
        {
            return sensor.PopNextPressurePsiValue();
        }

        protected virtual void NotifyAlarmState(string messageAlarmState)
        {
            Console.WriteLine(messageAlarmState);
        }

        private bool IsAlarmOn()
        {
            return alarmOn;
        }
    }
}