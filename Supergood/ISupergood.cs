
namespace Supergood.Unity
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System;

	internal interface ISupergood
	{
		void Init(
			HideUnityDelegate hideUnityDelegate,
			InitDelegate onInitComplete,
			KingskyDelegate<IPurchaseResult> onPurchaseCompleteDelegate,
			Action<bool> gameCenter,
			Dictionary<string, object> extras);

		void Login(string params_str,KingskyDelegate<ILoginResult> callback);

		void CallSendEmail (string email, string subject);

		void CallKeepScreenOn (bool isKeepScreenOn);

		void RegisterNotification ();

		void Rate();

		void LogEvent(string eventID,string keyvalueJSONStr);

		void Purchase(string pid,Dictionary<string, object> extras,KingskyDelegate<IPurchaseResult> callback);

		bool AppIsInstalled(string bundleId);
	}
}
