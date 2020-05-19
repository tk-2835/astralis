using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace Astralis.Sapphire.Commands
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddCommandDispatcher(this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			serviceCollection.TryAdd(new ServiceDescriptor(typeof(ICommandDispatcher), typeof(CommandDispatcher), serviceLifetime));
			return serviceCollection;
		}
	}
}
