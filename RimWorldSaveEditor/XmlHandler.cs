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
        public List<PawnNodeMap> Populate()
        {
            saveFile = new XmlDocument();
            saveFile.Load(filePath);

            XmlNodeList pawnFactionNodes = saveFile.SelectNodes(Globals.XMLFINDPAWN);
            List<PawnNodeMap> nodeMapList = new List<PawnNodeMap>();

            foreach(XmlNode pawnFactionNode in pawnFactionNodes)
            {
                XmlNode pawnNode = pawnFactionNode.ParentNode;
                XmlNode nameNodes = pawnNode.SelectSingleNode("story");
                XmlNodeList skillNodes = pawnNode.SelectNodes(Globals.XMLSKILLNODE);
                PawnNodeMap nodeMap = new PawnNodeMap()
                {
                    parent = pawnNode,
                    nameFirst = nameNodes.SelectSingleNode("name.first"),
                    nameNick = nameNodes.SelectSingleNode("name.nick"),
                    nameLast = nameNodes.SelectSingleNode("name.last"),
                    pawnHealth = pawnNode.SelectSingleNode("healthTracker/pawnHealth"),
                };

                /**
                 * Get Skills for map
                 **/
                foreach (XmlNode skillNode in skillNodes)
                {
                    string def = skillNode.SelectSingleNode("def").InnerText;
                    switch(def)
                    {
                        case "Artistic":
                            nodeMap.skillArtistic = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillArtistic == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillArtistic = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Construction":
                            nodeMap.skillConstruction = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillConstruction == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillConstruction = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Cooking":
                            nodeMap.skillCooking = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillCooking == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillCooking = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Crafting":
                            nodeMap.skillCrafting = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillCrafting == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillCrafting = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Growing":
                            nodeMap.skillGrowing = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillGrowing == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillGrowing = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Medicine":
                            nodeMap.skillMedicine = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillMedicine == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillMedicine = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Melee":
                            nodeMap.skillMelee = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillMelee == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillMelee = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Mining":
                            nodeMap.skillMining = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillMining == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillMining = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Research":
                            nodeMap.skillResearch = skillNode.SelectSingleNode("level");
                            continue;
                        case "Shooting":
                            nodeMap.skillShooting = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillShooting == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillShooting = skillNode.SelectSingleNode("level");
                            }
                            continue;
                        case "Social":
                            nodeMap.skillSocial = skillNode.SelectSingleNode("level");
                            if (nodeMap.skillSocial == null)
                            {
                                FixSkillNode(skillNode);
                                nodeMap.skillSocial = skillNode.SelectSingleNode("level");
                            }
                            continue;
                    }
                } //End inner foreach loop
                nodeMap.Init();
                nodeMapList.Add(nodeMap);
            }//End outer foreach loop
            return nodeMapList;
        }


        //Modify node, takes node and value
        public void ModifyNode(XmlNode node, string value)
        {
            node.InnerText = value;
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
            String backupFile = fileDir + "\\Backup\\" + Path.GetFileNameWithoutExtension(filePath) + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + "-backup.rim";
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
