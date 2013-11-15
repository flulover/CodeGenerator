using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTemplateGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var fakeTemplateGenerator = new FakeTemplateGenerator();
            fakeTemplateGenerator.Generate(@"C:\CodeGeneration\ZynxWebAPI.xsd", @"C:\CodeGeneration\FakeTemplateStyle");
        }
    }
}
