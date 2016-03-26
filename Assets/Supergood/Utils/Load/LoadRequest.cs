using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using System;

namespace Supergood.Unity.Load
{  
	public class LoadRequest

	{  

		delegate void OnSuccess (WWW www);

		delegate void OnFail (string error);

		IEnumerator Download (string url, OnSuccess onSuccess, OnFail onFail)
		{
			WWW www = new WWW (url);
			yield return www;
			if (www.isDone) {
				byte[] data = www.bytes;
				if (String.IsNullOrEmpty (www.error)) {

					if (onSuccess != null) {
						onSuccess (www);
					}
				} else { 
					if (onFail != null) {
						onFail (www.error);
					}
				}
			}
		}
	}  
	
}  
