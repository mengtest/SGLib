namespace Supergood.Unity
{
	internal static class Constants
	{
		// Callback keys
		public const string CallbackIdKey = "callback_id";
		
		public static bool IsMobile
		{
			get
			{
				#if UNITY_ANDROID || UNITY_IOS
				return true;
				#else
				return false;
				#endif
			}
		}
		
		public static bool IsEditor
		{
			get
			{
				#if UNITY_EDITOR
				return true;
				#else
				return false;
				#endif
			}
		}
		
		public static bool IsWeb
		{
			get
			{
				#if UNITY_WEBPLAYER || UNITY_WEBGL
				return true;
				#else
				return false;
				#endif
			}
		}
	}
}

