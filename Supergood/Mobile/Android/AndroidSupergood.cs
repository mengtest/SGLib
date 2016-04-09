
namespace Supergood.Unity.Mobile.Android
{
	using UnityEngine;
	using System.Collections;
	using Supergood.Unity;
	using System.Collections.Generic;
	using System;
	

	internal sealed class AndroidSupergood:MobileSupergood
	{

		private bool limitEventUsage;
		private IAndroidJavaClass kingskyJava;
		
		public AndroidSupergood () : this(new SGJavaClass(), new CallbackManager())
		{
		}
		
		public AndroidSupergood (IAndroidJavaClass kingskyJavaClass, CallbackManager callbackManager)
			: base(callbackManager)
		{
//			this.KeyHash = string.Empty;
			this.kingskyJava = kingskyJavaClass;
		}

		public override void Init (
			HideUnityDelegate hideUnityDelegate,
			InitDelegate onInitComplete,
			KingskyDelegate<IPurchaseResult> onPurchaseCompleteDelegate
			,Action<bool> gameCenter
			,Dictionary<string, object> extras)
		{
			GooglePlayGames.PlayGamesPlatform.Activate();
			base.Init (
				hideUnityDelegate,
				onInitComplete,
				onPurchaseCompleteDelegate,gameCenter,extras);
			
			var args = new MethodArguments ();
			args.AddDictionary ("configInfo", extras);
			var initCall = new JavaMethodCall<IResult> (this, "Init");
			initCall.Call (args);
		}

		public override void Login (
			string params_str, KingskyDelegate<ILoginResult> callback)
		{
			MethodArguments args = new MethodArguments ();
			args.AddString ("test", params_str);
			var loginCall = new JavaMethodCall<ILoginResult> (this, "Login");
			loginCall.Callback = callback;
			loginCall.Call (args);
		}

		public override bool LimitEventUsage {
			get {
				return this.limitEventUsage;
			}
			
			set {
				this.limitEventUsage = value;
				this.CallKS ("SetLimitEventUsage", value.ToString ());
			}
		}

		public override void CallSendEmail (string email, string subject)
		{
			MethodArguments args = new MethodArguments ();
			args.AddString ("email", email);
			args.AddString ("subject", subject);

			var sendEmialCall = new JavaMethodCall<IResult> (this, "SendEmail");
			sendEmialCall.Call (args);

		}

		public override void CallKeepScreenOn (bool isKeepScreenOn)
		{
			MethodArguments args = new MethodArguments ();
			args.AddPrimative ("isKeepScreenOn", true);
			var keepScreenOnCall = new JavaMethodCall<IResult> (this, "KeepScreenOn");
			keepScreenOnCall.Call (args);
		}


		 public override void RegisterNotification (){
			var args = new MethodArguments ();
		
			args.AddString (AlarmManager.AlarmInfoTag, AlarmManager.Instant.GetAlarmInfos());
			
			var initCall = new JavaMethodCall<IResult> (this, "RegisterNotification");
			initCall.Call (args);
			}


	     public override void Rate(){
			var args = new MethodArguments ();
			
			args.AddString ("appName","com.thegamecontriver.sharetext" );
			
			var initCall = new JavaMethodCall<IResult> (this, "Rate");
			initCall.Call (args);
		}


		public override void LogEvent(string eventID,string keyvalueJSONStr){
			var args = new MethodArguments ();

			args.AddString ("eventID",eventID );
			args.AddString ("keyvalueJSONStr",keyvalueJSONStr );

			var initCall = new JavaMethodCall<IResult> (this, "LogEvent");
			initCall.Call (args);
		}

		public override void Purchase(string pid,Dictionary<string, object> extras,KingskyDelegate<IPurchaseResult> callback){
			MethodArguments args = new MethodArguments();
			args.AddString(PurchaseResult.IAP_PID_TAG, pid);
			args.AddDictionary(PurchaseResult.EXTRAS, extras);
			var purchaseCall = new JavaMethodCall<IPurchaseResult>(this, "Purchase");
			purchaseCall.Callback = callback;
			purchaseCall.Call(args);
		}

		public override bool AppIsInstalled(string bundleId){
			return false;
		}

		private void CallKS (string method, string args)
		{
			this.kingskyJava.CallStatic (method, args);
		}
		
		private class JavaMethodCall<T> : MethodCall<T> where T : IResult
		{
			private AndroidSupergood androidImpl;
			
			public JavaMethodCall (AndroidSupergood androidImpl, string methodName)
				: base(androidImpl, methodName)
			{
				this.androidImpl = androidImpl;
			}
			
			public override void Call (MethodArguments args = null)
			{
				MethodArguments paramsCopy;
				if (args == null) {
					paramsCopy = new MethodArguments ();
				} else {
					paramsCopy = new MethodArguments (args);
				}
				
				if (this.Callback != null) {
					paramsCopy.AddString ("callback_id", this.androidImpl.CallbackManager.AddKingskyDelegate (this.Callback));
				}
				
				this.androidImpl.CallKS (this.MethodName, paramsCopy.ToJsonString ());
			}
		}
	}
}
