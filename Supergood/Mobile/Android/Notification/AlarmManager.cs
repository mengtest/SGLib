namespace Supergood.Unity.Mobile.Android
{
	using System.Collections.Generic;
	public class AlarmManager
	{

		public static readonly string  AlarmInfoTag = "alarm_infos";
		private int baseFlag = 20000;
		private int baseNotifyType = 30000;
		List<string> temp = new List<string> ();
		private static AlarmManager instant;

		public static AlarmManager  Instant {
			get {

				if (instant == null) {
					instant = new AlarmManager ();
				}
				return instant;
			}

		}

		private AlarmManager ()
		{
		}

		public void Init ()
		{
			temp.Clear ();
		}

		public void AddAlarmInfo (AlarmElement alarmElement)
		{
			alarmElement.alarm_flag = baseFlag + temp.Count;
			alarmElement.notifyType = baseNotifyType + temp.Count;
			temp.Add (alarmElement.ToJsonString ());
		}

		public string GetAlarmInfos ()
		{
			var argsInfo = new MethodArguments ();
			for (int i=0; i<temp.Count; i++) {
				argsInfo.AddString (i + "", temp [i]);
			}
			return  argsInfo.ToJsonString ();
		}

	}
}
