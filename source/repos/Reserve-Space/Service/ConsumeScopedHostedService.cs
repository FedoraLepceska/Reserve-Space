using Microsoft.Extensions.DependencyInjection;
using Service.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class ConsumeScopedHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public ConsumeScopedHostedService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await DoWork();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IBackgroundEmailSender>();
                await scopedProcessingService.DoWork();
            }
        }

    }
}
