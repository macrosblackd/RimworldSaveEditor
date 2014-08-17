using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using RimWorld;
using Verse;

namespace RimWorldDefDumperMod
{
    public class BackstoryDumper
    {
        public void Dump()
        {
            var xDoc = new XDocument();
            var backstories = new XElement("Backstories");
            
            foreach (var pair in BackstoryDatabase.allBackstories)
            {
                var xelm = MapToXElement(pair.Value);
                backstories.Add(xelm);
            }

            xDoc.Add(backstories);
            xDoc.Save("backstories.xml");
        }

        private XElement MapToXElement(Backstory bs)
        {
            var ret = new XElement("Backstory");
            ret.Add(
                new XElement("Title", bs.title),
                new XElement("TitleShort", bs.titleShort),
                new XElement("BaseDesc", bs.baseDesc),
                new XElement("DefName", bs.defName),
                new XElement("Slot", bs.slot.ToString()),
                new XElement("WorkDisables", GetDisabledWork(bs.workDisables)),
                GetSkillGains(bs.skillGainsResolved),
                GetSpawnCategories(bs.spawnCategories));

            if (bs.slot == BackstorySlot.Adulthood)
            {
                ret.Add(
                    new XElement("BodyTypeGlobal", bs.bodyTypeGlobal),
                    new XElement("BodyTypeMale", bs.bodyTypeMale),
                    new XElement("BodyTypeFemale", bs.bodyTypeFemale));
            }

            return ret;
        }

        private IEnumerable<XElement> GetDisabledWork(WorkTags disabledTags)
        {
            return EnumUtils.GetEnabledFlags(disabledTags).Where(tag => tag != WorkTags.None).Select(tag => new XElement("li", EnumUtils.GetName(tag)));
        }

        private XElement GetSkillGains(Dictionary<SkillDef, int> skillGains)
        {
            var ret = new XElement("SkillGains");
            if (skillGains == null)
            {
                ret.Add(string.Empty);
            }
            else
            {
                ret.Add(skillGains.Select(pair => new XElement("li", new XElement("key", pair.Key.skillLabel), new XElement("value", pair.Value))));
            }
            return ret;
        }

        private XElement GetSpawnCategories(IEnumerable<string> spawnCategories)
        {
            var ret = new XElement("SpawnCategories");
            if (spawnCategories == null)
            {
                ret.Add(string.Empty);
            }
            else
            {
                ret.Add(spawnCategories.Select(n => new XElement("li", n)));
            }
            return ret;
        }
    }
}