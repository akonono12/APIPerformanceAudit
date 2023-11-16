using APIPerformanceAudit.Domain.UrlManagement.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Application.UrlAudits.Commands
{
    internal class AuditUrlResponseTimeCommandHandler : IRequestHandler<AuditUrlResponseTimeCommand, int>
    {
        private readonly IUrlRepository _urlRepository;
        public AuditUrlResponseTimeCommandHandler(IUrlRepository urlRepository) 
        {
            _urlRepository = urlRepository;
        }
        public async Task<int> Handle(AuditUrlResponseTimeCommand request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(request.Url, cancellationToken);
                var responseTime = (int)stopwatch.ElapsedMilliseconds;

                return responseTime;
            }
        
        }
    }
}
