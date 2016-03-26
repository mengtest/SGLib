
namespace Supergood.Unity
{
	/// <summary>
	/// The result of a purchase request.
	/// </summary>
	public interface IPurchaseResult : IResult
	{
		string Pid{ get;}
		int ErrorCode{ get;}
	}


}