using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Xml.Schema;
using Microsoft.CSharp;
using Xsd2Code.Library;
using Xsd2Code.Library.Helpers;

namespace FakeTemplateGenerator
{
    class FakeTemplateGenerator
    {
        private readonly CodeDomProvider _providerField = new CSharpCodeProvider();

        public void Generate(string xsdFilePath, string outputDir)
        {
            if (outputDir != null)
                Directory.SetCurrentDirectory(outputDir);

            XmlSchema xsd = null;
            CodeNamespace ns = null;
            Generator.Process(xsdFilePath, ref xsd, ref ns);
            GenerateEntityGroovyCode(ns);
            GenerateEntityBuilderGroovyCode(ns);
        }

        private void GenerateEntityGroovyCode(CodeNamespace ns)
        {
            foreach (CodeTypeDeclaration type in ns.Types)
            {
                string literalCode = Resource.EntityGroovy_cs;
                
                using (var outputStream = new StreamWriter(type.Name + ".groovy", false))
                {
                    literalCode = literalCode.Replace(CodeTemplateSectionName.ClassName, type.Name);
                    string fieldsCode = string.Empty;
                    foreach (CodeTypeMember codeTypeMember in type.Members)
                    {
                        var prop = codeTypeMember as CodeMemberProperty;
                        if (prop != null)
                        {
                            string fieldCode = Resource.EntityFieldGroovy_cs;
                            fieldCode = fieldCode.Replace(CodeTemplateSectionName.FieldType, CSharpTypeToGroovyType.GetType(prop.Type.BaseType));
                            fieldCode = fieldCode.Replace(CodeTemplateSectionName.FieldName, prop.Name);
                            fieldsCode += fieldCode;
                        }
                    }

                    literalCode = literalCode.Replace(CodeTemplateSectionName.Fields, fieldsCode);

                    var cu = new CodeSnippetCompileUnit(literalCode);
                    _providerField.GenerateCodeFromCompileUnit(cu, outputStream, new CodeGeneratorOptions());
                }
            }
        }

        private void GenerateEntityBuilderGroovyCode(CodeNamespace ns)
        {
            foreach (CodeTypeDeclaration type in ns.Types)
            {
                string literalCode = Resource.EntityBuilderGroovy_cs;

                using (var outputStream = new StreamWriter(type.Name + "Builder.groovy", false))
                {
                    literalCode = literalCode.Replace(CodeTemplateSectionName.ClassName, type.Name);
                    string withFunctionCode = string.Empty;
                    foreach (CodeTypeMember codeTypeMember in type.Members)
                    {
                        var prop = codeTypeMember as CodeMemberProperty;
                        if (prop != null)
                        {
                            string fieldCode = Resource.EntityBuilderFunctionGroovy_cs;
                            fieldCode = fieldCode.Replace(CodeTemplateSectionName.FieldType, CSharpTypeToGroovyType.GetType(prop.Type.BaseType));
                            fieldCode = fieldCode.Replace(CodeTemplateSectionName.FieldName, prop.Name);
                            withFunctionCode += fieldCode;
                        }
                    }

                    literalCode = literalCode.Replace(CodeTemplateSectionName.FieldsFunction, withFunctionCode);

                    var cu = new CodeSnippetCompileUnit(literalCode);
                    _providerField.GenerateCodeFromCompileUnit(cu, outputStream, new CodeGeneratorOptions());
                }
            }
        }

    }
}
