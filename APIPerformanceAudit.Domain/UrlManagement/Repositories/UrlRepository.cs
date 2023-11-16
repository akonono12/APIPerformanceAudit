using APIPerformanceAudit.Domain.Common;
using APIPerformanceAudit.Domain.UrlManagement.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Domain.UrlManagement.Repositories
{
    public class UrlRepository:IUrlRepository
    {
        private readonly Queue<string> _epList = new Queue<string>();
        private readonly UrlAuditSetting _urlSettings;
        public UrlRepository(IOptions<UrlAuditSetting> urlSettings)
        {
            _urlSettings = urlSettings.Value;
            LoadUrlFromPath();
        }

        public string GetNextUrl()
        {
            return _epList.Dequeue();
        }

        public void AddUrls(List<string> urls)
        {
            foreach (var url in urls)
            {
                _epList.Enqueue(url);
            }
            this.SaveUrl();
        }

        public bool IsUrlAlreadyInTheList(string url)
        {
            return _epList.Contains(url);
        }

        private void SaveUrl()
        {
            var urls = new List<string>(_epList);
            var json = JsonConvert.SerializeObject(urls, Formatting.Indented);
            File.WriteAllText(_urlSettings.FilePath, json);
        }

        private void LoadUrlFromPath() 
        {
            if(File.Exists(_urlSettings.FilePath))
            {
                var urls = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(_urlSettings.FilePath));
                foreach(var url in urls.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    _epList.Enqueue(url);
                }
            }
        }


    }
}
