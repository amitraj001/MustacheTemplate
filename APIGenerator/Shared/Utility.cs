using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGenerator.Shared
{
    static class Utility
    {
        public static string GetTemplatePath(Component component, string version, string basePath)
        {
            return $"{basePath}\\{version}\\{component}.mustache";
        }

        public static dynamic GetSettings(string basePath, string version)
        {
            using (StreamReader r = new StreamReader($"{basePath}\\{version}\\appsettings.json"))
            {
                string json = r.ReadToEnd();
                List<Settings> items = JsonConvert.DeserializeObject<List<Settings>>(json);
                return items;
            }

        }
    }
}
