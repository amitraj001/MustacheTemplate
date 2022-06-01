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

        public static string IsRequired(bool required)
        {
            return (required == true ? "[Required]" : "");
        }

        public static string ParameterAttribute(string input)
        {
            switch(input)
            {
                case "query":
                    return "[FromQuery]";
                case "path":
                    return "[FromRoute]";
                default:
                    return "";
            }
        }

        public static string MapDataType(string input)
        {
            switch (input)
            {
                case "int32":
                case "int64":
                case "integer":
                    return "int";
                case "float":
                    return "float";
                default:
                    return input;
            }
        }
    }
}
