using RimWorldSaveEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RimWorldSaveEditor
{
    public partial class Form1 : Form
    {

        //Need access to XmlHandler and List<PawnNodeMap> globally in class
        XmlHandler handler;
        NodeMap nodeMap;

        //Will need to null check colony name
        public Form1()
        {
            InitializeComponent();
            Version asmVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string version = String.Format("{0}.{1}.{2}", asmVersion.Major, asmVersion.Minor, asmVersion.Build);
            this.Text = String.Format("RimWorld Save Editor Version: {0}", version);
            ToggleControls();
            
        }

        private void backupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(handler != null)
            {
                handler.ToggleBackup();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(handler != null)
            {
                handler.SaveChanges();
            }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select Save File to Open";
            fileDialog.Filter = "RIM files|*.rim";
            fileDialog.InitialDirectory = Settings.Default.lastUsedPath;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.lastUsedPath = Path.GetDirectoryName(fileDialog.FileName);
                Settings.Default.Save();

                handler = new XmlHandler(fileDialog.FileName);
                if (backupCheck.Checked == false)
                {
                    handler.ToggleBackup();
                }
                nodeMap = handler.Populate();
                colonistListBox.DataSource = nodeMap.pawnNodeList;
                colonistListBox.DisplayMember = "fullName";
                ToggleControls();
            }
        }

        private void colonistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(handler == null)
            {
                return;
            }
            ListBox box = (ListBox)sender;
            int index = box.SelectedIndex;
            SortedList<string, XmlNode> skillNodes = nodeMap.pawnNodeList[index].skillNodes;

            artisticBox.Text = skillNodes["Artistic"].InnerText;

            constructionBox.Text = skillNodes["Construction"].InnerText;

            cookingBox.Text = skillNodes["Cooking"].InnerText; 

            craftingBox.Text = skillNodes["Crafting"].InnerText;

            growingBox.Text = skillNodes["Growing"].InnerText;

            medicineBox.Text = skillNodes["Medicine"].InnerText;

            meleeBox.Text = skillNodes["Melee"].InnerText;

            miningBox.Text = skillNodes["Mining"].InnerText;

            researchBox.Text = skillNodes["Research"].InnerText;

            shootingBox.Text = skillNodes["Shooting"].InnerText;

            socialBox.Text = skillNodes["Social"].InnerText;

            healthBox.Text = nodeMap.pawnNodeList[index].pawnHealth.InnerText;
        }

        private void artisticBox_TextChanged(object sender, EventArgs e)
        {   
            handler.ModifyNode(GetSelectedPawn().skillNodes["Artistic"], artisticBox.Text);
        }

        private void constructionBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Construction"], constructionBox.Text);
        }

        private void cookingBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Cooking"], cookingBox.Text);
        }

        private void craftingBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Crafting"], craftingBox.Text);
        }

        private void growingBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Growing"], growingBox.Text);
        }

        private void medicineBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Medicine"], medicineBox.Text);
        }

        private void meleeBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Melee"], meleeBox.Text);
        }

        private void miningBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Mining"], miningBox.Text);
        }

        private void researchBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Research"], researchBox.Text);
        }

        private void shootingBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Shooting"], shootingBox.Text);
        }

        private void socialBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().skillNodes["Social"], socialBox.Text);
        }

        private void healthBox_TextChanged(object sender, EventArgs e)
        {
            handler.ModifyNode(GetSelectedPawn().pawnHealth, healthBox.Text);
        }

        private NodeMap.PawnNode GetSelectedPawn()
        {
            return nodeMap.pawnNodeList[colonistListBox.SelectedIndex];
        }



        private void ToggleControls()
        {
            backupCheck.Enabled = !backupCheck.Enabled;
            saveButton.Enabled = !saveButton.Enabled;
            socialBox.Enabled = !socialBox.Enabled;
            shootingBox.Enabled = !shootingBox.Enabled;
            researchBox.Enabled = !researchBox.Enabled;
            miningBox.Enabled = !miningBox.Enabled;
            meleeBox.Enabled = !meleeBox.Enabled;
            medicineBox.Enabled = !medicineBox.Enabled;
            growingBox.Enabled = !growingBox.Enabled;
            craftingBox.Enabled = !craftingBox.Enabled;
            cookingBox.Enabled = !cookingBox.Enabled;
            constructionBox.Enabled = !constructionBox.Enabled;
            artisticBox.Enabled = !artisticBox.Enabled;
            healthBox.Enabled = !healthBox.Enabled;
        }

    }
}
