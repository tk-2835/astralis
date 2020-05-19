using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Astralis.Sapphire.Commands
{
	public sealed class CommandDispatcher : ICommandDispatcher
	{
		private IServiceProvider ServiceProvider { get; }

		public CommandDispatcher(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
		{
			var handler = ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
			await handler.HandleAsync(command, cancellationToken);
		}
	}
}