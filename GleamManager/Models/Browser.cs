using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class Browser
    {
        public BuildConfig BuildConfig { get; set; } = new BuildConfig();
        public string Path { get; set; }
        public List<string> AccountProviders { get; set; }
        [JsonIgnore]
        public string _accountProviders { 
            get 
            { 
                if (AccountProviders == null) return string.Empty; 
                return string.Join(", ", AccountProviders); 
            } 
        }
    }
}
