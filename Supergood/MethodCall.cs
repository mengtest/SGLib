namespace Supergood.Unity
{
	using System.Collections;
	using System.Collections.Generic;
	
	internal abstract class MethodCall<T> where T : IResult
	{
		public MethodCall(SupergoodBase kingskyImpl, string methodName)
		{
			this.Parameters = new MethodArguments();
			this.KingskyImpl = kingskyImpl;
			this.MethodName = methodName;
		}
		
		public string MethodName { get; private set; }
		
		public KingskyDelegate<T> Callback { protected get; set; }
		
		protected SupergoodBase KingskyImpl { get; set; }
		
		protected MethodArguments Parameters { get; set; }
		
		public abstract void Call(MethodArguments args = null);
	}
}
