using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Supergood.Unity.Load;
using Supergood.Unity.Ad;
using System.IO;
using Supergood.Unity.App;
using Supergood.Unity.SGAds;

namespace Supergood.Unity
{
	public class SGConfig:Loader
	{
		private string configInitPath;
		private string extendLocalPath;

		public delegate void OnCompleteDelegate (bool changed);

		private OnCompleteDelegate onCompleteDelegate;

		public static void CreateInstant (OnCompleteDelegate onCompleteDelegate = null)
		{
			if (Instant == null) {
				Instant = new SGConfig (onCompleteDelegate);
			}
		}

		public static SGConfig Instant {
			get ;
			private set;
		}

		private SGConfig (OnCompleteDelegate onCompleteDelegate = null):base()
		{
			#if UNITY_IOS
			configInitPath = "Cogfig_init_IOS.plist";
			#else
			configInitPath = "Cogfig_init_Android.plist";
			#endif
			this.onCompleteDelegate = onCompleteDelegate;
			TextAsset textAsset = Resources.Load (configInitPath, typeof(TextAsset)) as TextAsset;
			if (textAsset.bytes != null) {

				Dictionary<string,object> plist = Plist.readPlist (DESUtil.Decrypt(textAsset.bytes,FileUtil.sKey)) as Dictionary<string,object>;
				_data = (Dictionary<string,object>)plist ["Data"];
				remoteUrl = GetStringValue ("", "ConfigPath", "RemoteConfig");
				localPath = GetStringValue ("", "ConfigPath", "LocalConfig");
				extendLocalPath = Application.persistentDataPath + "//" +localPath;
			}
		}

		private SGConfig (string remoteUrl, string localPath = "", OnCompleteDelegate onCompleteDelegate = null):base(remoteUrl,localPath)
		{
			this.onCompleteDelegate = onCompleteDelegate;
		}

		private SGConfig (string remoteUrl, string localPath = ""):base(remoteUrl,localPath)
		{

		}

		public Dictionary<string,object> Data {
			get{ return _data;}
		}

		private Dictionary<string,object> _data;

		public void ReadConfig ()
		{
			if (!ReadConfigFromExtern ()) {
				ReadConfigFromLocal (localPath);
			}

			if (this.onCompleteDelegate != null) {
				this.onCompleteDelegate (false);
			}
			AdManager adManager = AdManager.instant;
			SGCross.LoadResource (this);
		}

		private bool ReadConfigFromExtern ()
		{


			if (FileUtil.Exists (extendLocalPath)) {

				if (LaunchInfoManager.Instant.VersionChanged()) {
					FileUtil.DeleteFile(extendLocalPath);
				}
				try{

					byte[] dataByte = FileUtil.Read(extendLocalPath);
					CrateData ((DESUtil.Decrypt(dataByte,FileUtil.sKey)),false);
				return true;
				}catch(Exception e){}
			}
			return false;
		}

		private void ReadConfigFromLocal (string configPath)
		{

			TextAsset textAsset = Resources.Load (configPath, typeof(TextAsset)) as TextAsset;
			if (textAsset != null) {
				CrateData ( (DESUtil.Decrypt(textAsset.bytes,FileUtil.sKey)), false);
			}
		}

		private void CrateData (byte[] bytes, bool changed)
		{
			Dictionary<string,object> plist = Plist.readPlist (bytes) as Dictionary<string,object>;
			Merge ((Dictionary<string,object>)plist ["Data"]);
			if (this.onCompleteDelegate != null) {
				this.onCompleteDelegate (changed);
			}
			AdManager adManager = AdManager.instant;
			if(!changed){
				SGAdsConfig.Instant.StartLoader();
			}
			isLoad = true;
		}

		public override void OnSuccess (WWW www)
		{
			FileUtil.Write (extendLocalPath, www.bytes);
			CrateData (DESUtil.Decrypt(www.bytes,FileUtil.sKey), true);
		}
		
		public override void OnFail (string error)
		{
			//ReadConfigFromLocal (localPath);
		}

		public  Dictionary<string,object> GetDictionaryValue (params string[] paths)
		{
			return DictionaryUtil.GetDictionaryValue (_data, paths);
		}
		
		public  List<object> GetListValue (params string[] paths)
		{
			return DictionaryUtil.GetListValue (_data, paths);
		}
		
		public  string GetStringValue (string defaultValue, params string[] paths)
		{
			return DictionaryUtil.GetStringValue (_data, defaultValue, paths);
		}
		
		public  bool GetBoolValue (bool defaultValue, params string[] paths)
		{
			return DictionaryUtil.GetBoolValue (_data, defaultValue, paths);
		}
		
		public  int GetIntValue (int defaultValue, params string[] paths)
		{
			return DictionaryUtil.GetIntValue (_data, defaultValue, paths);
		}
		
		public  float GetLongValue (float defaultValue, params string[] paths)
		{
			return DictionaryUtil.GetLongValue (_data, defaultValue, paths);
		}
		
		public  long GetLongValue (long defaultValue, params string[] paths)
		{
			return DictionaryUtil.GetLongValue (_data, defaultValue, paths);
		}
		
		public  double GetLongValue (double defaultValue, params string[] paths)
		{
			return DictionaryUtil.GetLongValue (_data, defaultValue, paths);
		}

		public void Merge (Dictionary<string, object> remoteConfig)
		{
			DictionaryUtil.MergeDictionary (_data, remoteConfig);
		}

	
	}
	 
}
