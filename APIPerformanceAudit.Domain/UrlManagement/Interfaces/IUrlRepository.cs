using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Domain.UrlManagement.Interfaces
{
    public interface IUrlRepository
    {
        public string GetNextUrl();
        public void AddUrls(List<string> urls);
        public bool IsUrlAlreadyInTheList(string url);
    }
}
