
namespace Supergood.Unity
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	
	internal class PurchaseResult : ResultBase, IPurchaseResult
	{

		public static readonly string IAP_PID_TAG = "pid";
		public static readonly string EXTRAS = "extras";
		public static readonly int IAPErrorCode_Successed = 1;
		public static readonly int IAPErrorCode_Cancle= 2;
		public static readonly int IAPErrorCode_CannotFindPid= 3;
		public static readonly int IAPErrorCode_NotSupportBuy= 4;
		public static readonly int IAPErrorCode_Restore= 5;
		public static readonly int IAPErrorCode_OtherError= 6;
		public static readonly int IAPErrorCode_ALREADY_OWN = 7;
		public static readonly string IAPErrorCode_TAG= "errorcode";

		internal PurchaseResult(string response) : base(response)
		{
			if (this.ResultDictionary != null && this.ResultDictionary.ContainsKey(PurchaseResult.IAP_PID_TAG))
			{
				this.Pid =  this.ResultDictionary.GetValueOrDefault<string>(PurchaseResult.IAP_PID_TAG);
			}

			if (this.ResultDictionary != null && this.ResultDictionary.ContainsKey (PurchaseResult.IAPErrorCode_TAG)) {
				this.ErrorCode = int.Parse(this.ResultDictionary.GetValueOrDefault<string> (PurchaseResult.IAPErrorCode_TAG));
			} else {
				this.ErrorCode = IAPErrorCode_OtherError;
			}
		}

		public string Pid { get; private set; }
		public int ErrorCode { get; private set; }
	}
}