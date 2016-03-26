using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Supergood.Unity;
using Supergood.Unity.Ad;
using Supergood.Unity.Mobile.Android;
using System;
using System.IO;
using System.Collections.Generic;
using Supergood.MiniJSON;
using UnityEngine.SocialPlatforms.GameCenter; 
using UnityEngine.SocialPlatforms;
using Jump;

public class CreatObejct : MonoBehaviour
{

	public Text PidText;
	public Text ErrorCodeText;
	public Text UnityADReady;
	public Text VugleADReady;
	public Text AdColonyReady;
	public Text IAdReady;

	void Start ()
	{
		init ();
	}

	void init ()
	{
		Dictionary<string, object> extras = new Dictionary<string, object> ();
		extras.Add ("alarm_intent_action", "android.intent.action.hsgoldslots.MAIN");

		List<string> needConsumeProducts = new List<string> ();
		needConsumeProducts.Add ("com.superjump.fastgood1");


		List<string> noNeedConsumeProducts = new List<string> ();
		noNeedConsumeProducts.Add ("com.superjump.fastgood1NoNeed");
		var purchaseInfo = new MethodArguments ();

		purchaseInfo.AddPrimative ("purchaseOpen", true);
		purchaseInfo.AddList ("needConsumeProducts", needConsumeProducts);
		purchaseInfo.AddList ("noNeedConsumeProducts", noNeedConsumeProducts);

		extras.Add ("purchaseInfo", purchaseInfo.ToJsonString ());

		SGCross.Init (OnHideUnity, OnInitComplete, HandlePruchaseResult, HandleAuthenticated, extras,OnFacebookInitComplete);
	}


	public void OnFacebookInitComplete(){
		Debug.Log ("OnFacebookInitComplete OnHideUnity ... ");
	}

	public void CallSendEmail ()
	{
		SGCross.CallSendEmail ("wuxikq@gmail.com", "Connect Us");
		SGCross.CallKeepScreenOn (true);
	}

	void OnHideUnity (bool hide)
	{
		Debug.Log ("Kingsky OnHideUnity ... ");
	}

	void OnInitComplete ()
	{
		Debug.Log ("Kingsky InitComplete ... ");
	}

	public void Login ()
	{
		Debug.Log ("start login");
		SGCross.Login ("test", HandleResult);
	}

	protected void HandleResult (IResult result)
	{
		Debug.Log ("Lonig  complete call back");
	}

	public void RegisterNotification ()
	{

		AlarmManager.Instant.Init ();
		long time = (long)(DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		AlarmElement alarmElement = new AlarmElement ();
		alarmElement.content = "aaaaaaaa";
		alarmElement.title = "aaaaaaaa";
		alarmElement.startTime = time + 1000;

		AlarmManager.Instant.AddAlarmInfo (alarmElement);
		
		AlarmElement alarmElement2 = new AlarmElement ();

		alarmElement2.startTime = time + 2000;
		AlarmManager.Instant.AddAlarmInfo (alarmElement2);

		SGCross.RegisterNotification ();
	}

	public void Rate ()
	{
		SGCross.Rate ();
	}

	public void LogEvnet ()
	{
		string eventId = "FaceBookEvnet test";
		Dictionary<string,string> jsonstring = new Dictionary<string,string> ();
		jsonstring.Add ("key1", "value1");
		jsonstring.Add ("key2", "value2");
		jsonstring.Add ("FBEVENT", "event");
		SGCross.LogEvent (eventId, Json.Serialize (jsonstring));
	}

	IEnumerator LinkStreamingFolder (string path, string name)
	{
		Debug.Log ("start .. ");
		string FinalPath = path + "/" + name;
		WWW linkstream = new WWW (FinalPath);
		yield return linkstream;
		Debug.Log (linkstream.text);
	}

	public  void Purchase ()
	{
		Debug.Log ("Lonig  Purchase call back  enddddddddddddddddddddd");
		Dictionary<string,object> jsonstring = new Dictionary<string,object> ();
		jsonstring.Add ("key1", "value1");
		jsonstring.Add ("key2", "value2");
		jsonstring.Add ("FBEVENT", "event");
		SGCross.Purchase ("com.superjump.fastgood1", jsonstring, HandlePruchaseResult);
	}

	protected void HandlePruchaseResult (IPurchaseResult result)
	{
		Debug.Log ("Lonig  PurchaseRusult call back  enddddddddddddddddddddd");
		Debug.Log ("  pid  .... " + result.Pid);
		PidText.text = result.Pid;
		ErrorCodeText.text = result.ErrorCode + "";
	}

	public void ShowLearBroad ()
	{

		//GameCenterPlatform.ShowLeaderboardUI (GPID.leaderboard_,TimeScope.Today);
		Social.Active.ShowLeaderboardUI ();
	}

	public void GameCenterButtonPressed ()
	{
		Social.ReportScore (-5, GPID.leaderboard_, HandleScoreReported);

	}

	private void HandleAuthenticated (bool success)
	{
		platformSuccess = success;
	}

	private bool platformSuccess = false;

	private void HandleScoreReported (bool successed)
	{
		if (successed)
			Debug.Log ("  HandleScoreReported   Complete ... ");
		else
			Debug.Log ("  HandleScoreReported   Complete  error ... ");
	}

	void Update ()
	{  
#if UNITY_ANDROID
		if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Home))  
		{  
			Application.Quit();  
		}  

#endif

//		if (SGConfig.Instant.isLoad) {
//			if (AdManager.instant.isLoad (AdManager.ADCompany.UNITYAD)) {
//				UnityADReady.text = "UnityAdReay";
//			} else {
//				UnityADReady.text = "UnityAdNotReay";
//			}	
//
//			if (AdManager.instant.isLoad (AdManager.ADCompany.VUNGLE)) {
//				VugleADReady.text = "VungleAdReay";
//			} else {
//				VugleADReady.text = "VungleAdNotReay";
//			}
//
//			if (AdManager.instant.isLoad (AdManager.ADCompany.ADCOLONY)) {
//				AdColonyReady.text = "AdColonyReady";
//			} else {
//				AdColonyReady.text = "AdColonyNotReady";
//			}
//	

//		if (AdManager.instant.isLoad (AdManager.ADCompany.IAD)) {
//			IAdReady.text = "IAdReady";
//		} else {
//			IAdReady.text = "IAdNotReady";
//		}

//			if (AdManager.instant.adsVideoController.IsLoad ()) {
//				IAdReady.text = "IAdReady";
//			} else {
//				IAdReady.text = "IAdNotReady";
//			}
//		}
	}

	void Awake ()
	{
	}

	public void ShowAd ()
	{
		AdManager.instant.showAd (AdManager.ADCompany.ADMOB);
	}

	public void ShowVideo ()
	{
		AdManager.instant.showAd (AdManager.ADCompany.ADMOB);
	}

	public void ShowUnityAd ()
	{
		AdManager.instant.showAd (AdManager.ADCompany.UNITYAD);
	}

	public void ShowVungleAd ()
	{
		AdManager.instant.showAd (AdManager.ADCompany.VUNGLE);
	}

	public void ShowAdColony ()
	{
		AdManager.instant.showAd (AdManager.ADCompany.ADCOLONY);
	}
	
	public void ShowIAd ()
	{
		//AdManager.instant.showAd (AdManager.ADCompany.IAD);
		AdManager.instant.adsVideoController.Show ();
	}

	#region Optional: Example of Subscribing to All Events
	
	void OnEnable ()
	{
		AdManager.OnEnableVungle ();
	}
	
	void OnDisable ()
	{
		AdManager.OnDisableVungle ();
	}
	#endregion
	
}
