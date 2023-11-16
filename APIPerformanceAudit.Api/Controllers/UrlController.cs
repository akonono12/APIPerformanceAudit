using APIPerformanceAudit.Application.UrlAudits.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPerformanceAudit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UrlController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUrls([FromBody] AddUrlsCommand command)
        {
           var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
