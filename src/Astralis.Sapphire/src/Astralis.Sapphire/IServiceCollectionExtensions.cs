using Microsoft.Extensions.DependencyInjection;
using Astralis.Sapphire.Commands;
using Astralis.Sapphire.Events;
using Astralis.Sapphire.Queries;

namespace Astralis.Sapphire
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddDispatcher(this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			serviceCollection.AddCommandDispatcher(serviceLifetime);
			serviceCollection.AddQueryDispatcher(serviceLifetime);
			serviceCollection.AddEventDispatcher(serviceLifetime);
			return serviceCollection;
		}
	}
}
