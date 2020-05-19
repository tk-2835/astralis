using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Commands
{
	public interface ICommandDispatcher
	{
		public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
	}
}