using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace Astralis.Sapphire.Events
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddEventDispatcher(this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			serviceCollection.TryAdd(new ServiceDescriptor(typeof(IEventDispatcher), typeof(EventDispatcher), serviceLifetime));
			return serviceCollection;
		}
	}
}
