using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Supergood.Unity.Ad
{
	public class AdManager
	{
		private Dictionary<ADCompany,AdBase> adsAll;
		public AdShowController adsVideoController;
		public AdShowController adsFullController;
		private AdShowController adsBannerController;

		public enum ADCompany
		{
			ADMOB,
			UNITYAD,
			ADCOLONY,
			VUNGLE,
			IAD,
			UNKNOW
		}

		public enum ADType
		{
			BANNER,
			INTERSTITIAL,
			VIDEO
		}

		public static AdManager instant {
			get {
				if (_instant == null) {
					_instant = new AdManager ();
				}
				return _instant;
			}
		}
	
		private static AdManager _instant;

		protected AdManager ()
		{
			SGAdConfig sgAdConfig = SGAdConfig.Instant;
			adsAll = new Dictionary<ADCompany, AdBase> ();

			adsVideoController = new AdShowController (ADType.VIDEO);
			adsFullController = new AdShowController (ADType.INTERSTITIAL);
			adsBannerController = new AdShowController (ADType.BANNER);

			for (int i=0; i<sgAdConfig.configs.Count; i ++) {
				SGAdConfig.SGAdConfigElement config = sgAdConfig.configs [i];
				if (config.isOn) {
					AdBase adbase = CreateAd (config);
					if (adbase != null) {
						adsAll.Add (config.adCompany, adbase);
						adsVideoController.addAd(adbase);
						adsFullController.addAd(adbase);
						adsBannerController.addAd(adbase);
					}
				}
			}

			ShowBanner ();
		}

		public bool isLoad (ADCompany adCompany)
		{
			if (adsAll.ContainsKey (adCompany)) {
				return adsAll [adCompany].isLoad ();
			}
			return false;
		}

		public void showAd (ADCompany adCompany)
		{
			if (adsAll.ContainsKey (adCompany)) {
				adsAll [adCompany].show ();
			}
		}

		public void load (ADCompany adCompany)
		{
			if (adsAll.ContainsKey (adCompany)) {
				adsAll [adCompany].load ();
			}
		}

		public void ShowVideo(){
			adsVideoController.Show ();
		}

		public void ShowFull(){
			adsFullController.Show ();
		}

		public void ShowBanner(){
			adsBannerController.ShowBanner();
		}

		public static void OnEnableVungle ()
		{
			#if UNITY_IPHONE || UNITY_ANDROID
			Vungle.onAdStartedEvent += onAdStartedEvent;
			Vungle.onAdEndedEvent += onAdEndedEvent;
			Vungle.onAdViewedEvent += onAdViewedEvent;
			Vungle.onCachedAdAvailableEvent += onCachedAdAvailableEvent;
			#endif
		}
		
		public static void OnDisableVungle ()
		{
			#if UNITY_IPHONE || UNITY_ANDROID
			Vungle.onAdStartedEvent -= onAdStartedEvent;
			Vungle.onAdEndedEvent -= onAdEndedEvent;
			Vungle.onAdViewedEvent -= onAdViewedEvent;
			Vungle.onCachedAdAvailableEvent -= onCachedAdAvailableEvent;
			#endif
		}
		
		public static void onAdStartedEvent ()
		{
			Debug.Log ("onAdStartedEvent");
		}
		
		public static void onAdEndedEvent ()
		{
			Debug.Log ("onAdEndedEvent");
		}
		
		public static void onAdViewedEvent (double watched, double length)
		{
			Debug.Log ("onAdViewedEvent. watched: " + watched + ", length: " + length);
		}
		
		public static void onCachedAdAvailableEvent ()
		{
			Debug.Log ("onCachedAdAvailableEvent");
		}

		public static AdBase CreateAd (SGAdConfig.SGAdConfigElement config)
		{
			switch (config.adCompany) {
			case ADCompany.ADCOLONY:
				return new SGAdColony (config, null);
			case ADCompany.ADMOB:
				return new SGAdmob (config, null);
			case ADCompany.IAD:
				return new SGIAd (config, null);
			case ADCompany.UNITYAD:
				return new SGUnityAD (config, null);
			case ADCompany.VUNGLE:
				return new SGVungle (config, null);
			}
			return null;
		}

		public class AdShowController
		{
			public List<AdBase> adsAll;
			ADType adType ;

			public AdShowController (ADType adType)
			{
				adsAll = new List<AdBase> ();
				this.adType = adType;
			}

			public void addAd (AdBase adBase)
			{
				if (IsAvailable (adBase)) {
					int insetIndex = 0;
					for (int i =0; i<adsAll.Count; i++) {

						if (CheckWeight (adBase, adsAll [i])) {
							break;
						}
						insetIndex = i + 1;
					}
					adsAll.Insert (insetIndex, adBase);
				}
			}

			public  bool CheckWeight (AdBase adBase1, AdBase adBase2)
			{
				switch (this.adType) {
				case ADType.BANNER:
					return adBase1.config.weightBanner > adBase1.config.weightBanner;
				case ADType.INTERSTITIAL:
					return adBase1.config.weightFull > adBase1.config.weightFull;
				case ADType.VIDEO:
					return adBase1.config.weightVideo > adBase1.config.weightVideo;
				}
				return false;
			}

			public  bool IsAvailable (AdBase adBase1)
			{
				switch (this.adType) {
				case ADType.BANNER:
					return adBase1.config.weightBanner > 0;
				case ADType.INTERSTITIAL:
					return adBase1.config.weightFull > 0;
				case ADType.VIDEO:
					return adBase1.config.weightVideo > 0;
				}
				return false;
			}


			public void Show(){
				foreach(AdBase adBase in adsAll){
					if(adBase.isLoad()){
						adBase.show();
						break;
					}
				}
			}

			public bool IsLoad(){
				foreach(AdBase adBase in adsAll){
					if(adBase.isLoad()){
						return true;
					}
				}
				return false;
			}

			public void ShowBanner(){
				if (adsAll.Count > 0) {
					adsAll[0].showBanner();
				}
			}
		}
	}
}