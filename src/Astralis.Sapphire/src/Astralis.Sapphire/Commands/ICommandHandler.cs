using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Commands
{
	public interface ICommandHandler<TCommand> where TCommand : ICommand
	{
		public Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
	}
}