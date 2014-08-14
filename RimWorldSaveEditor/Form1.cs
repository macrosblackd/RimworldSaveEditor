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

        SortedList<string, TextBox> textBoxes;
        Dictionary<TextBox, string> textBoxesReverse;

        SortedList<string, ComboBox> comboBoxes;
        Dictionary<ComboBox, string> comboBoxesReverse;
        bool toggleOnce;

        //Will need to null check colony name
        public Form1()
        {
            InitializeComponent();
            Version asmVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string version = String.Format("{0}.{1}.{2}", asmVersion.Major, asmVersion.Minor, asmVersion.Build);
            this.Text = String.Format("RimWorld Save Editor Version: {0}", version);
            PopulateControlLists();
            AssignEventHandler(this);
            ToggleControls();

        }

        public void AssignEventHandler(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox textBox = (TextBox)ctrl;
                    if ((string)textBox.Tag != "health") { textBox.TextChanged += new EventHandler(TextBoxChanged); }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)ctrl;
                    comboBox.SelectedIndexChanged += new EventHandler(ComboBoxChanged);
                }
                else { AssignEventHandler(ctrl); }
            }
        }

        private void backupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler.ToggleBackup();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler.SaveChanges();
            }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select Save File to Open";
            fileDialog.Filter = "RWM files|*.rwm";
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
                if (!toggleOnce)
                {
                    ToggleControls();
                    toggleOnce = true;
                }
            }
        }


        private void TextBoxChanged(object sender, EventArgs args)
        {
            TextBox changedBox = (TextBox)sender;
            XmlNode modNode = GetSelectedPawn().skillNodes[textBoxesReverse[changedBox]];
            handler.ModifyNode(modNode, changedBox.Text);
        }

        private void ComboBoxChanged(object sender, EventArgs args)
        {
            ComboBox changedBox = (ComboBox)sender;
            string value = null;
            switch (changedBox.SelectedIndex)
            {
                case 0:
                    value = "None";
                    break;
                case 1:
                    value = "Major";
                    break;
                case 2:
                    value = "Minor";
                    break;
            }

            if (value == "None")
            {
                handler.RemovePassionNode(GetSelectedPawn().skillNodes[comboBoxesReverse[changedBox]].ParentNode);
            }

            XmlNode pNode = GetSelectedPawn().passionNodes[comboBoxesReverse[changedBox]];
            if (pNode != null && pNode.InnerText != null)
            {
                handler.ModifyNode(pNode, value);
            }
            else
            {
                XmlNode passNode = GetSelectedPawn().skillNodes[comboBoxesReverse[changedBox]].ParentNode;
                //GetSelectedPawn().passionNodes[comboBoxesReverse[changedBox]] = 
                handler.MakePassionNode(passNode, value);
            }
        }


        private void colonistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }
            ListBox box = (ListBox)sender;
            int index = box.SelectedIndex;

            //populate skill levels and health
            SortedList<string, XmlNode> skillNodes = nodeMap.pawnNodeList[index].skillNodes;
            foreach (string key in textBoxes.Keys)
            {
                textBoxes[key].Text = skillNodes[key].InnerText;
            }
            healthBox.Text = nodeMap.pawnNodeList[index].pawnHealth.InnerText;

            //populate passions
            SortedList<string, XmlNode> passionNodes = nodeMap.pawnNodeList[index].passionNodes;
            foreach (string key in comboBoxes.Keys)
            {
                if (passionNodes[key] == null || passionNodes[key].InnerText == null)
                {
                    if (comboBoxes[key].SelectedText != null)
                    {
                        comboBoxes[key].ResetText();
                    }
                    comboBoxes[key].SelectedText = "None";
                }
                else
                {
                    if (comboBoxes[key].SelectedText != null)
                    {
                        comboBoxes[key].ResetText();
                    }
                    comboBoxes[key].SelectedText = passionNodes[key].InnerText;
                }
            }
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


        private void PopulateControlLists()
        {
            textBoxes = new SortedList<string, TextBox>();
            textBoxes.Add("Artistic", artisticBox);
            textBoxes.Add("Construction", constructionBox);
            textBoxes.Add("Cooking", cookingBox);
            textBoxes.Add("Crafting", craftingBox);
            textBoxes.Add("Growing", growingBox);
            textBoxes.Add("Medicine", medicineBox);
            textBoxes.Add("Melee", meleeBox);
            textBoxes.Add("Mining", miningBox);
            textBoxes.Add("Research", researchBox);
            textBoxes.Add("Shooting", shootingBox);
            textBoxes.Add("Social", socialBox);
            textBoxesReverse = textBoxes.ToDictionary(x => x.Value, x => x.Key);


            comboBoxes = new SortedList<string, ComboBox>();
            comboBoxes.Add("Artistic", artisticPassion);
            comboBoxes.Add("Construction", constructionPassion);
            comboBoxes.Add("Cooking", cookingPassion);
            comboBoxes.Add("Crafting", craftingPassion);
            comboBoxes.Add("Growing", growingPassion);
            comboBoxes.Add("Medicine", medicinePassion);
            comboBoxes.Add("Melee", meleePassion);
            comboBoxes.Add("Mining", miningPassion);
            comboBoxes.Add("Research", researchPassion);
            comboBoxes.Add("Shooting", shootingPassion);
            comboBoxes.Add("Social", socialPassion);
            comboBoxesReverse = comboBoxes.ToDictionary(x => x.Value, x => x.Key);
        }
    }
}
