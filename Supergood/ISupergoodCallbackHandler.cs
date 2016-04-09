
namespace Supergood.Unity
{
	using UnityEngine;
	using System.Collections;

	internal interface ISupergoodCallbackHandler
	{
		void OnInitComplete(string message);
	

		void OnLoginComplete(string message);

		void OnPurchaseComplete(string message);
	}
}
