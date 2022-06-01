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
        public string OperationParameters { get; set; }
        public OpenApiResponses? OperationResponses { get; set; }
    }

    public class ParameterModel
    {
        public string name { get; set; }
        public string description { get; set; }
        public string inputin { get; set; }
        public string schema { get; set; }
        public string required { get; set; }
        
    }
}
