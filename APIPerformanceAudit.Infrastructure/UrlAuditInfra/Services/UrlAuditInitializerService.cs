using APIPerformanceAudit.Infrastructure.UrlAuditInfra.Backgrounds;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Infrastructure.UrlAuditInfra.Services
{
    public class UrlAuditInitializerService
    {
        private readonly IServiceProvider _serviceProvider;

        public UrlAuditInitializerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void InitializeWorkers(int workerCount,CancellationToken cancellationToken)
        {
            for (int i = 0; i < workerCount; i++)
            {
                var worker = ActivatorUtilities.CreateInstance<UrlAuditBackgroundWorker>(_serviceProvider);
                worker.StartAsync(cancellationToken,i);
            }
        }
    }
}
