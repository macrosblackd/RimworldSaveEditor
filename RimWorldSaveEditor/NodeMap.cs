using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RimWorldSaveEditor
{
    class NodeMap
    {
        //Stores references to nodes

        //Pawn Specific Nodes
        public List<PawnNode> pawnNodeList { get; set; }

        //Research Nodes
        public SortedList<string, XmlNode> researchNodes { get; set; }


        public XmlNode colonyName { get; set; }






        public class PawnNode
        {

            //Skill list and passion list key = skill def name
            public SortedList<string, XmlNode> skillNodes { get; set; }
            public SortedList<string, XmlNode> passionNodes { get; set; }
            //Pawn's <thing Class = "Pawn"> node;
            public XmlNode pawn { get; set; }
            //Name Nodes
            public XmlNode nameFirst { get; set; }
            public XmlNode nameNick { get; set; }
            public XmlNode nameLast { get; set; }
            public string fullName { get; set; }
            //Health Node
            public XmlNode pawnHealth { get; set; }
            public SortedList<string, XmlNode> thoughtNodes { get; set; }

            public void InitNames()
            {
                this.fullName = nameFirst.InnerText + " " + nameNick.InnerText + " " + nameLast.InnerText;
            }
        }
    }
}
