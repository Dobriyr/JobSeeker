using FluentResults;

namespace JobSeeker.BLL.MediatR.ResultVariations
{
	public class NullResult<T> : Result<T>
	{
		public NullResult() : base()
		{
		}
	}
}
