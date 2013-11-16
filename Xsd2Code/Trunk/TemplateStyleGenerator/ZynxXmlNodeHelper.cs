using System.Collections.Generic;
using System.Xml;

namespace TemplateStyleGenerator
{
    public class ZynxXmlNodeHelper
    {
        class VaribleUseInfo
        {
            public uint DeclarationTimes;
            public uint UseageTimes;
        }

        private readonly Dictionary<string, VaribleUseInfo> _varibleNameDictionary = new Dictionary<string, VaribleUseInfo>();

        private  List<XmlNode> GetWideFirstNodeListOrder(XmlNode node)
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

        private  List<XmlNode> GetNextLevelNode(List<XmlNode> curLevelNodes)
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

        private  string ToCamelStyle(string nodeName)
        {
            return char.ToLower(nodeName[0]) + nodeName.Substring(1);
        }

        public  string GetDeclarationVaribleName(string nodeName)
        {
            var tmpVaribleName = ToCamelStyle(nodeName);
            if (_varibleNameDictionary.ContainsKey(tmpVaribleName))
            {
                _varibleNameDictionary[tmpVaribleName].DeclarationTimes++;
                return tmpVaribleName + _varibleNameDictionary[tmpVaribleName].DeclarationTimes;
            }

            return tmpVaribleName;
        }

        public string GetUsageVaribleName(string nodeName)
        {
            var tmpVaribleName = ToCamelStyle(nodeName);
            if (_varibleNameDictionary.ContainsKey(tmpVaribleName))
            {
                _varibleNameDictionary[tmpVaribleName].UseageTimes++;
                return tmpVaribleName + _varibleNameDictionary[tmpVaribleName].UseageTimes;
            }

            return tmpVaribleName;
        }

        private  void RegisterVaribleName(List<XmlNode> nodeList)
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
                    _varibleNameDictionary.Add(keyValuePair.Key, new VaribleUseInfo());
            }
        }

        public string GetActualNodeName(XmlNode node)
        {
            var att = node.Attributes["i:type"];
            if (att == null)
                return node.Name;
            else
                return node.Attributes["i:type"].Value;
        }

        public  List<XmlNode> GetXmlNodeList(XmlNode rootNode)
        {
            // tree to list(wide first)
            var nodeList = GetWideFirstNodeListOrder(rootNode);

            nodeList.Reverse();

            // varible name duplication
            RegisterVaribleName(nodeList);

            return nodeList;
        }
        
        public static bool ShouldGeneratorVarible(XmlNode node)
        {
            if (!node.HasChildNodes)
                return false;

            if (node.ChildNodes.Count == 1 && node.FirstChild is XmlText)
                return false;

            return true;
        }
 
    }
}
