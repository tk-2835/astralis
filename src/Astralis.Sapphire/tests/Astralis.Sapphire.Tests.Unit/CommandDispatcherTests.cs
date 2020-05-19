using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Astralis.Sapphire.Commands;
using Xunit;

namespace Astralis.Sapphire.Tests.Unit
{
	public class CommandDispatcherTests
	{
		[Fact]
		public async Task DispatchAsync_Should_Dispatch_Command()
		{
			#region Arrange

			var mockedCommandHandler = new Mock<ICommandHandler<TestCommand>>();
			mockedCommandHandler.Setup(x => x.HandleAsync(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()));

			var serviceProvider = new ServiceCollection()
				.AddScoped<ICommandDispatcher, CommandDispatcher>()
				.AddScoped(_ => mockedCommandHandler.Object)
				.BuildServiceProvider();

			using var serviceScope = serviceProvider.CreateScope();

			var commandDispatcher = serviceScope.ServiceProvider.GetRequiredService<ICommandDispatcher>();

			var command = new TestCommand(Guid.NewGuid());

			#endregion Arrange

			#region Act

			await commandDispatcher.DispatchAsync(command);

			#endregion Act

			#region Assert

			mockedCommandHandler.Verify(x => x.HandleAsync(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()), Times.Once());

			#endregion Assert
		}

		public sealed class TestCommand : ICommand
		{
			public Guid Id { get; }

			public TestCommand(Guid id)
			{
				Id = id;
			}
		}
	}
}