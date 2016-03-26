using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Supergood.Unity.Load;
using Supergood.Unity;

namespace Supergood.Unity.SGAds
{
	public class SGAdsConfig : Loader
	{
		private static readonly string CONFIG_NAME = "InterstitialAdConfig";
		private static readonly string URL = "URL";
		private static readonly string ON = "On";
		public static readonly string APP_NAME = "AppName";
		public static readonly string APP_URL = "AppURL";
		public static readonly string IMAGE_URL = "Image";
		public static readonly string BANNER_ADS = "BannerAds";
		public static readonly string ICONS_URL = "IconsURL";
		public static readonly string APPS_INFO = "AppsInfo";
		public static readonly string APP_DESC = "AppDesc";

		public static SGAdsConfig Instant {
			get {
				if (_instant == null) {
					Dictionary<string,object> dictionary = SGConfig.Instant.GetDictionaryValue (CONFIG_NAME);

					if (dictionary == null) {
						Debug.Log ("InterstitialAdConfig   .....  InterstitialAdConfig");
					}

					_instant = new SGAdsConfig (DictionaryUtil.GetStringValue (dictionary, "", URL));
					_instant.isOn = DictionaryUtil.GetBoolValue (dictionary, false, ON);
				}
				return _instant;
			}
		}
		
		private static SGAdsConfig _instant;
		private Dictionary<string,object> _sgAds;
		private List<object> AppsInfo;
		private bool IconsImageIsLoader = false;
		private List<object> _bannerInfos;

		private string bannerGameName= "";

		public bool isOn {
			get;
			private set;
		}

		public bool loadSuccessed {
			get;
			private set;
		}

		public SimpleZipDownloader iconsImageLoader {
			get;
			private set;
		}

		private SGAdsConfig (string remoteUrl, string localPath = ""):base(remoteUrl,localPath)
		{
			
		}

		public override void OnSuccess (WWW www)
		{
			Dictionary<string,object> plist = Plist.readPlist (DESUtil.Decrypt (www.bytes, FileUtil.sKey)) as Dictionary<string,object>;
			_sgAds = DictionaryUtil.GetDictionaryValue (plist, "Data", "SGAds");
			List<object> InterstitialAds = DictionaryUtil.GetListValue (_sgAds, "InterstitialAds");
			Dictionary<string,object> v1 = InterstitialAds [0] as  Dictionary<string,object>;
			Debug.Log ("InterstitialAdConfig   .....  OnSuccess");
			InterstitialAdLoader interstitialAdLoader = new InterstitialAdLoader (DictionaryUtil.GetStringValue (v1, "", IMAGE_URL), DictionaryUtil.GetStringValue (v1, "", APP_NAME), DictionaryUtil.GetStringValue (v1, "", APP_URL));
			isOn = true;
			interstitialAdLoader.StartLoader ();
			Dictionary<string,object> BannerAds = DictionaryUtil.GetDictionaryValue (_sgAds, BANNER_ADS);
			iconsImageLoader = new SimpleZipDownloader (DictionaryUtil.GetStringValue (BannerAds, "", ICONS_URL), OnIconsLoad, false);
			iconsImageLoader.StartLoader ();
			_bannerInfos = DictionaryUtil.GetListValue (BannerAds, APPS_INFO);
		}
		
		public override void OnFail (string error)
		{
			Debug.Log ("InterstitialAdConfig   .....  false  " + error);
			isOn = false;
		}

		public void StartLoader ()
		{
			Debug.Log ("InterstitialAdConfig   .....  StartLoader  " + remoteUrl);
			SGCross.LoadResource (this);
		}

		private void OnIconsLoad ()
		{
			IconsImageIsLoader = true;
			Dictionary<string,object> bannerConfig = GetBannerInfo ();
			if (bannerConfig != null) {
				SGCross.sgAdsGO.bannerAdGO.gameObject.SetActive (true);
				SGCross.sgAdsGO.bannerAdGO.SetConfig (bannerConfig);
			}
		}


		private Dictionary<string,object> GetBannerInfo(){
			for (int i=0; i<_bannerInfos.Count; i++) {
				Dictionary<string,object> bannerConfig = _bannerInfos [i] as Dictionary<string,object>;
				string AppName = DictionaryUtil.GetStringValue (bannerConfig, "", SGAdsConfig.APP_NAME);
				if(FileUtil.Exists(iconsImageLoader.localPath + "/" + AppName + ".png")){
					bannerGameName = AppName;
					return bannerConfig;
				}
			}
			return null;
		}

	}
}
