using RimWorldSaveEditor.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RimWorldSaveEditor
{
    class DefDumper
    {
        static List<string> thoughtDefFiles = new List<string>()
        {
            "Thoughts_Broken.xml",
            "Thoughts_ConditionsGeneral.xml",
            "Thoughts_ConditionsSpecial.xml",
            "Thoughts_Memories.xml",
            "Thoughts_PsychicDrone.xml",
            "Thoughts_Traits.xml"
        };

        //defname, label
        public static SortedList<string, string> defList;
        public static Dictionary<string, string> defListReverse;
        public static Dictionary<string, string> moodlist;
        public static Dictionary<string, string> backstoriesAdult;
        public static Dictionary<string, string> backstoriesAdultReverse;
        public static Dictionary<string, string> backstoriesChild;
        public static Dictionary<string, string> backstoriesChildReverse;

        

        public static void DumpThoughts()
        {
            defList = new SortedList<string, string>();
            moodlist = new Dictionary<string, string>();
            foreach(string file in thoughtDefFiles)
            {
                XmlDocument defFile = new XmlDocument();
                defFile.Load(Settings.Default.rimworldDir + "/Mods/Core/Defs/ThoughtDefs/" + file);

           
                XmlNodeList thoughtList = defFile.SelectNodes("node()/ThoughtDef");
                foreach(XmlNode thoughtNode in thoughtList)
                {
                    XmlNode nameNode = thoughtNode.SelectSingleNode("defName");
                    XmlNode labelNode = thoughtNode.SelectSingleNode("label");
                    XmlNode moodNode = thoughtNode.SelectSingleNode("baseMoodEffect");
                    string label = labelNode.InnerText;
                    if (defList.Keys.Contains(label))
                    {
                        label = label + ":" + defList.Keys.Count();
                    }

                    defList.Add(label, nameNode.InnerText);
                    if (moodNode != null)
                    {
                        moodlist.Add(nameNode.InnerText, moodNode.InnerText);
                    }

                }

            }
            defListReverse = defList.ToDictionary(x => x.Value, x => x.Key);
        }

        public static void DumpBackstories()
        {
            backstoriesAdult = new Dictionary<string,string>();
            backstoriesAdultReverse = new Dictionary<string, string>();
            backstoriesChild = new Dictionary<string, string>();
            backstoriesChildReverse = new Dictionary<string, string>();
            Dictionary<string, int> dupeCounterAdult = new Dictionary<string, int>();
            Dictionary<string, int> dupeCounterChild = new Dictionary<string, int>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("backstory.xml");
            XmlNodeList stories = xDoc.SelectNodes("root/backstory");
            foreach(XmlNode node in stories)
            {
                if (node.SelectSingleNode("slot").InnerText == "Adulthood")
                {
                    string sTitle = node.SelectSingleNode("title").InnerText;
                    if (!dupeCounterAdult.ContainsKey(node.SelectSingleNode("title").InnerText))
                    {
                        dupeCounterAdult.Add(node.SelectSingleNode("title").InnerText, 1);
                    }
                    else
                    {
                        sTitle = sTitle + ":" + dupeCounterAdult[node.SelectSingleNode("title").InnerText];
                        dupeCounterAdult[node.SelectSingleNode("title").InnerText]++;
                    }
                    backstoriesAdult.Add(sTitle, node.SelectSingleNode("def").InnerText);
                }


                else if (node.SelectSingleNode("slot").InnerText == "Childhood")
                {
                    string sTitle = node.SelectSingleNode("title").InnerText;
                    if (!dupeCounterChild.ContainsKey(node.SelectSingleNode("title").InnerText))
                    {
                        dupeCounterChild.Add(node.SelectSingleNode("title").InnerText, 1);
                    }
                    else
                    {
                        sTitle = sTitle + ":" + dupeCounterChild[node.SelectSingleNode("title").InnerText];
                        dupeCounterChild[node.SelectSingleNode("title").InnerText]++;
                    }
                    backstoriesChild.Add(sTitle, node.SelectSingleNode("def").InnerText);
                }
            }
            backstoriesAdultReverse = backstoriesAdult.ToDictionary(x => x.Value, x => x.Key);
            backstoriesChildReverse = backstoriesChild.ToDictionary(x => x.Value, x => x.Key);
        }
    }

}
