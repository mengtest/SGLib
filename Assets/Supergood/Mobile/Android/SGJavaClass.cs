namespace Supergood.Unity.Mobile.Android
{
	using System;
	using UnityEngine;
	
	internal class SGJavaClass : IAndroidJavaClass
	{
		private const string KingskyJavaClassName = "com.supergood.framework.unity.SGCross";
		private AndroidJavaClass kingskyJavaClass = new AndroidJavaClass(KingskyJavaClassName);
		
		public T CallStatic<T>(string methodName)
		{
			return this.kingskyJavaClass.CallStatic<T>(methodName);
		}
		
		public void CallStatic(string methodName, params object[] args)
		{
			this.kingskyJavaClass.CallStatic(methodName, args);
		}
		
		// Mock the AndroidJava to compile on other platforms
		#if !UNITY_ANDROID
		private class AndroidJNIHelper
		{
			public static bool Debug { get; set; }
		}
		
		private class AndroidJavaClass
		{
			public AndroidJavaClass(string mock)
			{
			}
			
			public T CallStatic<T>(string method)
			{
				return default(T);
			}
			
			public void CallStatic(string method, params object[] args)
			{
			}
		}
		#endif
	}
}
