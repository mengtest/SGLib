using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using Supergood.Unity.Mobile;

namespace Supergood.Unity.Editor
{
	internal class EditorSupergood : SupergoodBase, IMobileSupergoodImplementation
	{
		private bool limitEventUsage;

		public EditorSupergood() : base(new CallbackManager())
		{
		}
		protected EditorSupergood(CallbackManager callbackManager) : base(callbackManager)
		{
		}
		
		public override void OnLoginComplete(string message)
		{
			
			Debug.Log ("OnLoginComplete  Begin ...");
			LoginResult result = new LoginResult(message);
			//KS.CreateConfigDictionary(message);
			this.CallbackManager.OnKingskyResponse (result);
		}


		public override void Init (
			HideUnityDelegate hideUnityDelegate,
			InitDelegate onInitComplete,
			KingskyDelegate<IPurchaseResult> onPurchaseCompleteDelegate
			,Action<bool> gameCenter
			,Dictionary<string, object> extras)
		{
			base.Init (
				hideUnityDelegate,
				onInitComplete,
				onPurchaseCompleteDelegate,gameCenter,extras);
		}
		
		public override void Login (
			string params_str, KingskyDelegate<ILoginResult> callback)
		{
		
		}
		
		public override bool LimitEventUsage {
			get {
				return this.limitEventUsage;
			}
			
			set {
				this.limitEventUsage = value;
			}
		}
		
		public override void CallSendEmail (string email, string subject)
		{
		
			
		}
		
		public override void CallKeepScreenOn (bool isKeepScreenOn)
		{
		
		}
		
		
		public override void RegisterNotification (){

		}
		
		
		public override void Rate(){

		}

		
		public override void LogEvent(string eventID,string keyvalueJSONStr){

		}
		
		public override void Purchase(string pid,Dictionary<string, object> extras,KingskyDelegate<IPurchaseResult> callback){
		}
	
		public override bool AppIsInstalled(string bundleId){
			return false;
		}
	}
}
