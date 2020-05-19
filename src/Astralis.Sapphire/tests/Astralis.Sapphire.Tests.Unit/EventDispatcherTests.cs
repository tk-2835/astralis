using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Astralis.Sapphire.Events;
using Xunit;

namespace Astralis.Sapphire.Tests.Unit
{
	public class EventDispatcherTests
	{
		[Fact]
		public async Task DispatchAsync_Should_Dispatch_Event()
		{
			#region Arrange

			var mockedFirstEventHandler = new Mock<IEventHandler<TestEvent>>();
			mockedFirstEventHandler.Setup(x => x.HandleAsync(It.IsAny<TestEvent>(), It.IsAny<CancellationToken>()));

			var mockedSecondEventHandler = new Mock<IEventHandler<TestEvent>>();
			mockedSecondEventHandler.Setup(x => x.HandleAsync(It.IsAny<TestEvent>(), It.IsAny<CancellationToken>()));

			var serviceProvider = new ServiceCollection()
				.AddScoped<IEventDispatcher, EventDispatcher>()
				.AddScoped(_ => mockedFirstEventHandler.Object)
				.AddScoped(_ => mockedSecondEventHandler.Object)
				.BuildServiceProvider();

			using var serviceScope = serviceProvider.CreateScope();

			var eventDispatcher = serviceScope.ServiceProvider.GetRequiredService<IEventDispatcher>();

			var @event = new TestEvent(Guid.NewGuid());

			#endregion Arrange

			#region Act

			await eventDispatcher.DispatchAsync(@event);

			#endregion Act

			#region Assert

			mockedFirstEventHandler.Verify(x => x.HandleAsync(It.IsAny<TestEvent>(), It.IsAny<CancellationToken>()), Times.Once());
			mockedSecondEventHandler.Verify(x => x.HandleAsync(It.IsAny<TestEvent>(), It.IsAny<CancellationToken>()), Times.Once());

			#endregion Assert
		}

		public sealed class TestEvent : IEvent
		{
			public Guid Id { get; }

			public TestEvent(Guid id)
			{
				Id = id;
			}
		}
	}
}