namespace Supergood.Unity.Mobile.IOS
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	
	internal class IOSSupergood : MobileSupergood
	{
		private const string CancelledResponse = "{\"cancelled\":true}";
		private bool limitEventUsage;
		
		public IOSSupergood()
			: this(new CallbackManager())
		{
		}
		
		public IOSSupergood(CallbackManager callbackManager)
			: base(callbackManager)
		{
		}
		
		public enum FBInsightsFlushBehavior
		{
			FBInsightsFlushBehaviorAuto,
			FBInsightsFlushBehaviorExplicitOnly,
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


			IOSSupergood.KSIOSInit();
		}
		
		public override void Login (
			string params_str, KingskyDelegate<ILoginResult> callback)
		{
			IOSSupergood.KSLogIn(this.AddCallback(callback), "aaaaa");
		}
		
		public override bool LimitEventUsage {
			get {
				return this.limitEventUsage;
			}
			
			set {
				this.limitEventUsage = value;
//				this.CallKS ("SetLimitEventUsage", value.ToString ());
			}

		}
		
		public override void CallSendEmail (string email, string subject)
		{
//			MethodArguments args = new MethodArguments ();
//			args.AddString ("email", email);
//			args.AddString ("subject", subject);
//			
//			var sendEmialCall = new JavaMethodCall<IResult> (this, "SendEmail");
//			sendEmialCall.Call (args);
			
		}
		
		public override void CallKeepScreenOn (bool isKeepScreenOn)
		{
//			MethodArguments args = new MethodArguments ();
//			args.AddPrimative ("isKeepScreenOn", true);
//			var keepScreenOnCall = new JavaMethodCall<IResult> (this, "KeepScreenOn");
//			keepScreenOnCall.Call (args);
		}
		
		
		public override void RegisterNotification (){
//			var args = new MethodArguments ();
//			
//			args.AddString (AlarmManager.AlarmInfoTag, AlarmManager.Instant.GetAlarmInfos());
//			
//			var initCall = new JavaMethodCall<IResult> (this, "RegisterNotification");
//			initCall.Call (args);
		}
		
		
		public override void Rate(){
//			var args = new MethodArguments ();
//			
//			args.AddString ("appName","com.thegamecontriver.sharetext" );
//			
//			var initCall = new JavaMethodCall<IResult> (this, "Rate");
//			initCall.Call (args);
		}
		

		
		public override void LogEvent(string eventID,string keyvalueJSONStr){
//			var args = new MethodArguments ();
//			
//			args.AddString ("eventID",eventID );
//			args.AddString ("keyvalueJSONStr",keyvalueJSONStr );
//			
//			var initCall = new JavaMethodCall<IResult> (this, "LogEvent");
//			initCall.Call (args);
		}
		
		public override void Purchase(string pid,Dictionary<string, object> extras,KingskyDelegate<IPurchaseResult> callback){
			extras.Add (PurchaseResult.IAP_PID_TAG,pid);
			NativeDict dict = MarshallDict(extras);
			IOSSupergood.KSIOSPurchase(this.AddCallback(callback), dict.NumEntries, dict.Keys, dict.Values);
		}

		public override bool AppIsInstalled(string bundleId){
			return false;
		}
		
		#if UNITY_IOS
		[DllImport ("__Internal")]
		private static extern void KSIOSInit();
		
		[DllImport ("__Internal")]
		private static extern void KSLogIn(
			int requestId,
			string scope);
		[DllImport ("__Internal")]
		private static extern void KSIOSPurchase(
			int requestId,
			int numParams,
			string[] paramKeys,
			string[] paramVals);
		#else
		private static  void KSIOSInit(){
		}
		private static  void KSLogIn(int requestId,string scope){
		}
		private static  void KSIOSPurchase(
			int requestId,
			int numParams,
			string[] paramKeys,
			string[] paramVals){
		}
		#endif
		
		private static NativeDict MarshallDict(Dictionary<string, object> dict)
		{
			NativeDict res = new NativeDict();
			
			if (dict != null && dict.Count > 0)
			{
				res.Keys = new string[dict.Count];
				res.Values = new string[dict.Count];
				res.NumEntries = 0;
				foreach (KeyValuePair<string, object> kvp in dict)
				{
					res.Keys[res.NumEntries] = kvp.Key;
					res.Values[res.NumEntries] = kvp.Value.ToString();
					res.NumEntries++;
				}
			}
			
			return res;
		}
		
		private static NativeDict MarshallDict(Dictionary<string, string> dict)
		{
			NativeDict res = new NativeDict();
			
			if (dict != null && dict.Count > 0)
			{
				res.Keys = new string[dict.Count];
				res.Values = new string[dict.Count];
				res.NumEntries = 0;
				foreach (KeyValuePair<string, string> kvp in dict)
				{
					res.Keys[res.NumEntries] = kvp.Key;
					res.Values[res.NumEntries] = kvp.Value;
					res.NumEntries++;
				}
			}
			
			return res;
		}
		
		private int AddCallback<T>(KingskyDelegate<T> callback) where T : IResult
		{
			string asyncId = this.CallbackManager.AddKingskyDelegate(callback);
			return Convert.ToInt32(asyncId);
		}
		
		private class NativeDict
		{
			public NativeDict()
			{
				this.NumEntries = 0;
				this.Keys = null;
				this.Values = null;
			}
			
			public int NumEntries { get; set; }
			
			public string[] Keys { get; set; }
			
			public string[] Values { get; set; }
		}
	}
}

