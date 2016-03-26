namespace Supergood.Unity
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	using Supergood.Unity.Load;
	using Supergood.Unity.App;

	//	using Supergood.Unity.Canvas;
	using Supergood.Unity.Editor;
	using Supergood.Unity.Mobile;
	using Supergood.Unity.Mobile.Android;
	using Supergood.Unity.Mobile.IOS;
	using UnityEngine;
	using Facebook.Unity;
	using Supergood.Unity.Analytics;
	using Supergood.Unity.SGAds;

	public sealed class SGCross : ScriptableObject
	{

		private static InitDelegate onInitComplete;
		private static HideUnityDelegate onHideUnity;
		private static KingskyDelegate<IPurchaseResult> onPurchaseCompleteDelegate;
		private static Action<bool> gameCenter;
		private static Dictionary<string, object> extras;
		private static SupergoodGameObject supergood;
		private static string authResponse;
		private static bool isInitCalled = false;
		public static LaunchInfoManager launchInfoMagager;
		public static SGAdsGO sgAdsGO;
//		private static string appId;
//		private static bool cookie;
//		private static bool logging;
//		private static bool status;
//		private static bool xfbml;
//		private static bool frictionlessRequests;


		/// <summary>
		/// Gets a value indicating is the SDK is initialized.
		/// </summary>
		/// <value><c>true</c> if is initialized; otherwise, <c>false</c>.</value>
		public static bool IsInitialized {
			get {
				return (supergood != null) && supergood.Initialized;
			}
		}
		
		internal static ISupergood SuperGoodImpl {
			get {
				if (supergood == null) {
					throw new NullReferenceException ("Supergood object is not yet loaded.  Did you call KS.Init()?");
				}
				
				return supergood.Supergood;
			}
		}

		/// <summary>
		/// This is the preferred way to call FB.Init(). It will take the facebook app id specified in your "Facebook"
		/// => "Edit Settings" menu when it is called.
		/// </summary>
		/// <param name="onInitComplete">
		/// Delegate is called when FB.Init() finished initializing everything. By passing in a delegate you can find
		/// out when you can safely call the other methods.
		/// </param>
		/// <param name="onHideUnity">A delegate to invoke when unity is hidden.</param>
		/// <param name="authResponse">Auth response.</param>
//		public static void Init(InitDelegate onInitComplete = null, HideUnityDelegate onHideUnity = null)
//		{
//			Init(
//				onHideUnity,
//				onInitComplete);
//		}


		/// <summary>
		/// If you need a more programmatic way to set the facebook app id and other setting call this function.
		/// Useful for a build pipeline that requires no human input.
		/// </summary>
		/// <param name="appId">App identifier.</param>
		/// <param name="cookie">If set to <c>true</c> cookie.</param>
		/// <param name="logging">If set to <c>true</c> logging.</param>
		/// <param name="status">If set to <c>true</c> status.</param>
		/// <param name="xfbml">If set to <c>true</c> xfbml.</param>
		/// <param name="frictionlessRequests">If set to <c>true</c> frictionless requests.</param>
		/// <param name="authResponse">Auth response.</param>
		/// <param name="onHideUnity">
		/// A delegate to invoke when unity is hidden.
		/// </param>
		/// <param name="onInitComplete">
		/// Delegate is called when FB.Init() finished initializing everything. By passing in a delegate you can find
		/// out when you can safely call the other methods.
		/// </param>
		public static void Init (
			HideUnityDelegate onHideUnity = null,
			InitDelegate onInitComplete = null,
			KingskyDelegate<IPurchaseResult> onPurchaseCompleteDelegate = null,
			Action<bool> gameCenter = null,
			Dictionary<string, object> extras = null, Facebook.Unity.InitDelegate onInitCompleteFacebook = null, Facebook.Unity.HideUnityDelegate onHideUnityFacebook = null, string authResponseFacebook = null)
		{
//			if (string.IsNullOrEmpty(appId))
//			{
//				throw new ArgumentException("appId cannot be null or empty!");
//			}
			FB.Init (onInitCompleteFacebook, onHideUnityFacebook, authResponseFacebook);

			SGCross.onInitComplete = onInitComplete;
			SGCross.onHideUnity = onHideUnity;
			SGCross.onPurchaseCompleteDelegate = onPurchaseCompleteDelegate;
			SGCross.gameCenter = gameCenter;
			SGCross.extras = extras;

			if (!isInitCalled) {
				
				#if UNITY_EDITOR
					ComponentFactory.GetComponent<EditorSupergoodLoader>();
//				#elif UNITY_WEBPLAYER || UNITY_WEBGL
				//				ComponentFactory.GetComponent<CanvasSupergoodLoader>();
				#elif UNITY_IOS
				ComponentFactory.GetComponent<IOSSupergoodLoader>();
				#elif UNITY_ANDROID
				ComponentFactory.GetComponent<AndroidSupergoodLoader>();
				#else
				throw new NotImplementedException ("Supergood API does not yet support this platform");
				#endif
				SGConfig.CreateInstant ();
				AnalyticsManager analyticsManager = AnalyticsManager.Instant;
				isInitCalled = true;
				return;
			}
			
			Debug.LogWarning ("KS.Init() has already been called.  You only need to call this once and only once.");
			
			// Init again if possible just in case something bad actually happened.
			if (SuperGoodImpl != null) {
				OnDllLoaded ();
			}
		}

		public static void Login (
			string permissions = null,
			KingskyDelegate<ILoginResult> callback = null)
		{
			SuperGoodImpl.Login (permissions, callback);
		}

		public static void CallSendEmail (string email, string subject)
		{
			SuperGoodImpl.CallSendEmail (email, subject);
		}

		public static void  CallKeepScreenOn (bool isKeepScreenOn)
		{
			SuperGoodImpl.CallKeepScreenOn (isKeepScreenOn);
		}

		public static void RegisterNotification ()
		{
			SuperGoodImpl.RegisterNotification ();
		}

		public static void Rate ()
		{
			SuperGoodImpl.Rate ();
		}

		public static void LogEvent (string eventID, string keyvalueJSONStr)
		{
			SuperGoodImpl.LogEvent (eventID, keyvalueJSONStr);
		}

		public static void Purchase (string pid, Dictionary<string, object> extras, KingskyDelegate<IPurchaseResult> callback = null)
		{
			SuperGoodImpl.Purchase (pid, extras, callback);
		}

		public static void LoadResource (Loader loader)
		{
			//if(loader != null)
			SGCross.supergood.LoadResource (loader);
		}

		private static void OnDllLoaded ()
		{
			SuperGoodImpl.Init (
				onHideUnity,
				onInitComplete,
				onPurchaseCompleteDelegate,
				gameCenter,
				extras);
		}


		/// <summary>
		/// A class containing the settings specific to the supported mobile platforms.
		/// </summary>
		public sealed class Mobile
		{
			private static IMobileSupergood MobileSuperGoodImpl {
				get {
					IMobileSupergood impl = SuperGoodImpl as IMobileSupergood;
					if (impl == null) {
						throw new InvalidOperationException ("Attempt to call Mobile interface on non mobile platform");
					}
					
					return impl;
				}
			}
		}

		internal abstract class CompiledSupergoodLoader : MonoBehaviour
		{
			protected abstract SupergoodGameObject SGGameObject { get; }
			
			public void Start ()
			{
				SGCross.supergood = this.SGGameObject;
				sgAdsGO = Instantiate (Resources.Load ("SGAdsCanvas", typeof(SGAdsGO)) as SGAdsGO) as SGAdsGO;
				SGConfig.Instant.ReadConfig ();
				SGCross.OnDllLoaded ();
				MonoBehaviour.Destroy (this);
			}
		}


	}
}
