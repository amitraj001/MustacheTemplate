using APIGenerator.Builder;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;
using System.Text;

Generator generator = new Generator("netcore","6.0");
generator.Generate();


public class operationname
{
    public string name { get; set; }
}