using UnityEngine;
using System.Collections;

namespace Supergood.Unity.Mobile.Android
{
	
	internal class AndroidSupergoodLoader : SGCross.CompiledSupergoodLoader
	{
		protected override SupergoodGameObject SGGameObject {
			get {
				AndroidSupergoodGameObject androidSG = ComponentFactory.GetComponent<AndroidSupergoodGameObject> ();
				if (androidSG.Supergood == null) {
					androidSG.Supergood = new AndroidSupergood ();
				}
				
				return androidSG;
			}
		}
	}
}
