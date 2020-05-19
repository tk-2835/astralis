using System.Threading;
using System.Threading.Tasks;
using Astralis.Sapphire.Commands;
using Astralis.Sapphire.Events;
using Astralis.Sapphire.Queries;

namespace Astralis.Sapphire
{
	public interface IDispatcher
	{
		public Task DispatchCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
		public Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>;
		public Task DispatchEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
	}
}
