using APIPerformanceAudit.Application.UrlAudits.Commands;
using APIPerformanceAudit.Domain.UrlManagement.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Infrastructure.UrlAuditInfra.Backgrounds
{
    internal class UrlAuditBackgroundWorker
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMediator _mediator;

        public UrlAuditBackgroundWorker(IUrlRepository urlRepository, IMediator mediator)
        {
            _urlRepository = urlRepository;
            _mediator = mediator;
        }

        public async Task StartAsync(CancellationToken stoppingToken,int workerInstance)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string url = _urlRepository.GetNextUrl();

                try
                {
                   var result = await _mediator.Send(new AuditUrlResponseTimeCommand() { Url = url });
                   Console.WriteLine($"Using Background Worker: {workerInstance} (URL: {url}, Response Time: {result} ms)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"URL: {url}, Error: {ex.Message}");
                }
            }
        }

    }
}

