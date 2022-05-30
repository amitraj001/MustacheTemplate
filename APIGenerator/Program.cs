using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;
using Mustache;
using System.Text;

await OpenAPIRead();

async Task OpenAPIRead()
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/")
    };

    var stream = await httpClient.GetStreamAsync("master/examples/v3.0/petstore.yaml");

    // Read V3 as YAML
    var openApiDocument = new OpenApiStreamReader().Read(stream, out var diagnostic);

    List<operationname> operations = new List<operationname>();
    foreach (var path in openApiDocument.Paths)
    {
        operationname op = new operationname();
        op.name = path.Key;
        operations.Add(op);
    }

    TestMustache("API.Application", operations);
}

void TestMustache(string controllernamespace, List<operationname> _operationname)
{
    string path = @"C:\APIGenerator\APIGenerator\Controller.mustache";
    string readText = File.ReadAllText(path);

    FormatCompiler compiler = new FormatCompiler();
    compiler.RemoveNewLines = false;
    Generator generator = compiler.Compile(readText);
    var input = new
    {
        controllername = "ApplicationAPIController",
        controllernamespace = "API.Application.Controller",
        operationname = _operationname 
    };

    string result = generator.Render(input);

    string outputpath = @"C:\APIGenerator\APIGenerator\Controller.cs";

    File.WriteAllText(outputpath, result, Encoding.UTF8);
}

public class operationname
{
    public string name { get; set; }
}