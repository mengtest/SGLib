
using Supergood.Unity.Load;

namespace Supergood.Unity
{
	using UnityEngine;
	using System.Collections;

	public delegate void InitDelegate ();
	
	public delegate void KingskyDelegate<T> (T result) where T : IResult;

	public delegate void HideUnityDelegate(bool isUnityShown);


	internal abstract class SupergoodGameObject : MonoBehaviour,ISupergoodCallbackHandler
	{

		public  ISupergoodImplementation Supergood { get; set; }
		
		public bool Initialized { get; private set; }
		
		public void Awake ()
		{
			MonoBehaviour.DontDestroyOnLoad (this);
		
			
			// run whatever else needs to be setup
			this.OnAwake ();
		}
		public void OnInitComplete(string message)
		{
			this.Supergood.OnInitComplete(message);
			this.Initialized = true;
		}

		public void OnLoginComplete(string message)
		{
			this.Supergood.OnLoginComplete(message);
		}


		public void  OnPurchaseComplete(string message){
			this.Supergood.OnPurchaseComplete (message);
		}

		public void LoadResource(Loader loader){
			StartCoroutine (loader.Download());
		}

		// use this to call the rest of the Awake function
		protected virtual void OnAwake ()
		{
		}


	}
}
