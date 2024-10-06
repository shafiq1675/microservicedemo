
using Catelogue.API.Context;

namespace Catelogue.API.AppHostedServices
{
    public class AppHostedServices : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            CatelogueDBContextSeed.Seed();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
