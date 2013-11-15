using Xsd2Code.Library;

namespace CodeDOMStyleGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputFilePath = @"C:\CodeGeneration\ZynxWebAPI.xsd";
            var generatorParams = GetGeneratorParams(inputFilePath);
            generatorParams.TargetFramework = TargetFramework.Net40;
            generatorParams.PropertyParams.AutomaticProperties = true;
            generatorParams.GenerateDataContracts = true;
            generatorParams.Serialization.GenerateXmlAttributes = true;
            generatorParams.OutputFilePath = GetOutputFilePath();
            generatorParams.Serialization.Enabled = false;
            generatorParams.Serialization.GenerateXmlAttributes = false;
            generatorParams.GenerateCloneMethod = false;
            generatorParams.Miscellaneous.DisableDebug = false;
            generatorParams.Miscellaneous.EnableSummaryComment = false;
            generatorParams.Miscellaneous.HidePrivateFieldInIde = false;
            generatorParams.TrackingChanges.Enabled = false;
            generatorParams.EnableDataBinding = false;
            generatorParams.Language = GenerationLanguage.CSharp;
            generatorParams.NameSpace = "Cdss.Domain.Entities";
            generatorParams.EnableInitializeFields = false;
           
            var xsdGen = new GeneratorFacade(generatorParams);

            xsdGen.Generate();

        }

        private static GeneratorParams GetGeneratorParams(string inputFilePath)
        {
            var generatorParams = new GeneratorParams
            {
                InputFilePath = inputFilePath,
                CollectionObjectType = CollectionType.ObservableCollection,
                EnableDataBinding = true,
                GenerateDataContracts = true,
                GenerateCloneMethod = true,
                OutputFilePath = GetOutputFilePath(),
                Miscellaneous = {HidePrivateFieldInIde = true, DisableDebug = true},
                Serialization = {Enabled = true}
            };
            return generatorParams;
        }

        static private string GetOutputFilePath()
        {
            return @"c:/CodeGeneration/CodeDOMStyle";
        }
    }
}
