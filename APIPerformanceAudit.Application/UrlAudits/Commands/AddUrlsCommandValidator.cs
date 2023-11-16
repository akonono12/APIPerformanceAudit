using APIPerformanceAudit.Domain.UrlManagement.Interfaces;
using APIPerformanceAudit.Domain.UrlManagement.Repositories;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Application.UrlAudits.Commands
{
    public class AddUrlsCommandValidator:AbstractValidator<AddUrlsCommand>
    {
        private readonly IUrlRepository _urlRepository;
        public AddUrlsCommandValidator(IUrlRepository urlRepository) 
        {
            _urlRepository = urlRepository;
            RuleFor(x => x.Urls)
            .NotEmpty()
            .NotNull()
            .WithMessage("Field must not be empty");

            RuleForEach(x => x.Urls)
            .Must(IsUrlValid)
            .WithMessage("One of the url is invalid")
            .When(x => x.Urls.Any());

            RuleForEach(x => x.Urls)
            .Must(HasDuplicateUrl)
            .WithMessage("One of the url has the same data in the list")
            .When(x => x.Urls.Any());

        }

        private bool IsUrlValid(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            if(!(Uri.TryCreate(url, UriKind.Absolute, out  Uri uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
            {
                return false;
            }
            return true;
        }

        private bool HasDuplicateUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            
            return !_urlRepository.IsUrlAlreadyInTheList(url);
        }

    }
}
