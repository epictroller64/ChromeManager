using Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Classes
{
    internal class ChromeBuilder
    {
        List<string> Extensions { get; set; } = new List<string>();
        List<string> Arguments { get; set; } = new List<string>();
        string UserDataDir { get; set; }
        string DebuggerPort { get; set; }
        ChromeProxy ChromeProxy { get; set; } = null;
        string ProxyPath { get; set; }
        string ProxyAuthPath { get; set; }
        BuildConfig BuildConfig { get; set; }
        string ChromeString = $@" ";

        public ChromeBuilder(string userDataDir, string debuggerPort)
        {
            UserDataDir = userDataDir;
            DebuggerPort = debuggerPort;
        }

        public ChromeBuilder(string userDataDir, string debuggerPort, ChromeProxy proxy)
        {
            UserDataDir = userDataDir;
            DebuggerPort = debuggerPort;
            ChromeProxy = proxy;
        }

        public void AddConfig(BuildConfig config)
        {
            BuildConfig = config;
            if(config.Extensions != null)
            {
                SetExtensions(config.Extensions);
            }
            if(config.Arguments != null)
            {

            }
        }

        public void SetExtensions(List<string> extensions)
        {
            if (extensions == null) return;
            foreach (var item in extensions)
            {
                Extensions.Add(Path.GetFullPath(item));
            }
        }

        public void SetArguments(List<string> extensions)
        {
            if(extensions == null) return;
            Arguments = extensions;
        }

        private void GetExtensionPaths()
        {
            ProxyPath = Path.GetFullPath("Extensions/proxy");
            ProxyAuthPath = Path.GetFullPath("Extensions/proxy_auth");
        }

        private void AddArgument(string argument)
        {
            Arguments.Add(argument);
        }

        private void AddExtensions()
        {
            GetExtensionPaths();
            Extensions.Add(ProxyPath);
            if (BuildConfig.UseProxy)
            {
                if (ChromeProxy.Username != null)
                {
                    Extensions.Add(ProxyAuthPath);
                }
            }
            var argument = "--load-extension=";
            foreach (var item in Extensions)
            {
                argument += $"{item},";
            }
            argument = argument.Remove(argument.Length - 1);
            AddArgument(argument);
            //AddArgument("--blink-settings=imagesEnabled=false");

        }

        public string Build()
        {
            ChromeString += $"--remote-debugging-port={DebuggerPort} ";
            ChromeString += $"--user-data-dir={UserDataDir} ";
            AddExtensions();
            AddArgument("--start-maximized");
            AddArgument("--disable-infobars");
            AddArgument("--disable-gpu-vsync");
            foreach (var item in Arguments)
            {
                ChromeString += $"{item} ";
            }
            return ChromeString;
        }
    }
}
