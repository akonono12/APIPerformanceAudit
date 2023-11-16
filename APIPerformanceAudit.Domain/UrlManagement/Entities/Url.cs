using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPerformanceAudit.Domain.UrlManagement.Entities
{
    public class Url
    {
        private Url() { }

        public string EndpoindName { get; set; } 

        public Url(string url) 
        {
            EndpoindName = url;
        }
    }
}
