using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TestCodeGenerator
{
    class Program
    {
        class TagName
        {
            public static readonly string NodeName = "[NodeName]";
            public static readonly string WithFunctions = "[WithFunction]";
        }

        private static readonly string XmlBuilderCodeFormat = "new {0}Builder()\n" + "{1}";
        private static readonly string XmlBuilderFuctionCodeFormat = "\t.with{0}({1})\n";

        private static readonly string XmlBuildNodeVaribleFormat = "def {0} = {1};\n\n";

        class VaribleUseInfo
        {
            public uint declarationTimes;
            public uint useageTimes;
        }

        private static Dictionary<string, VaribleUseInfo> s_VaribleNameDictionary = new Dictionary<string, VaribleUseInfo>(); 

        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("OrderSet.xml");

            string result = GeneratorCode(doc.FirstChild);

            using (var outputStream = new StreamWriter("BuilderCode.groovy", false))
            {
                outputStream.Write(result);
            }

            Console.WriteLine(result);
        }

        private static List<XmlNode> GetWideFirstNodeListOrder(XmlNode node)
        {
            var nodeList = new List<XmlNode>();
            nodeList.Add(node);

            List<XmlNode> curLevelNodes = new List<XmlNode>();
            curLevelNodes.Add(node);

            List<XmlNode> nextLevelNode = null;
            while ((nextLevelNode = GetNextLevelNode(curLevelNodes)).Count > 0)
            {
                nodeList.AddRange(nextLevelNode);

                curLevelNodes.Clear();
                curLevelNodes.AddRange(nextLevelNode);
            }

            return nodeList;
        }

        private static List<XmlNode> GetNextLevelNode(List<XmlNode> curLevelNodes)
        {
            List<XmlNode> nextLevelNode = new List<XmlNode>();
            foreach (XmlNode curLevelNode in curLevelNodes)
            {
                foreach (XmlNode childNode in curLevelNode.ChildNodes)
                {
                    nextLevelNode.Add(childNode);
                }
            }

            return nextLevelNode;
        }

        private static string ToCamelStyle(string nodeName)
        {
            return char.ToLower(nodeName[0]) + nodeName.Substring(1);
        }

        private static string GetDeclarationVaribleName(string nodeName)
        {
            var tmpVaribleName = ToCamelStyle(nodeName);
            if (s_VaribleNameDictionary.ContainsKey(tmpVaribleName))
            {
                s_VaribleNameDictionary[tmpVaribleName].declarationTimes++;
                return tmpVaribleName + s_VaribleNameDictionary[tmpVaribleName].declarationTimes;
            }

            return tmpVaribleName;
        }

        private static string GetUseageVaribleName(string nodeName)
        {
            var tmpVaribleName = ToCamelStyle(nodeName);
            if (s_VaribleNameDictionary.ContainsKey(tmpVaribleName))
            {
                s_VaribleNameDictionary[tmpVaribleName].useageTimes++;
                return tmpVaribleName + s_VaribleNameDictionary[tmpVaribleName].useageTimes;
            }

            return tmpVaribleName;
        }

        private static void RegisterVaribleName(List<XmlNode> nodeList)
        {
            // record all the varible name show times
            Dictionary<string, uint> tmpVaribleNameDictionary = new Dictionary<string, uint>();
            foreach (XmlNode xmlNode in nodeList)
            {
                if (ShouldGeneratorVarible(xmlNode))
                {
                    var varibleName = ToCamelStyle(xmlNode.Name);
                    if (tmpVaribleNameDictionary.ContainsKey(varibleName))
                    {
                        tmpVaribleNameDictionary[varibleName]++;
                    }
                    else
                    {
                        tmpVaribleNameDictionary[varibleName] = 1;
                    }
                }
            }

            // record duplicate varible and set the used time
            foreach (KeyValuePair<string, uint> keyValuePair in tmpVaribleNameDictionary)
            {
                if (keyValuePair.Value > 1)
                    s_VaribleNameDictionary.Add(keyValuePair.Key, new VaribleUseInfo());
            }
        }

        private static string GetActualNodeName(XmlNode node)
        {
            var att = node.Attributes["i:type"];
            if (att == null)
                return node.Name;
            else
                return node.Attributes["i:type"].Value;
        }

        private static string GeneratorCode(XmlNode rootNode)
        {
            // tree to list(wide first)
            var nodeList = GetWideFirstNodeListOrder(rootNode);

            nodeList.Reverse();

            // varible name duplication
            RegisterVaribleName(nodeList);

            string code = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                if (ShouldGeneratorVarible(node))
                {
                    string withFunctionCode = string.Empty;
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (ShouldGeneratorVarible(childNode))
                        {
                            withFunctionCode += string.Format(
                            XmlBuilderFuctionCodeFormat,
                            childNode.Name,
                            GetUseageVaribleName(GetActualNodeName(childNode)));
                        }
                        else
                        {
                            withFunctionCode += string.Format(
                            XmlBuilderFuctionCodeFormat,
                            childNode.Name,
                            string.Format("\"{0}\"" ,childNode.FirstChild.Value));
                        }
                    }

                    // delete last \n
                    withFunctionCode = withFunctionCode.Substring(0, withFunctionCode.Length - 1);

                    string builderCode = string.Format(XmlBuilderCodeFormat, GetActualNodeName(node), withFunctionCode);
                    string nodeCode = string.Format(XmlBuildNodeVaribleFormat, GetDeclarationVaribleName(GetActualNodeName(node)), builderCode);

                    code += nodeCode;
                }
            }

            return code;
        }

        private static bool ShouldGeneratorVarible(XmlNode node)
        {
            if (!node.HasChildNodes)
                return false;

            if (node.ChildNodes.Count == 1 && node.FirstChild is XmlText)
                return false;

            return true;    
        }
    }
}
