using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Supergood.Unity.Ad
{
	public class SGAdConfig
	{
		public static readonly string SGAD_CONFIG = "SGAdConfig";
		public static readonly string ADCOMPANY = "adCompany";
		public static readonly string PLATFORMID = "platformId";
		public static readonly string BANNERID = "bannerId";
		public static readonly string WEIGHT_FULL = "weightFull";
		public static readonly string WEIGHT_BANNER = "weightBanner";
		public static readonly string WEIGHT_VIDEO = "weightVideo";
		public static readonly string ISON = "isOn";

		public static  SGAdConfig Instant {
			get {
				if (_sgAdConfig == null) {
					_sgAdConfig = new SGAdConfig ();
				}
				return _sgAdConfig;
			}
		}
		
		private static SGAdConfig _sgAdConfig;

		public List<SGAdConfigElement> configs; 
		
		private SGAdConfig ()
		{
			if (SGConfig.Instant.Data != null) {
				List<object> configInfs = SGConfig.Instant.GetListValue(SGAD_CONFIG);
				configs = new List<SGAdConfigElement> ();
				for(int i=0;i<configInfs.Count;i++){
					SGAdConfigElement config = new SGAdConfigElement(configInfs[i] as Dictionary<string,object>);
					configs.Add(config);
				}
			}
		}

		public class SGAdConfigElement
		{
			public AdManager.ADCompany adCompany {
				get;
				private set;
			}
			public string platformId{
				get;
				private set;
			}
			public string bannerId{
				get;
				private set;
			}
			public bool isOn{
				get;
				private set;
			}

			public int weightFull{
				get;
				private set;
			}

			public int weightBanner{
				get;
				private set;
			}

			public int weightVideo{
				get;
				private set;
			}

			public SGAdConfigElement (AdManager.ADCompany adCompany, string platformId, string bannerId, bool isOn)
			{
				this.adCompany = adCompany;
				this.platformId = platformId;
				this.bannerId = bannerId;
				this.isOn = isOn;
			}

			public SGAdConfigElement (Dictionary<string,object> configInf)
			{
				this.adCompany = (AdManager.ADCompany)Enum.Parse (typeof(AdManager.ADCompany),DictionaryUtil.GetStringValue (configInf,"UNKNOW",ADCOMPANY));
				this.platformId = DictionaryUtil.GetStringValue (configInf,"",PLATFORMID);
				this.bannerId =DictionaryUtil.GetStringValue (configInf,"",BANNERID); 
				this.isOn = DictionaryUtil.GetBoolValue (configInf,false,ISON);
				this.weightFull = DictionaryUtil.GetIntValue (configInf,0,WEIGHT_FULL);
				this.weightBanner =DictionaryUtil.GetIntValue (configInf,0,WEIGHT_BANNER);
				this.weightVideo =DictionaryUtil.GetIntValue (configInf,0,WEIGHT_VIDEO); 
				Debug.Log( "  adCompany :"  + adCompany+ 
				          "  platformId :"  + platformId+ 
				          "  bannerId :"  + bannerId+ 
				          "  isOn :"  + isOn + 
				          " weightFull " + weightFull + 
				          " weightVideo " + weightVideo
				          + 
				          " weightBanner " + weightBanner);
			}
		}

	}
}
