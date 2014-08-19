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
        public static Dictionary<string, Trait> traitList;

        
        //Masterlist = label, defName
        //MoodList = defName, value
        public static void DumpThoughts()
        {
            defList = new SortedList<string, string>();
            moodlist = new Dictionary<string, string>();
            Dictionary<string, int> dupeLabels = new Dictionary<string, int>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Resources.thoughts);

            XmlNodeList tList = xDoc.SelectNodes("thoughts/thought");
            foreach(XmlNode thought in tList)
            {
                string label = thought.SelectSingleNode("label").InnerText;
                if(defList.ContainsKey(label))
                {
                    if(dupeLabels.ContainsKey(label))
                    {
                        label = label + ":" + dupeLabels[thought.SelectSingleNode("label").InnerText].ToString();
                        dupeLabels[thought.SelectSingleNode("label").InnerText]++;
                    }
                    else
                    {
                        dupeLabels.Add(label, 1);
                        label = label + ":" + "1";
                    }
                }
                defList.Add(label, thought.SelectSingleNode("defName").InnerText);
                if (thought.SelectSingleNode("moodEffect") != null)
                {
                    moodlist.Add(thought.SelectSingleNode("defName").InnerText, thought.SelectSingleNode("moodEffect").InnerText);
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
            xDoc.LoadXml(Resources.backstory);
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


        //groundwork for listing available traits to add to a colonist
        public static void DumpTraits()
        {
            traitList = new Dictionary<string, Trait>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Resources.traits);
            XmlNodeList tNList = xDoc.SelectNodes("TraitDefs/def");

            foreach(XmlNode tNode in tNList)
            {
                Trait trait = new Trait();
                trait.defName = tNode.SelectSingleNode("defName").InnerText;
                

                if (tNode.SelectSingleNode("degree") == null)
                {
                    trait.degree = "none";
                }
                else
                {
                    trait.degree = tNode.SelectSingleNode("degree").InnerText;
                }

                traitList.Add(tNode.SelectSingleNode("label").InnerText, trait);
            }

        }

        public static string GetLabelFromTraitDef(string defName, string degree)
        {
            foreach(string label in traitList.Keys)
            {
                if (degree == null)
                {
                    degree = "none";
                }

                if (traitList[label].defName == defName && traitList[label].degree == degree)
                {
                     return label;
                }

            }
            return null;
        }
    }
}
