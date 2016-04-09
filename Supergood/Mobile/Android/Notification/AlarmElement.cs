
namespace Supergood.Unity.Mobile.Android
{

	public class AlarmElement
	{
		private  const  string REPEAT = "repeat";
		private const  string TITLE = "title";
		private const  string CONTENT = "content";
		private const  string START_TIME = "startTime";
		private const  string NOTIFY_TYPE = "notifyType";
		private const  string REPEAT_INTERVAL = "repeatInterval";
		private const  string ALARM_NAME = "alarm_name";
		private const  string ALARM_FLAG = "alarm_flag";
		private  MethodArguments param;

		public string alarm_name {
			set {
				param.AddString (ALARM_NAME, value);
			}
		}

		public int alarm_flag {
			set {
				param.AddPrimative (ALARM_FLAG, value);
			}
		}

		public int notifyType {
			set {
				param.AddPrimative (NOTIFY_TYPE, value);
			}
		}

		public string title {
			
			set {
				param.AddString (TITLE, value);
			}
		}

		public string content {
			
			set {
				param.AddString (CONTENT, value);
			}
		}

		public bool repeat {
			set {
				param.AddPrimative (REPEAT, value);
			}
		}

		public long startTime {
			set {
				param.AddPrimative (START_TIME, value);
			}
		}

	

		public long repeatInterval {
			set {
				param.AddPrimative (REPEAT_INTERVAL, value);
			}
		}

		public AlarmElement ()
		{
			param = new MethodArguments ();
			alarm_name = "name";
			alarm_flag = 0;
			notifyType = 0;
			title = "title";
			content = "content";
			repeat = false;
			startTime = 0;
			repeatInterval = 0;
		}
		
		public string ToJsonString ()
		{
			return param.ToJsonString ();
		}
	}



}
