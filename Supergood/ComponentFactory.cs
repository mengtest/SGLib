namespace Supergood.Unity
{
	using UnityEngine;
	
	internal class ComponentFactory
	{
		public const string GameObjectName = "UnityKingskySDKPlugin";
		
		private static GameObject kingskyGameObject;
		
		internal enum IfNotExist
		{
			AddNew,
			ReturnNull
		}
		
		private static GameObject KingskyGameObject
		{
			get
			{
				if (kingskyGameObject == null)
				{
					kingskyGameObject = new GameObject(GameObjectName);
				}
				
				return kingskyGameObject;
			}
		}
		
		/**
         * Gets one and only one component.  Lazy creates one if it doesn't exist
         */
		public static T GetComponent<T>(IfNotExist ifNotExist = IfNotExist.AddNew) where T : MonoBehaviour
		{
			var facebookGameObject = KingskyGameObject;
			
			T component = facebookGameObject.GetComponent<T>();
			if (component == null && ifNotExist == IfNotExist.AddNew)
			{
				component = facebookGameObject.AddComponent<T>();
			}
			
			return component;
		}
		
		/**
         * Creates a new component on the Facebook object regardless if there is already one
         */
		public static T AddComponent<T>() where T : MonoBehaviour
		{
			return KingskyGameObject.AddComponent<T>();
		}
	}
}
