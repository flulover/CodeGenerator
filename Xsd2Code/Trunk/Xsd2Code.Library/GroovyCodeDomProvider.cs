using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CSharp;

namespace Xsd2Code.Library
{
    class GroovyCodeProvider : CSharpCodeProvider
    {
        public override void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
        {
            foreach (CodeTypeDeclaration codeTypeDeclaration in codeNamespace.Types)
            {
                var classType = new CodeTypeDeclaration(codeTypeDeclaration.Name);
                foreach (CodeTypeMember codeTypeMember in codeTypeDeclaration.Members)
                {
                    var prop = codeTypeMember as CodeMemberProperty;
                    if (prop != null)
                    {
                        var field = new CodeMemberField();
                        field.Name = prop.Name;
                        field.Type = prop.Type;
                        field.Attributes = MemberAttributes.Public;
                        classType.Members.Add(field);
                    }
                }
                options.BlankLinesBetweenMembers = false;
                GenerateCodeFromType(classType, writer, options);
            }
        }

        public override string FileExtension
        {
            get { return ".Groovy"; }
        }
    }
    
}