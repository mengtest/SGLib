using UnityEngine;
using System.Collections;

namespace Supergood.Unity.Ad
{
	public class SGIAd : AdBase
	{
		public SGIAd(SGAdConfig.SGAdConfigElement config,AdDelegate adDelegate): base(config,adDelegate){
		}
		public override void init (string platformId, string unitId, AdDelegate adDelegate = null)
		{
			KSiAd.init ();
		}

		public override bool isLoad ()
		{
			return KSiAd.AdIsload ();
		}
		
		public override void load ()
		{
		}
		
		public override void show ()
		{
			KSiAd.ShowAd ();
		}

		public override void showBanner (SGAdPosition sgAdPosition = SGAdPosition.Bottom)
		{
			KSiAd.showBannerView (sgAdPosition);
		}
	
	}
}
