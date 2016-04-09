using UnityEngine;
using System.Collections;

namespace Supergood.Unity.Load
{
	public interface LoadComplete
	{

		void OnSuccess (WWW www);
		
		void OnFail (string error);

	}
}
