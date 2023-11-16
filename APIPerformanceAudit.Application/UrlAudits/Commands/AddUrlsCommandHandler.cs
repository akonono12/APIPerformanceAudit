using APIPerformanceAudit.Application.Shared.Results;
using APIPerformanceAudit.Domain.UrlManagement.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Application.UrlAudits.Commands
{
    internal class AddUrlsCommandHandler : IRequestHandler<AddUrlsCommand, CommandResult<Unit>>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IValidator<AddUrlsCommand> _validator;

        public AddUrlsCommandHandler(IUrlRepository urlRepository,IValidator<AddUrlsCommand> validator) 
        {
            _urlRepository = urlRepository;
            _validator = validator;
        }
        public async Task<CommandResult<Unit>> Handle(AddUrlsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CommandResult<Unit> { Success = false, Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList() };
            }

            _urlRepository.AddUrls(request.Urls);

            return new CommandResult<Unit> { Success = true, Data = Unit.Value };
        }
    }
}
