using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Queries
{
	public sealed class QueryDispatcher : IQueryDispatcher
	{
		private IServiceProvider ServiceProvider { get; }

		public QueryDispatcher(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>
		{
			var handler = ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
			return await handler.HandleAsync(query, cancellationToken);
		}
	}
}