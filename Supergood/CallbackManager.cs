namespace Supergood.Unity
{
	using System;
	using System.Collections.Generic;
	
	internal class CallbackManager
	{
		private IDictionary<string, object> kingskyDelegates = new Dictionary<string, object> ();
		private int nextAsyncId;
		
		public string AddKingskyDelegate<T> (KingskyDelegate<T> callback) where T : IResult
		{
			if (callback == null) {
				return null;
			}
			
			this.nextAsyncId++;
			this.kingskyDelegates.Add (this.nextAsyncId.ToString (), callback);
			return this.nextAsyncId.ToString ();
		}
		
		public void OnKingskyResponse (IInternalResult result)
		{
			if (result == null || result.CallbackId == null) {
				return;
			}
			
			object callback;
			if (this.kingskyDelegates.TryGetValue (result.CallbackId, out callback)) {
				CallCallback (callback, result);
				this.kingskyDelegates.Remove (result.CallbackId);
			}
		}
		
		// Since unity mono doesn't support covariance and contravariance use this hack
		private static void CallCallback (object callback, IResult result)
		{
			if (callback == null || result == null) {
				return;
			}


			if ( CallbackManager.TryCallCallback<ILoginResult> (callback, result)||CallbackManager.TryCallCallback<IPurchaseResult> (callback, result)) {
				return;
			}
		
			throw new NotSupportedException ("Unexpected result type: " + callback.GetType ().FullName);
		}
		
		private static bool TryCallCallback<T> (object callback, IResult result) where T : IResult
		{
			var castedCallback = callback as KingskyDelegate<T>;
			if (castedCallback != null) {
				castedCallback ((T)result);
				return true;
			}
			
			return false;
		}
	}
}

