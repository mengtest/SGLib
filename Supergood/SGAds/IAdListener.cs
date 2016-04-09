
using System;

namespace Supergood.Unity.SGAds
{
	// Interface for the methods to be invoked by the native plugin.
	internal interface IAdListener
	{
		void AdLoaded();
		void AdFailedToLoad(string message);
		void AdOpened();
		void AdClosing();
		void AdClosed();
		void AdLeftApplication();
	}
}
