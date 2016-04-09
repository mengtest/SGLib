namespace Supergood.Unity.Mobile
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	internal abstract class MobileSupergood:SupergoodBase,IMobileSupergoodImplementation
	{
		protected MobileSupergood(CallbackManager callbackManager) : base(callbackManager)
		{
		}

		public override void OnLoginComplete(string message)
		{

			Debug.Log ("OnLoginComplete  Begin ...");
			LoginResult result = new LoginResult(message);
			//KS.CreateConfigDictionary(message);
			this.CallbackManager.OnKingskyResponse (result);
		}


	


//		public override void OnPurchaseComplete(string message){
//			Debug.Log ("Begin OnPurchaseComplete ...");
//			PurchaseResult result = new PurchaseResult(message);
//			base.OnPurchaseComplete ();
//			Debug.Log ("Begin OnPurchaseComplete ..."  + result.Pid);
//			this.CallbackManager.OnKingskyResponse (result);
//		}
	}
}
