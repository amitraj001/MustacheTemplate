using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGenerator.Builder.NetCore._6._0
{
    public class ControllerModel
    {
        public string? OperationName { get; set; }
        public string? OperationVerb { get; set; }
        public string? OperationSummary { get; set; }
        public string? OperationPath { get; set; }

        public IList<OpenApiParameter> OperationParameters { get; set; }
    }
}
