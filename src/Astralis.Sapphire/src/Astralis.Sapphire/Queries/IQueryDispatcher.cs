using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Queries
{
	public interface IQueryDispatcher
	{
		public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>;
	}
}