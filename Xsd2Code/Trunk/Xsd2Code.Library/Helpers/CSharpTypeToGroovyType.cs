using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xsd2Code.Library.Helpers
{
    class CSharpTypeToGroovyType
    {
        // key:     C# type 
        // value:   Groovy type
        private static readonly Dictionary<string, string> dict = new Dictionary<string, string>()
        {
            {"System.Int32","int"},
            {"System.String","String"},
            {"System.Boolean","Boolean"},
        };

        public static string GetType(string dotNetType)
        {
            var groovyType = dict[dotNetType];
            return groovyType == null ? dotNetType : groovyType;
        }
    }
}
