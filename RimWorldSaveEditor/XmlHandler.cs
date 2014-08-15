using RimWorldSaveEditor.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RimWorldSaveEditor
{
    
    class XmlHandler
    {
        List<string> debugList;
        //document needs to be available to all class methods
        string filePath;
        string fileDir;
        XmlDocument saveFile;
        bool backup;


        //Constructor, takes file path to instantiate
        public XmlHandler(String file)
        {
            filePath = file;
            fileDir = Path.GetDirectoryName(file);
            backup = true;
            debugList = new List<string>();
            
        }

        //intialize document, Populate PawnNodeMaps return list of maps
        public NodeMap Populate()
        {
            saveFile = new XmlDocument();
            saveFile.Load(filePath);

            //rebuild from scratch
            NodeMap nodeMap = new NodeMap();

            //Populate Colony nodes
            XmlNode colonyName = saveFile.SelectSingleNode("map/colonyInfo/colonyName");
            if (colonyName.InnerText != null) { nodeMap.colonyName = colonyName; }

            //Populate research nodes
            XmlNodeList researchNames = saveFile.SelectNodes("map/researchManager/progress/keys");
            XmlNodeList researchValues = saveFile.SelectNodes("map/researchManager/progress/values");

            SortedList<string, XmlNode> researchNodes = new SortedList<string, XmlNode>();
            for (int i = 0; i < researchNames.Count; i++)
            {
                string name = researchNames.Item(i).InnerText;
                XmlNode value = researchValues.Item(i);
                researchNodes.Add(name, value);
            }
            nodeMap.researchNodes = researchNodes;

            //Get pawns by finding pawns in colonist faction
            XmlNodeList pawnFactionNodes = saveFile.SelectNodes(Globals.XMLFINDPAWN);
            List<XmlNode> pawns = new List<XmlNode>();
            foreach (XmlNode pawnFactionNode in pawnFactionNodes)
            {
                pawns.Add(pawnFactionNode.ParentNode);
            }

            List<NodeMap.PawnNode> pawnNodeList = new List<NodeMap.PawnNode>();
            //populate colonist nodes
            foreach(XmlNode pawnNode in pawns)
            {
                //new pawn node object
                NodeMap.PawnNode workingPawnNode = new NodeMap.PawnNode();
               
                //get name nodes
                XmlNode storyNode = pawnNode.SelectSingleNode("story");
                workingPawnNode.nameFirst = storyNode.SelectSingleNode("name.first");
                workingPawnNode.nameLast = storyNode.SelectSingleNode("name.last");
                workingPawnNode.nameNick = storyNode.SelectSingleNode("name.nick");
                //Concatenate full name
                workingPawnNode.InitNames();

                //get pawn health node
                workingPawnNode.pawnHealth = pawnNode.SelectSingleNode("healthTracker/pawnHealth");

                //Psychology to come later


                
                XmlNodeList skillNodes = pawnNode.SelectNodes("skills/skills/li");
                SortedList<string, XmlNode> skillNodesList = new SortedList<string, XmlNode>();
                SortedList<String, XmlNode> passionNodesList = new SortedList<string, XmlNode>();

                //loop for skills
                foreach(XmlNode skillNode in skillNodes)
                {
                    string skillName = skillNode.SelectSingleNode("def").InnerText;
                    XmlNode skillLevel = skillNode.SelectSingleNode("level");
                    
                    //Fix for NRE's due to non-existing node for 0 values
                    if (skillLevel == null)
                    {
                        XmlElement nreFix = saveFile.CreateElement("level");
                        nreFix.InnerText = "0";
                        skillNode.AppendChild(nreFix);
                        skillLevel = skillNode.SelectSingleNode("level");
                    }
                    //add Skill to list
                    skillNodesList.Add(skillName, skillLevel);
                    
                    //While we're already at the skill grab the passion node
                    XmlNode passionNode = skillNode.SelectSingleNode("passion");
                    passionNodesList.Add(skillName, passionNode);
                }
                //End Skill loop
                //After loop add lists to pawn
                workingPawnNode.skillNodes = skillNodesList;
                workingPawnNode.passionNodes = passionNodesList;

                //Start Thought Loop
                SortedList<string, XmlNode> thoughtNodeList = new SortedList<string, XmlNode>();
                XmlNodeList topLevelThoughts = pawnNode.SelectNodes("psychology/thoughts/thoughts/li");

                foreach(XmlNode thoughtNode in topLevelThoughts)
                {
                    string name = thoughtNode.SelectSingleNode("def").InnerText + ":" + thoughtNode.SelectSingleNode("age").InnerText;
                    thoughtNodeList.Add(name, thoughtNode);
                }
                //add thoughts to pawn
                workingPawnNode.thoughtNodes = thoughtNodeList;
                //add this pawn to master list as we're done with it
                pawnNodeList.Add(workingPawnNode);
            }
            //Add pawn list to nodeMap
            nodeMap.pawnNodeList = pawnNodeList;
            
            
            
            
            return nodeMap;
        }


        //Modify node, takes node and value
        public void ModifyNode(XmlNode node, string value)
        {
            node.InnerText = value;
        }

        public void MakePassionNode(XmlNode parent, string value)
        {
            XmlElement passionNode = saveFile.CreateElement("passion");
            passionNode.InnerText = value;
            parent.AppendChild(passionNode);
            XmlNode retNode = parent.SelectSingleNode("passion");

            
        }

        public void RemovePassionNode(XmlNode parent)
        {
            XmlNode removeNode = parent.SelectSingleNode("passion");
            parent.RemoveChild(removeNode);
        }


        public void FixSkillNode(XmlNode skillNode)
        {
            XmlElement levelElement = saveFile.CreateElement("level");
            levelElement.InnerText = "0";
            skillNode.AppendChild(levelElement);
        }

        //Toggle backup enable
        public void ToggleBackup()
        {
            backup = !backup;
        }


        //Backup
        public string Backup()
        {
            String backupFile = fileDir + "\\Backup\\" + Path.GetFileNameWithoutExtension(filePath) + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + "-backup.rwm";
            String backupDir = fileDir + "\\Backup\\";
            if(!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }
            File.Copy(filePath, backupFile);
            return backupFile;
            
        }

        //Save changes and backup if enabled.
        public void SaveChanges()
        {
            string backupFile = null;
            if(backup)
            {
                backupFile = Backup();
            }
            saveFile.Save(filePath);

            if(backup)
            {
                MessageBox.Show(String.Format("Save successfully modified, backup of original made at {0}", backupFile));
            }
            else
            {
                MessageBox.Show("Save successfully modified. No backup made per user selection");
            }

            //Do popup box confirming changes saved
        }
    }
}
