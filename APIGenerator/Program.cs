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
generator.Version = "6.0";
generator.OutputPath = @"C:\src";
generator.OpeAPISpecPath = "";
generator.ResolveBuilder();
generator.Generate();

