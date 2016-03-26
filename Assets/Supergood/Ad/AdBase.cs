using UnityEngine;
using System.Collections;
namespace Supergood.Unity.Ad
{
	public delegate void AdDelegate();
	public abstract class AdBase  {
		public SGAdConfig.SGAdConfigElement config {
			get;
			set;
		}

		public AdBase(SGAdConfig.SGAdConfigElement config,AdDelegate adDelegate){
			this.config = config;
			init (config.platformId,config.bannerId,adDelegate);
		}
		public abstract  void init(string platformId,string unitId,AdDelegate adDelegate);
		public abstract  bool isLoad();
		public abstract  void load();
		public abstract  void show();
		public abstract  void showBanner();
	}	
}
