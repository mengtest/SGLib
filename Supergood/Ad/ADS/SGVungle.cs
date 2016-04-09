using UnityEngine;
using System.Collections;

namespace Supergood.Unity.Ad
{
	public class SGVungle : AdBase
	{
		public SGVungle(SGAdConfig.SGAdConfigElement config,AdDelegate adDelegate): base(config,adDelegate){
		}

		public override void init (string platformId, string unitId, AdDelegate adDelegate = null)
		{
			#if UNITY_ANDROID
			Vungle.init( platformId, "");
			#elif UNITY_IOS
			Vungle.init( "", platformId);
			#else

			#endif
		}

		public override bool isLoad ()
		{
			#if UNITY_IPHONE || UNITY_ANDROID
			return Vungle.isAdvertAvailable ();
			#endif
			return false;
		}

		public override void load ()
		{
		}

		public override void show ()
		{
			#if UNITY_IPHONE || UNITY_ANDROID
			Vungle.playAd ();
			#endif
		}

		public override void showBanner ()
		{
		}

		public  void OnEnableVungle ()
		{
			#if UNITY_IPHONE || UNITY_ANDROID
			Vungle.onAdStartedEvent += AdManager.onAdStartedEvent;
			Vungle.onAdEndedEvent += AdManager.onAdEndedEvent;
			Vungle.onAdViewedEvent += AdManager.onAdViewedEvent;
			Vungle.onCachedAdAvailableEvent += AdManager.onCachedAdAvailableEvent;
			#endif
		}
		
		public  void OnDisableVungle ()
		{
			#if UNITY_IPHONE || UNITY_ANDROID
			Vungle.onAdStartedEvent -= AdManager.onAdStartedEvent;
			Vungle.onAdEndedEvent -= AdManager.onAdEndedEvent;
			Vungle.onAdViewedEvent -= AdManager.onAdViewedEvent;
			Vungle.onCachedAdAvailableEvent -= AdManager.onCachedAdAvailableEvent;
			#endif
		}
	}
}
