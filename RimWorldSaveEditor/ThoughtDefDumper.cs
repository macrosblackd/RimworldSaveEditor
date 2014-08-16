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
    class ThoughtDefDumper
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
        public SortedList<string, string> defList;
        public Dictionary<string, string> defListReverse;
        //defname, mood
        public Dictionary<string, string> moodlist;

        

        public void Dump()
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

        public string DictLookupReverse(string label)
        {
            return defList.FirstOrDefault(x => x.Value == label).Key;
        }
    }

}
