using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build configuration from appsettings.json and environment variables
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Configure EF Core options
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                config.GetConnectionString("Default")
                ?? "Server=.;Database=EcomDb;Trusted_Connection=True;TrustServerCertificate=True"
            );

            // Use no-op mediator for design-time
            var mediator = new NoMediator();

            return new ApplicationDbContext(optionsBuilder.Options, mediator);
        }

        private class NoMediator : IMediator
        {
            // Publish
            public Task Publish(object notification, CancellationToken cancellationToken = default)
                => Task.CompletedTask;

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
                where TNotification : INotification
                => Task.CompletedTask;

            // Send: Generic over TRequest and TResponse (must have constraint)
            public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                where TRequest : IRequest<TResponse>
                => Task.FromResult(default(TResponse)!);

            // Send: Generic over TResponse
            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
                => Task.FromResult(default(TResponse)!);

            // Send: Non-generic
            public Task<object?> Send(object request, CancellationToken cancellationToken = default)
                => Task.FromResult<object?>(null);

            // Stream: Generic
            public async IAsyncEnumerable<TResponse> CreateStream<TResponse>(
                IStreamRequest<TResponse> request,
                [EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                yield break;
            }
            public async IAsyncEnumerable<object?> CreateStream(
                object request,
                [EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                yield break;
            }

            public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
            {
                throw new NotImplementedException();
            }
        }




    }
}
