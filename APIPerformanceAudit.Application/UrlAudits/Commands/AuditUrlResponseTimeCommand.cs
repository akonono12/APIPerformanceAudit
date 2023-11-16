using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Application.UrlAudits.Commands
{
    public class AuditUrlResponseTimeCommand : IRequest<int>
    {
        public string Url { get; set; }
    
    }
}
