using APIGenerator.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;
using System.Text;


//Entry point
Generator generator = new Generator();
generator.BuilderType = "netcore";
generator.version = "6.0";
generator.ResolveBuilder();
generator.Generate();

