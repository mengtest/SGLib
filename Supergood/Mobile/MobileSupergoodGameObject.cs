namespace Supergood.Unity.Mobile
{
	using System;
	internal abstract class MobileSupergoodGameObject :SupergoodGameObject,IMobileSupergoodCallbackHandler
	{

		private IMobileSupergoodImplementation MobileFacebook {
			get {
				return (IMobileSupergoodImplementation)this.Supergood;
			}
		}
	}
}
