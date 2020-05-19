using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Events
{
	public class EventDispatcher : IEventDispatcher
	{
		private IServiceProvider ServiceProvider { get; }

		public EventDispatcher(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		public async Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
		{
			var handlers = ServiceProvider.GetServices<IEventHandler<TEvent>>();

			foreach (var handler in handlers)
			{
				await handler.HandleAsync(@event, cancellationToken);
			}
		}
	}
}