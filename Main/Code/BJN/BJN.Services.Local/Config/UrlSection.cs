using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Domain.Config
{
    public class UrlSection : ConfigurationSection
    {
        [ConfigurationProperty("baseUrl")]
        public BaseUrlElement BaseUrl
        {
            get { return (BaseUrlElement)this["baseUrl"]; }
        }
    }

    public class BaseUrlElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string) this["name"]; }
        }
    }
}
