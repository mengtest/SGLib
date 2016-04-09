using UnityEngine;
using System.Collections;
using System;

namespace Supergood.Unity.Load
{  
	public abstract class Loader :LoadComplete
	{

		public Loader(){
		}

		public Loader(string remoteUrl,string localPath = ""){
			this.remoteUrl = remoteUrl;
			this.localPath = localPath;
			this.isLoad = false;
		}

		public string remoteUrl {
			get;
			set;
		}

		public string localPath {
			get;
			set;
		}

		public bool isLoad {
			get;
			set;
		}

	    

		public abstract void OnSuccess (WWW www);

		public abstract void OnFail (string error);

		public IEnumerator Download ()
		{
			WWW www = new WWW (remoteUrl);
			yield return www;
			if (www.isDone) {
				byte[] data = www.bytes;
				if (String.IsNullOrEmpty (www.error)) {
					OnSuccess (www);
				} else { 
					OnFail (www.error);
				}
			}
		}
	}
}
