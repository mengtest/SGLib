using UnityEngine;
using System.Collections;

namespace Supergood.Unity.Ad
{
	public class SGAdColony : AdBase
	{
		public SGAdColony(SGAdConfig.SGAdConfigElement config,AdDelegate adDelegate): base(config,adDelegate){
		}
		private  string[] Adzones;

		public override void init (string platformId, string unitId, AdDelegate adDelegate = null)
		{
			Adzones = new string[] {unitId};
			AdColony.Configure (Application.version, platformId, Adzones);
		}
		
		public override bool isLoad ()
		{
			return AdColony.IsVideoAvailable (Adzones [0]);
		}
		
		public override void load ()
		{
		}
		
		public override void show ()
		{
			AdColony.ShowVideoAd (Adzones [0]);
		}

		public override void showBanner ()
		{
		}
	}
}
