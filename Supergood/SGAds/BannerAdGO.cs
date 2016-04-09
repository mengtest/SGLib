using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Supergood.Unity.SGAds
{
	public class BannerAdGO : MonoBehaviour
	{

		public Button ClickButton;
		public Image IconImage;
		public  Text AppName ;
		public BannerConfig config;
		public Text AppDesc ;

		public void OnClick ()
		{
			Debug.Log ("  open  " + config.AppUrl);
		}

		public void SetConfig (BannerConfig config)
		{
			this.config = config;
			IconImage.sprite = this.config.IconSprite;
			AppName.text = this.config.AppName;
			AppDesc.text = this.config.AppDesc;
		}

		public void SetConfig (Dictionary<string,object> bannerConfig)
		{
			this.config = new BannerConfig (bannerConfig);
			IconImage.sprite = this.config.IconSprite;
			AppName.text = this.config.AppName;
			AppDesc.text = this.config.AppDesc;
		}

		public class BannerConfig
		{
			public BannerConfig ()
			{
			}

			public BannerConfig (Dictionary<string,object> bannerConfig)
			{
				AppName = DictionaryUtil.GetStringValue (bannerConfig, "", SGAdsConfig.APP_NAME);
				AppUrl = DictionaryUtil.GetStringValue (bannerConfig, "", SGAdsConfig.APP_URL);
				AppDesc = DictionaryUtil.GetStringValue (bannerConfig, "", SGAdsConfig.APP_DESC);
				Debug.Log ("  aaaaaaa  " + (SGAdsConfig.Instant.iconsImageLoader.localPath + "/" + AppName + ".png"));
				IconSprite = UnityUtil.CreateSprite (FileUtil.LoadPNG (SGAdsConfig.Instant.iconsImageLoader.localPath + "/" + AppName + ".png"));
			}

			public string AppName {
				get;
				set;
			}

			public string AppUrl {
				get;
				set;
			}
		
			public string AppDesc {
				get;
				set;
			}

			public Sprite IconSprite {
				get;
				set;
			}

		}

	}
}
