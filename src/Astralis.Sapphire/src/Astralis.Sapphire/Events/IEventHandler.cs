using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Events
{
	public interface IEventHandler<TEvent> where TEvent : IEvent
	{
		public Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
	}
}