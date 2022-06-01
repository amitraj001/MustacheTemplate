using APIGenerator.Builder.NetCore._6._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGenerator.Builder
{
    public class Generator
    {
        public ICodeBuilder builder = null;
        public string BuilderType { get; set; }
        public string Version { get; set; }
        public string OutputPath { get; set; }
        public string OpeAPISpecPath { get; set; }

        public Generator()
        {

        }

        public Generator(string _builderType, string _version)
        {
            ResolveBuilder();
        }

        public void setBuilder(ICodeBuilder _builder)
        {
            builder = _builder;
        }

        public void Generate()
        {
            builder.Generate();
        }

        public void ResolveBuilder()
        {
            switch (BuilderType)
            {
                case "netcore":
                    builder = new NetCoreCodeBuilder(Version, OutputPath);
                    break;
                default:
                    builder = null;
                    break;
            }

        }
    }
}
