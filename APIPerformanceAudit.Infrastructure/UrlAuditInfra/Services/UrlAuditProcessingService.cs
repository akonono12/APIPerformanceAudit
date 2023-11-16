using APIPerformanceAudit.Domain.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Infrastructure.UrlAuditInfra.Services
{
    public class UrlAuditProcessingService : BackgroundService
    {
        private readonly UrlAuditInitializerService _initializer;
        private readonly UrlAuditSetting _urlAuditSetting;
        public UrlAuditProcessingService(UrlAuditInitializerService initializer, IOptions<UrlAuditSetting> urlSettings)
        {

            _initializer = initializer;
            _urlAuditSetting = urlSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _initializer.InitializeWorkers(_urlAuditSetting.BGWorkers ?? 1, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
               
                await Task.Delay(TimeSpan.FromSeconds(_urlAuditSetting.BGCycleTimeInSeconds ?? 10), stoppingToken);
            }
        }
    }
}


