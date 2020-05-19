using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Astralis.Sapphire.Queries;
using Xunit;

namespace Astralis.Sapphire.Tests.Unit
{
	public class QueryDispatcherTests
	{
		[Fact]
		public async Task DispatchAsync_Should_Dispatch_Query_And_Return_Expected_Result()
		{
			#region Arrange

			var queryHandlerMock = new Mock<IQueryHandler<TestQuery, TestResult>>();
			queryHandlerMock.Setup(x => x.HandleAsync(It.IsAny<TestQuery>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((TestQuery query, CancellationToken cancellationToken) =>
				{
					return new TestResult(query.Id);
				});

			var serviceProvider = new ServiceCollection()
				.AddScoped<IQueryDispatcher, QueryDispatcher>()
				.AddScoped(_ => queryHandlerMock.Object)
				.BuildServiceProvider();

			using var serviceScope = serviceProvider.CreateScope();

			var queryDispatcher = serviceScope.ServiceProvider.GetRequiredService<IQueryDispatcher>();

			var query = new TestQuery(Guid.NewGuid());

			#endregion Arrange

			#region Act

			var result = await queryDispatcher.DispatchAsync<TestQuery, TestResult>(query);

			#endregion Act

			#region Assert

			queryHandlerMock.Verify(x => x.HandleAsync(It.IsAny<TestQuery>(), It.IsAny<CancellationToken>()), Times.Once());

			Assert.Equal(query.Id, result.Id);

			#endregion Assert
		}

		public sealed class TestQuery : IQuery<TestResult>
		{
			public Guid Id { get; }

			public TestQuery(Guid id)
			{
				Id = id;
			}
		}

		public sealed class TestResult
		{
			public Guid Id { get; }

			public TestResult(Guid id)
			{
				Id = id;
			}
		}
	}
}