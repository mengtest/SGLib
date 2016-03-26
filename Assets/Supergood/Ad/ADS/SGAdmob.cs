using UnityEngine;
using System.Collections;
using  GoogleMobileAds.Api;

namespace Supergood.Unity.Ad
{
	public class SGAdmob : AdBase
	{
		public SGAdmob(SGAdConfig.SGAdConfigElement config,AdDelegate adDelegate): base(config,adDelegate){
		}
		string bannerId;
		string interstitialId;

		public override void init (string platformId, string unitId, AdDelegate adDelegate = null)
		{
			this.interstitialId = platformId;
			this.bannerId = unitId;

			if (this.interstitialId != null && (!this.interstitialId.Equals (""))) {
				load ();
			}
			//RequestBannerGoogle ();
		}

		public override bool isLoad ()
		{
			return interstitialVideoGoogle.IsLoaded ();
		}
		
		public override void load ()
		{
			RequestInterstitialVideoGoogle ();
		}
		
		public override void show ()
		{
			if (isLoad ()) {
				interstitialVideoGoogle.Show ();
			} else {
				load ();
			}
		}
		
		public override void showBanner ()
		{
			RequestBannerGoogle ();
		}

		private  void RequestBannerGoogle ()
		{
			BannerView bannerView = new BannerView (bannerId, AdSize.Banner, AdPosition.Bottom);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder ().Build ();
			// Load the banner with the request.
			bannerView.LoadAd (request);
		}
		
		private  InterstitialAd interstitialVideoGoogle;
		
		private  void RequestInterstitialVideoGoogle ()
		{
			// Initialize an InterstitialAd.
			interstitialVideoGoogle = new InterstitialAd (interstitialId);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder ().Build ();
			// Load the interstitial with the request.
			interstitialVideoGoogle.LoadAd (request);
		}

	}
}
