using Manager.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Classes
{
    internal class Filemanager
    {
        public static List<Browser> LoadBrowsers()
        {
            try
            {
                var text = File.ReadAllText("browsers.json");
                var browsers = JsonConvert.DeserializeObject<List<Browser>>(text);
                return browsers;
            }
            catch (Exception e)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(e.Message);
                return new List<Browser>();
            }
        }

        public static bool SaveBrowsers(List<Browser> browsers)
        {
            try
            {
                var text = JsonConvert.SerializeObject(browsers);
                File.WriteAllText("browsers.json",text);
                return true;
            }
            catch (Exception e)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(e.Message);
                return false;
            }
        }

        public static bool DeleteBrowser(Browser browser)
        {
            var browsers = LoadBrowsers();
            var existing = browsers.Where(x => x.Path == browser.Path).FirstOrDefault();
            if (existing == null) return true; 
            browsers.Remove(existing);
            Directory.Delete(browser.Path, true);
            return SaveBrowsers(browsers);
        }

        public static bool SaveBrowser(Browser browser)
        {
            try
            {
                var browsers = LoadBrowsers();
                var existing = browsers.Where(x=> x.Path == browser.Path).ToList();
                if (existing.Any())
                {
                    browsers.Remove(existing[0]);
                }
                browsers.Add(browser);
                return SaveBrowsers(browsers);
            }
            catch (Exception e)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(e.Message);
                return false;
            }
        }

        public static Config LoadConfig()
        {
            try
            {
                var text = File.ReadAllText("config.json");
                var json = JsonConvert.DeserializeObject<Config>(text);
                return json;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
