using NUnit.Framework;

namespace KataTirePressureVariation.Test
{
    public class AlarmShould
    {
        private const string AlarmActivated = "Alarm activated!";
        private const string AlarmDeactivated = "Alarm deactivated!";

        [Test]
        public void Alarm_is_activated_when_pressure_is_in_range()
        {
            var alarm = new AlarmForTesting(5);
            
            alarm.Check();
            
            Assert.That(alarm.messageAlarmaState, Is.EqualTo(AlarmActivated));
        }

        [Test]
        public void Alarm_is_desactivated_and_pressure_is_out_of_range_then_there_are_not_print_message()
        {
            var alarm = new AlarmForTesting(18);

            alarm.Check();

            Assert.That(alarm.messageAlarmaState, Is.Empty);
        }

        [Test]
        public void Alarm_is_desactivated_when_pressure_is_in_range_and_alarm_is_actived()
        {
            var alarm = new AlarmForTesting(5, 18);

            alarm.Check();
            alarm.Check();

            Assert.That(alarm.messageAlarmaState, Is.EqualTo(AlarmDeactivated));
        }
    }

    public class AlarmForTesting : Alarm
    {
        private readonly Queue<int> _pressureValues;
        public string messageAlarmaState = string.Empty;

        public AlarmForTesting(params int[] pressureValues)
        {
            _pressureValues = new Queue<int>(pressureValues);
        }

        protected override double GetSensorValue()
        {
            return _pressureValues.Dequeue();
        }

        protected override void NotifyAlarmState(string messageAlarmState)
        {
            messageAlarmaState = messageAlarmState;
        }
    }
}