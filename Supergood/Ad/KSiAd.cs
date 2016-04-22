namespace Supergood.Unity.Ad
{
	using UnityEngine;
	using System.Collections;

#if UNITY_IPHONE
using ADBannerView = UnityEngine.iOS.ADBannerView;
using ADInterstitialAd = UnityEngine.iOS.ADInterstitialAd;
#endif

	public class KSiAd : MonoBehaviour
	{

		public bool showOnTop = true;
		public bool dontDestroy = false;

	#if UNITY_IOS
	private ADBannerView banner = null;
	private ADInterstitialAd fullscreenAd = null;
	// Use this for initialization
	void Start () 
	{
		if(dontDestroy)
		{
			GameObject.DontDestroyOnLoad(gameObject);
		}
		
		

		fullscreenAd = new ADInterstitialAd();
		ADInterstitialAd.onInterstitialWasLoaded  += OnFullscreenLoaded;
		Debug.Log ("KKKKKKKKKKKKKKKKK  init   ....  ");
	}
	
	// onBannerLoaded is declared here
	void OnBannerLoaded () 
	{
		//banner.visible = true;
		Debug.Log ("KKKKKKKKKKKKKKKKK  init   ....  onBannerLoaded");
			banner.visible = true;
	}
	
	void OnBannerClicked()
	{
		Debug.Log(  "KKKKKKKKKKKKKKKKK  init   Clicked!\n");
	}
	
	void OnBannerFailedToLoad()
	{
		Debug.Log("KKKKKKKKKKKKKKKKK  init   FAIL!\n");
		banner.visible = false;
	}

	void OnFullscreenLoaded()
	{

		Debug.Log("KKKKKKKKKKKKKKKKK  init   OnFullscreenLoaded!\n");
		//fullscreenAd.Show();
	}

	void ShowFullScreenAd(){
		if(fullscreenAd.loaded)
			fullscreenAd.Show();
	}

	bool FullScreenAdIsLoad(){
		return fullscreenAd.loaded;
	}

		void showBanner(ADBannerView.Layout adPosition){
			banner = new ADBannerView(ADBannerView.Type.Banner, 
			                          showOnTop? ADBannerView.Layout.Top : ADBannerView.Layout.Bottom);
			
			ADBannerView.onBannerWasClicked += OnBannerClicked;
			ADBannerView.onBannerWasLoaded += OnBannerLoaded;
			ADBannerView.onBannerFailedToLoad += OnBannerFailedToLoad;
		}
	#else
		void Start ()
		{
			init ();
		}

		void ShowFullScreenAd ()
		{

		}
		
		bool FullScreenAdIsLoad ()
		{
			return false;
		}

		void showBanner (SGAdPosition sgAdPosition = SGAdPosition.Bottom)
		{
		}
	#endif

		private static KSiAd instance;

		public static void init ()
		{
			ensureInstance ();
		}

		private static void ensureInstance ()
		{
#if UNITY_IOS
		if(instance == null)
		{
			Debug.LogWarning("KSiAd Unity version -- " );
			instance = FindObjectOfType( typeof(KSiAd)) as KSiAd;
			if(instance == null)
			{
				instance = new GameObject("KSiAd").AddComponent<KSiAd>();
			}
		}
#endif
		}

		public static void ShowAd ()
		{
			if (instance != null) {
				instance.ShowFullScreenAd ();
			}
		}

		public static bool AdIsload ()
		{
			if (instance != null) {
				instance.FullScreenAdIsLoad ();
			}
			return false;
		}

		public static void showBannerView (SGAdPosition sgAdPosition = SGAdPosition.Bottom)
		{
			#if UNITY_IOS
			if (instance != null) {
				switch (sgAdPosition) {
				case SGAdPosition.Bottom:
					instance.showBanner (ADBannerView.Layout.BottomCenter);
					break;
				case SGAdPosition.BottomLeft:
					instance.showBanner (ADBannerView.Layout.BottomLeft);
					break;
				case SGAdPosition.BottomRight:
					instance.showBanner (ADBannerView.Layout.BottomRight);
					break;
				case SGAdPosition.Top:
					instance.showBanner (ADBannerView.Layout.TopCenter);
					break;
				case SGAdPosition.TopLeft:
					instance.showBanner (ADBannerView.Layout.TopLeft);
					break;
				case SGAdPosition.TopRight:
					instance.showBanner (ADBannerView.Layout.TopRight);
					break;
					
				}
			}
#endif
		}
	}
}
