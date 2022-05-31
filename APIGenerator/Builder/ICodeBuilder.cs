using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGenerator.Builder
{
    public interface ICodeBuilder
    {
        Task InitiliazeYaml();
        void GenerateFolders();
        void GenerateModels();
        void GenerateControllers();
        void GenerateStartUp();
        void GenerateAppSettings();
        void GenerateAttributes();

        void Generate();
    }
}
