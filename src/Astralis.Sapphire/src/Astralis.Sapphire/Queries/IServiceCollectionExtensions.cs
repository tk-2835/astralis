using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace Astralis.Sapphire.Queries
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddQueryDispatcher(this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			serviceCollection.TryAdd(new ServiceDescriptor(typeof(IQueryDispatcher), typeof(QueryDispatcher), serviceLifetime));
			return serviceCollection;
		}
	}
}
