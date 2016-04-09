using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Supergood.Unity.Analytics.Flurry;

namespace Supergood.Unity.Analytics
{
	public class AnalyticsManager
	{

		private static readonly string FLURRY_KEY = "FlurryKey";

		public static AnalyticsManager Instant {
			get {
				if (_instant == null) {
					_instant = new AnalyticsManager ();
				}
				return _instant;
			}
		}

		private static AnalyticsManager _instant;
		public IAnalytics flurryAnalytics;

		private AnalyticsManager ()
		{
			// For Flurry Android only:
			FlurryAndroid.SetLogEnabled (true);
			
			// For Flurry iOS only:
			FlurryIOS.SetDebugLogEnabled (true);
			flurryAnalytics = Flurry.Flurry.Instance;
			flurryAnalytics.StartSession (SGConfig.Instant.GetStringValue (FLURRY_KEY, ""));
			flurryAnalytics.LogUserID ("Github User");
			flurryAnalytics.LogEvent ("event", new Dictionary<string, string> {{ "platform", "Github" }});
		}
	}
}
