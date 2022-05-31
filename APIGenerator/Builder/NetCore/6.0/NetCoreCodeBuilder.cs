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
        public string basePath = @"..\..\..\Template\NetCore\6.0\";
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
            List<ControllerModel> operations = new List<ControllerModel>();
            foreach (var _path in openApiDocument.Paths)
            {
                foreach (var _operation in _path.Value.Operations)
                {
                    ControllerModel op = new ControllerModel();
                    op.OperationName = _operation.Value.OperationId;
                    op.OperationSummary = _operation.Value.Summary;
                    op.OperationPath = _path.Key.ToString(); 
                    op.OperationVerb = _operation.Key.ToString();
                    op.OperationParameters = _operation.Value.Parameters;
                    operations.Add(op);
                }
            }

            //Get template
            string readText = File.ReadAllText(GetTemplatePath(Components.Controller));

            //Init Mustache engine
            FormatCompiler compiler = new FormatCompiler();
            compiler.RemoveNewLines = false;

            //Merge template with data
            Mustache.Generator generator = compiler.Compile(readText);
            var input = new
            {
                controllername = "ApplicationAPIController", //Get from openapi spec
                basenamespace = "API.Application", //Get from openapi spec
                Operations = operations 
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

        enum Components
        {
            Controller,
            Csproj,
            Dockerfile,
            Model,
            Sln,
            Program
        }

        string GetTemplatePath(Components component)
        {
           return basePath + component + ".mustache";
        }

    }
}
