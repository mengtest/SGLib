
namespace Supergood.Unity
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	internal abstract class SupergoodBase :ISupergoodImplementation
	{

		private InitDelegate onInitCompleteDelegate;
		private HideUnityDelegate onHideUnityDelegate;
		private KingskyDelegate<IPurchaseResult> onPurchaseCompleteDelegate;
		
		protected SupergoodBase (CallbackManager callbackManager)
		{
			this.CallbackManager = callbackManager;
		}
		
		public abstract bool LimitEventUsage { get; set; }

		protected CallbackManager CallbackManager { get; private set; }

		public virtual void Init (
			HideUnityDelegate hideUnityDelegate,
			InitDelegate onInitComplete,
			KingskyDelegate<IPurchaseResult> onPurchaseCompleteDelegate
			, Action<bool> gameCenter,
			Dictionary<string, object> extras)
		{
			this.onHideUnityDelegate = hideUnityDelegate;
			this.onInitCompleteDelegate = onInitComplete;
			this.onPurchaseCompleteDelegate = onPurchaseCompleteDelegate;
			Social.localUser.Authenticate (gameCenter);
		}

		public abstract  void Login (string params_str, KingskyDelegate<ILoginResult> callback);

		public abstract  void CallSendEmail (string email, string subject);
		
		public abstract  void CallKeepScreenOn (bool isKeepScreenOn);

		public abstract void RegisterNotification ();

		public abstract void Rate ();

		public abstract void LogEvent (string eventID, string keyvalueJSONStr);

		public abstract void Purchase (string pid, Dictionary<string, object> extras, KingskyDelegate<IPurchaseResult> callback);

		public abstract bool AppIsInstalled(string bundleId);

		public virtual void OnInitComplete (string message)
		{
			this.OnLoginComplete (message);
			if (this.onInitCompleteDelegate != null) {
				this.onInitCompleteDelegate ();
			}
		}

		public abstract void OnLoginComplete (string message);


		public  void OnPurchaseComplete (string message)
		{
			Debug.Log ("Begin OnPurchaseComplete ...");
			PurchaseResult result = new PurchaseResult (message);
			if (this.onPurchaseCompleteDelegate != null)
				onPurchaseCompleteDelegate (result);
			Debug.Log ("Begin OnPurchaseComplete ..." + result.Pid);
		}
	}
}
