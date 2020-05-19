using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Events
{
	public interface IEventDispatcher
	{
		public Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
	}
}