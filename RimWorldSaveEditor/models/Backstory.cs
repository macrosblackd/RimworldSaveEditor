using System.Collections;
using System.Collections.Generic;

namespace RimWorldSaveEditor.models
{
    public class Backstory
    {
        public string Title { get; set; }
        public string TitleShort { get; set; }
        public string BaseDesc { get; set; }
        public string DefName { get; set; }
        public BackstorySlot Slot { get; set; }
        public IEnumerable<string> WorkDisables { get; set; }
        public IDictionary<string, int> SkillGains { get; set; }
        public IEnumerable<string> SpawnCategories { get; set; }
        public string BodyTypeGlobal { get; set; }
        public string BodyTypeMale { get; set; }
        public string BodyTypeFemale { get; set; }

        public override int GetHashCode()
        {
            return DefName.GetHashCode();
        }
    }
}

