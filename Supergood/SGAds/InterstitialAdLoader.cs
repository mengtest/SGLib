using UnityEngine;
using System.Collections;
using Supergood.Unity;
using Supergood.Unity.Load;
using System.Collections.Generic;

namespace Supergood.Unity.SGAds
{
	public class InterstitialAdLoader : Loader
	{

		private string imageName;
		private string appName;
		private string appURL;
		private bool isFromLocal = false;

		public InterstitialAdLoader (string remoteUrl, string appName, string appURL):base(remoteUrl,"")
		{
			//string a = remoteUrl.
			string[] temp = remoteUrl.Split ('/');
			if (temp.Length > 0) {
				imageName = temp [temp.Length - 1];
			} else {
				imageName = "";
			}

			localPath = Application.persistentDataPath + "//" + imageName;

			if (FileUtil.Exists (localPath)) {
				this.remoteUrl = "file://" + Application.persistentDataPath + "/" + imageName;
				isFromLocal = true;
			} else {
				isFromLocal = false;
			}

			this.appName = appName;
			this.appURL = appURL;


		}
	
		public override void OnSuccess (WWW www)
		{

			Debug.Log ("InterstitialAdLoader   .....  OnSuccess");
			SGCross.sgAdsGO.interstitialAdGO.ShowFullImage (UnityUtil.CreateSprite (www.texture));
			if (!isFromLocal)
				FileUtil.Write (localPath, www.bytes);
		}
	
		public override void OnFail (string error)
		{
			Debug.Log ("InterstitialAdLoader   .....  false  " + error);
		}
	
		public void StartLoader ()
		{
			Debug.Log ("InterstitialAdLoader   .....  StartLoader  " + remoteUrl);
			SGCross.LoadResource (this);
		}
	}
}
