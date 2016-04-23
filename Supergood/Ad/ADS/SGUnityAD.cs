using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

namespace Supergood.Unity.Ad
{
	public class SGUnityAD : AdBase
	{
		public SGUnityAD (SGAdConfig.SGAdConfigElement config, AdDelegate adDelegate): base(config,adDelegate)
		{
		}

		public override void init (string platformId, string unitId, AdDelegate adDelegate = null)
		{
			if (Advertisement.isSupported) {
				Advertisement.Initialize (platformId);
			} else {
				Debug.Log ("Platform not supported");
			}
		}

		public override bool isLoad ()
		{
			return Advertisement.IsReady ();
		}

		public override void load ()
		{
		}

		public override void show ()
		{
			Advertisement.Show (null, new ShowOptions { 
				resultCallback = result => {
					switch(result){
					case ShowResult.Finished:
						if(AdManager.AdShowSucessed!= null){
							AdManager.AdShowSucessed();
						}
						break;
//					case ShowResult.Failed:
//						break;
//					case ShowResult.Skipped:
//						break;
					default:
						if(AdManager.AdShowFailed!= null){
							AdManager.AdShowFailed();
						}
						break;
					}
				}
			});
		}

		public override void showBanner (SGAdPosition sgAdPosition = SGAdPosition.Bottom)
		{
		}
	}
}
