using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class BuildConfig
    {
        public List<string> Extensions { get; set; }
        [JsonIgnore]
        public string _extensions { 
            get 
            {
                if (Extensions == null) return string.Empty;
                return string.Join(", ", Extensions); 
            } 
        }
        [JsonIgnore]
        public string _arguments { 
            get 
            {
                if (Arguments == null) return string.Empty;
                return string.Join(", ", Arguments); 
            } 
        }
        public List<string> Arguments { get; set; }
        public ChromeProxy Proxy { get; set; }
        public bool UseProxy { get; set; }
    }
}
