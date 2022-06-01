using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Mustache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using APIGenerator.Shared;

namespace APIGenerator.Builder.NetCore._6._0
{
    internal class NetCoreCodeBuilder : ICodeBuilder
    {
      
        public OpenApiDocument openApiDocument = null;
        public string basePath = @"..\..\..\Template\NetCore\";
        public string Version{ get; private set; }
        public string OutputPath { get; set; }
        string projname = "PetStore.API"; //pick from config

        public NetCoreCodeBuilder(string version , string outputpath)
        {
            Version = version;
            OutputPath = outputpath;
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
            //Create template input
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

            var input = new
            {
                controllername = "PetStoreController", //Get from openapi spec
                basenamespace = "Pet.API", //Get from openapi spec
                Operations = operations
            };

            string outputpath = @$"{OutputPath}\{projname}\Controllers\{input.controllername}.cs";

            MergeTemplate(Component.Controller, input, outputpath);
        }

        public void GenerateFolders()
        {
            Directory.CreateDirectory(@$"{OutputPath}");
            Directory.CreateDirectory(@$"{OutputPath}");
            Directory.CreateDirectory(@$"{OutputPath}\components");
            Directory.CreateDirectory(@$"{OutputPath}\obj");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\bin");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\components");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\obj");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\.config");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\Controllers");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\Models");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\Properties");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\Services");
            Directory.CreateDirectory(@$"{OutputPath}\{projname}\DTO");

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

      

        void MergeTemplate(Component component, dynamic input, string outputpath)
        {
            var settings = Utility.GetSettings(basePath, Version);
            
            //Get template
            string readText = File.ReadAllText(Utility.GetTemplatePath(component, Version, basePath));

            //Init Mustache engine
            FormatCompiler compiler = new FormatCompiler();
            compiler.RemoveNewLines = false;

            //Merge template with data
            Mustache.Generator generator = compiler.Compile(readText);

            //Create output file
            string result = generator.Render(input);

            //Get from appsettings config
            File.WriteAllText(outputpath, result, Encoding.UTF8);
        }

    }
}
