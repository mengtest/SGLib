
using System.Collections;
using UnityEngine;

namespace Supergood.Unity.App
{
	public class LaunchInfoManager
	{

		private readonly static string key = "LaunchInfo";
		private readonly static string VersionCode = "VersionCode";
		private LaunchInfo lastLaunchInfo;
		private LaunchInfo currentLaunchInfo;

		public static LaunchInfoManager Instant {
			get {
				if (_instant == null) {
					_instant = new LaunchInfoManager ();
				}
				return _instant;
			}
		}

		private static LaunchInfoManager _instant;

		private  LaunchInfoManager ()
		{
			lastLaunchInfo = SharedPrefs.LoadDecrypt<LaunchInfo> (key);
			currentLaunchInfo = new LaunchInfo (SGConfig.Instant.GetIntValue(1,VersionCode));
			SharedPrefs.SaveEncrypt<LaunchInfo> (key, currentLaunchInfo);
		}

		public bool VersionChanged ()
		{
			return (lastLaunchInfo.version != currentLaunchInfo.version);
		}

		public bool IsFirstLaunch ()
		{
			return lastLaunchInfo.version == 0f;
		}

		public class LaunchInfo
		{
			public int version {
				get;
				set;
			}

			public LaunchInfo ()
			{
				version = 0;
			}

			public LaunchInfo (int version)
			{
				this.version = version;
			}
		}

	}
}
