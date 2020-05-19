using System;
using System.Threading;
using System.Threading.Tasks;
using Astralis.Sapphire.Commands;
using Astralis.Sapphire.Events;
using Astralis.Sapphire.Queries;

namespace Astralis.Sapphire
{
	public class Dispatcher : IDispatcher
	{
		private ICommandDispatcher CommandDispatcher { get; }
		private IQueryDispatcher QueryDispatcher { get; }
		private IEventDispatcher EventDispatcher { get; }

		public Dispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IEventDispatcher eventDispatcher)
		{
			CommandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
			QueryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
			EventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
		}

		public async Task DispatchCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
		{
			await CommandDispatcher.DispatchAsync(command, cancellationToken);
		}

		public async Task DispatchEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
		{
			await EventDispatcher.DispatchAsync(@event, cancellationToken);
		}

		public async Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>
		{
			return await QueryDispatcher.DispatchAsync<TQuery, TResult>(query, cancellationToken);
		}
	}
}
