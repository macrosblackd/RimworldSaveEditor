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

namespace RimWorldSaveEditor
{
    public partial class Form1 : Form
    {

        //Need access to XmlHandler and List<PawnNodeMap> globally in class
        XmlHandler handler;
        List<PawnNodeMap> nodeMapList;


        /**TODO
         * 
         * Build Gui
         * Handle Events
         * Figure out how to modify xml (more)easily
         * 
         **/




        public Form1()
        {
            InitializeComponent();
            Version asmVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string version = String.Format("{0}.{1}.{2}", asmVersion.Major, asmVersion.Minor, asmVersion.Build);
            this.Text = String.Format("RimWorld Save Editor Version: {0}", version);
            
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
                nodeMapList = handler.Populate();
                colonistListBox.DataSource = nodeMapList;
                colonistListBox.DisplayMember = "fullName";
                debugBox.DataSource = nodeMapList;
                debugBox.DisplayMember = "fullName";
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
            PawnNodeMap nodeMap = nodeMapList[index];

            artisticBox.Text = nodeMap.skillArtistic.InnerText;
            constructionBox.Text = nodeMap.skillConstruction.InnerText;
            cookingBox.Text = nodeMap.skillCooking.InnerText;
            craftingBox.Text = nodeMap.skillCrafting.InnerText;
            growingBox.Text = nodeMap.skillGrowing.InnerText;
            medicineBox.Text = nodeMap.skillMedicine.InnerText;
            meleeBox.Text = nodeMap.skillMelee.InnerText;
            miningBox.Text = nodeMap.skillMining.InnerText;
            researchBox.Text = nodeMap.skillResearch.InnerText;
            shootingBox.Text = nodeMap.skillShooting.InnerText;
            socialBox.Text = nodeMap.skillSocial.InnerText;
            healthBox.Text = nodeMap.pawnHealth.InnerText;
        }

        private void artisticBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillArtistic, artisticBox.Text);
        }

        private void constructionBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillConstruction, constructionBox.Text);
        }

        private void cookingBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillCooking, cookingBox.Text);
        }

        private void craftingBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillCrafting, craftingBox.Text);
        }

        private void growingBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillGrowing, growingBox.Text);
        }

        private void medicineBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillMedicine, medicineBox.Text);
        }

        private void meleeBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillMelee, meleeBox.Text);
        }

        private void miningBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillMining, miningBox.Text);
        }

        private void researchBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillResearch, researchBox.Text);
        }

        private void shootingBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillShooting, shootingBox.Text);
        }

        private void socialBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.skillSocial, socialBox.Text);
        }

        private void healthBox_TextChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            PawnNodeMap nodeMap = nodeMapList[colonistListBox.SelectedIndex];
            handler.ModifyNode(nodeMap.pawnHealth, healthBox.Text);
        }
    }
}
