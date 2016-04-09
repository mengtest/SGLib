using UnityEngine;
using System.Collections;
using System.IO;

namespace Supergood.Unity.Load
{  
	public class SimpleZipDownloader : Loader
	{

		private bool remove;
		public delegate void OnFinish ();

		public OnFinish onFinish;
		private string zipPath;

		public SimpleZipDownloader (string remoteUrl, OnFinish onFinish = null, bool remove = true):base(remoteUrl,"")
		{
			//string a = remoteUrl.
			string file = UriHelper.GetFileName (remoteUrl);
			
			int lastDot = file.LastIndexOf ('.');
			string dirName = file.Substring (0, lastDot);
			Debug.Log ("dirName  :  " + dirName);
			
			localPath = Application.persistentDataPath + "/" + dirName;
			zipPath = Application.persistentDataPath + "/" + file;
			
			this.remoteUrl = remoteUrl;
			this.remove = remove;
			this.onFinish = onFinish;
			
		}
		
		public override void OnSuccess (WWW www)
		{
			byte[] data = www.bytes;

			Directory.CreateDirectory (localPath);
			
			Debug.Log ("Downloaded file path: " + zipPath);

			ZipFile.UnZip (zipPath, data);
			if (this.onFinish != null) {
				this.onFinish ();
			}

			if (remove) {
				System.IO.File.Delete (zipPath);
			}
		}
		
		public override void OnFail (string error)
		{
			Debug.Log ("SimpleZipDownloader   .....  false  " + error);
		}
		
		public void StartLoader ()
		{
			Debug.Log ("SimpleZipDownloader   .....  StartLoader  " + remoteUrl);
			SGCross.LoadResource (this);

		}
	}
}
