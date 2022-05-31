using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Mustache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGenerator.Builder.NetCore._6._0
{
    internal class NetCoreCodeBuilder : ICodeBuilder
    {
        public OpenApiDocument openApiDocument = null;
        public NetCoreCodeBuilder()
        {

        }

        public async Task InitiliazeYaml()
        {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/")
                };

                var stream = await httpClient.GetStreamAsync("master/examples/v3.0/petstore.yaml");

                // Read V3 as YAML
                openApiDocument = new OpenApiStreamReader().Read(stream, out var diagnostic);

        }

        public void GenerateAppSettings()
        {
        }

        public void GenerateAttributes()
        {
        }

        public void GenerateControllers()
        {
            List<operationname> operations = new List<operationname>();
            foreach (var _path in openApiDocument.Paths)
            {
                operationname op = new operationname();
                op.name = _path.Key;
                operations.Add(op);
            }

            //Get template
            string path = @"C:\APIGenerator\APIGenerator\Template\NetCore\6.0\Controller.mustache";
            string readText = File.ReadAllText(path);

            //Init Mustache engine
            FormatCompiler compiler = new FormatCompiler();
            compiler.RemoveNewLines = false;

            //Merge template with data
            Mustache.Generator generator = compiler.Compile(readText);
            var input = new
            {
                controllername = "ApplicationAPIController", //Get from openapi spec
                basenamespace = "API.Application.Controller", //Get from openapi spec
                operationname = operations //Get from openapi spec
            };

            //Create output file
            string result = generator.Render(input);

            //Get from appsettings config
            string outputpath = @"C:\APIGenerator\APIGenerator\Output\Controller.cs";
            File.WriteAllText(outputpath, result, Encoding.UTF8);

        }

        public void GenerateFolders()
        {
        }

        public void GenerateModels()
        {
        }

        public void GenerateStartUp()
        {
        }

        public void Generate()
        {
            InitiliazeYaml().Wait();
            GenerateFolders();
            GenerateModels();
            GenerateControllers();
            GenerateStartUp();
            GenerateAttributes();
            GenerateAppSettings();
        }


    }
}
