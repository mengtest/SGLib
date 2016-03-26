namespace Supergood.Unity.Mobile.IOS
{
	using System.Collections;
	using UnityEngine;
	
	internal class IOSSupergoodLoader : SGCross.CompiledSupergoodLoader
	{
		protected override SupergoodGameObject SGGameObject
		{
			get
			{
				IOSSupergoodGameObject iosSG = ComponentFactory.GetComponent<IOSSupergoodGameObject>();
				if (iosSG.Supergood == null)
				{
					iosSG.Supergood = new IOSSupergood();
				}
				
				return iosSG;
			}
		}
	}
}
