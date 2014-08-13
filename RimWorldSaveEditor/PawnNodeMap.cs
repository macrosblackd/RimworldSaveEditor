using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RimWorldSaveEditor
{
    class PawnNodeMap
    {
        //stores relevant Pawn nodes
        //XmlNode example;
        public XmlNode parent { get; set; }
        public XmlNode skillArtistic { get; set; }
        public XmlNode skillConstruction { get; set; }
        public XmlNode skillCooking { get; set; }
        public XmlNode skillCrafting { get; set; }
        public XmlNode skillGrowing { get; set; }
        public XmlNode skillMedicine { get; set; }
        public XmlNode skillMelee { get; set; }
        public XmlNode skillMining { get; set; }
        public XmlNode skillResearch { get; set; }
        public XmlNode skillShooting { get; set; }
        public XmlNode skillSocial { get; set; }
        public XmlNode pawnHealth { get; set; }
        public XmlNode nameFirst { get; set; }
        public XmlNode nameNick { get; set; }
        public XmlNode nameLast { get; set; }
        List<XmlNode> allSkillNodes { get; set; }
        public string fullName { get; set; }

        public string getFullName()
        {
            return nameFirst.InnerText + " " + nameNick.InnerText + " " + nameLast.InnerText;   
        }

        public void Init()
        {
            this.fullName = getFullName();
        }
    }
}
