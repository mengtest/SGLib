namespace Supergood.Unity.Mobile.Android
{
	using UnityEngine;
	using System.Collections;

	internal class AndroidSupergoodGameObject:MobileSupergoodGameObject
	{
		protected override void OnAwake ()
		{
			#if UNITY_ANDROID
			AndroidJNIHelper.debug = true;
			#endif
		}
	}
}
