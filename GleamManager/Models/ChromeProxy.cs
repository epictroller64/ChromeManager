using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class ChromeProxy
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseAuth { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Protocol { get; set; }
    }
}
