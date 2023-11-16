using APIPerformanceAudit.Application.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Application.UrlAudits.Commands
{
    public class AddUrlsCommand:IRequest<CommandResult<Unit>>
    {
        public List<string> Urls { get; set; }
    }
}
