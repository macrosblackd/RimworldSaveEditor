using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimWorldSaveEditor
{
    static class Globals
    {
        public const string XMLFINDPAWN = "//map/things/thing[@Class = 'Pawn']/kindDef[text() = 'Colonist']";
        public const string XMLSKILLNODE = "skills/skills/li";
    }
}
