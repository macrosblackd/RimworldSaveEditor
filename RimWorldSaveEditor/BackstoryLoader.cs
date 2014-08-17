using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using RimWorldSaveEditor.models;

namespace RimWorldSaveEditor
{
    public class BackstoryLoader
    {
        public static IEnumerable<Backstory> LoadBackstories()
        {
            var xdoc = XDocument.Load("backstories.xml");

            var stories = xdoc.XPathSelectElements("Backstories/Backstory");
                return stories.Select(MapXmlToBackstory);
        }

        private static Backstory MapXmlToBackstory(XElement story)
        {
            var ret = new Backstory();
            ret.Title = (string) story.Element("Title");
            ret.TitleShort = (string) story.Element("TitleShort");
            ret.BaseDesc = (string)story.Element("BaseDesc");
            ret.DefName = (string)story.Element("DefName");
            ret.Slot = (BackstorySlot)Enum.Parse(typeof(BackstorySlot), (string)story.Element("Slot"));
            ret.WorkDisables = story.Elements("WorkDisables").Select(elm => elm.Value).ToList();
            ret.SkillGains = story.Elements("SkillGains").Elements("li").ToDictionary(elm => elm.Element("key").Value, elm => int.Parse(elm.Element("value").Value));
            ret.SpawnCategories = story.Elements("SpawnCategories").Select(elm => elm.Value);

            if (ret.Slot == BackstorySlot.Adulthood)
            {
                ret.BodyTypeGlobal = story.Element("BodyTypeGlobal").Value;
                ret.BodyTypeMale = story.Element("BodyTypeMale").Value;
                ret.BodyTypeFemale = story.Element("BodyTypeFemale").Value;
            }

            return ret;
        }
    }
}