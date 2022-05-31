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
        public string builderType;
        public string version;

        public Generator()
        {

        }

        public Generator(string _builderType, string _version)
        {
            this.builderType = _builderType;
            this.version = _version;

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
            switch (builderType)
            {
                case "netcore":
                    builder = new NetCoreCodeBuilder();
                    break;
                default:
                    builder = null;
                    break;
            }

        }
    }
}
