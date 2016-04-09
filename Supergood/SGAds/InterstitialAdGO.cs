using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Supergood.Unity.SGAds
{
	public class InterstitialAdGO : MonoBehaviour
	{

		public Button FullButton;
		public Button CloseButton;

		public string AppURL {
			get;
			set;
		}

		public void OnFullButtonClick ()
		{
			Debug.Log ("OnFullButtonClick  ...  " + AppURL);
		}

		public void OnCloseButtonClick ()
		{
			Debug.Log ("OnCloseButtonClick  ...  " + AppURL);
			this.gameObject.SetActive (false);
			FullButton.image.sprite = null;
		}

		public void ShowFullImage (Sprite sprite)
		{
			this.gameObject.SetActive (true);
			FullButton.image.sprite = sprite;
		}
	}
}
	
