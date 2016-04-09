namespace Supergood.Unity
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	
	internal class LoginResult : ResultBase, ILoginResult
	{
		internal LoginResult(string response) : base(response)
		{

		}
	}
}
