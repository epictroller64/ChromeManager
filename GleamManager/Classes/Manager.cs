using Manager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Classes
{
    public class Manager
    {
        Config Config { get; set; } = Filemanager.LoadConfig();
        public Manager()
        {

        }
        public Process StartBrowser(Browser browser)
        {
            var builder = new ChromeBuilder(browser.Path, Config.Port.ToString());
            builder.SetExtensions(browser.BuildConfig.Extensions);
            var run = builder.Build();
            return Process.Start(Config.ChromePath + "/chrome.exe", run);
        }

        public void StopBrowser(Process process, Browser browser)
        {
            process.Kill();
            Filemanager.SaveBrowser(browser);
        }

        public void DeleteBrowser(Browser browser)
        {
            Filemanager.DeleteBrowser(browser);
            Directory.Delete(browser.Path, true);
        }

        public void NewBrowser()
        {
            var browser = new Browser();
            browser.Path = Path.GetFullPath($"Profiles/{Generator.RandomString(12, true)}");
            Directory.CreateDirectory(browser.Path);
            Filemanager.SaveBrowser(browser);
        }
    }
}
