using RimWorldSaveEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace RimWorldSaveEditor
{
    public partial class Form1 : Form
    {
        string releaseThreadUrl = "http://ludeon.com/forums/index.php?topic=5346.0";
        string versionCheckUrl = "http://pastebin.com/raw.php?i=3LvpsTWB";
        string currentVersion;

        XmlHandler handler;
        NodeMap nodeMap;
        //Lists and reversed lists of SkillName-Control Mappings
        SortedList<string, TextBox> textBoxes;
        Dictionary<TextBox, string> textBoxesReverse;
        SortedList<string, ComboBox> comboBoxes;
        Dictionary<ComboBox, string> comboBoxesReverse;
        ThoughtDefDumper tDefDumper;

        bool toggleOnce,updateChecked;

        //Will need to null check colony name
        public Form1()
        {
            InitializeComponent();
            Version asmVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string version = String.Format("{0}.{1}.{2}.{3}", asmVersion.Major, asmVersion.Minor, asmVersion.Build, asmVersion.Revision);
            currentVersion = String.Format(version + ".{0}", asmVersion.Revision);
            this.Text = String.Format("RimWorld Save Editor Version: {0}", version);
            PopulateControlLists();
            AssignEventHandler(this);
            ToggleControls();
            if (Settings.Default.updateCheckEnabled)
            {
                Thread updateThread = new Thread(CheckUpdate);
                updateThread.Start();
            }
        }


        //Update check methods
        public void CheckUpdate()
        {
            using (var WebClient = new WebClient())
            {
                string updateVersion = WebClient.DownloadString(versionCheckUrl);
                updateChecked = true;
                
                if (CompareVersions(updateVersion) == 1)
                {
                    if (MessageBox.Show("New version available, Goto release thread?", "Open Thread?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(releaseThreadUrl);
                        Application.Exit();
                        Environment.Exit(0);
                    }
                    else { return; }
                }
                else { return; }
            }
        }

        public int CompareVersions(string stringFromNet)
        {
            string[] asmStyleParse = stringFromNet.Split('.');
            string[] currentVerParse = currentVersion.Split('.');

            for(int i = 0; i < asmStyleParse.Count(); i++)
            {
                if (Convert.ToInt32(asmStyleParse[i]) > Convert.ToInt32(currentVerParse[i])) 
                {
                    return 1;
                }
            }
            return 0;
        }

        //Assign textboxes and comboboxes to a single event.
        public void AssignEventHandler(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox textBox = (TextBox)ctrl;
                    if (textBox.Tag == null) { textBox.TextChanged += new EventHandler(TextBoxChanged); }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)ctrl;
                    if (comboBox.Tag == null) { comboBox.SelectedIndexChanged += new EventHandler(ComboBoxChanged); }
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
            if(!Settings.Default.rimworldDirSet)
            {
                FolderBrowserDialog folderSelector = new FolderBrowserDialog();
                folderSelector.Description = "Please find the path to your rimworld directory. This is important for thoughtDefs and traitDefs to function.";
                if(folderSelector.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.rimworldDir = folderSelector.SelectedPath;
                    Settings.Default.rimworldDirSet = true;
                    Settings.Default.Save();
                }
            }
            if(Settings.Default.rimworldDirSet)
            {
                tDefDumper = new ThoughtDefDumper();
                tDefDumper.Dump();
                foreach(string defName in tDefDumper.defList.Keys)
                {
                    availableThoughtBox.Items.Add(tDefDumper.defList[defName]);
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
            //Don't do anything if a save hasn't been loaded
            if (handler == null) { return; }

            //populate Relevant sections
            healthBox.Text = GetSelectedPawn().pawnHealth.InnerText;
            RefreshSkills();
            RefreshPassions();
            RefreshThoughts();
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
            foreach(string key in textBoxes.Keys)
            {
                textBoxes[key].Enabled = !textBoxes[key].Enabled;
            }
            foreach(string key in comboBoxes.Keys)
            {
                comboBoxes[key].Enabled = !comboBoxes[key].Enabled;
            }
            saveButton.Enabled = !saveButton.Enabled;
            removeThoughtButton.Enabled = !removeThoughtButton.Enabled;
            healthBox.Enabled = !healthBox.Enabled;
            backupCheck.Enabled = !backupCheck.Enabled;
            dirChangeButton.Enabled = !dirChangeButton.Enabled;
            availableThoughtBox.Enabled = !availableThoughtBox.Enabled;
            addThoughtButton.Enabled = !addThoughtButton.Enabled;
        }

        //Populates SkillName-Control mappings and reverses
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

        //the following refresh* methods were made to encapsulate changes so we're not refreshing everything when one thing changes except when needed
        private void RefreshSkills()
        {
            foreach (string key in textBoxes.Keys)
            {
                textBoxes[key].Text = GetSelectedPawn().skillNodes[key].InnerText;
            }

        }

        private void RefreshPassions()
        {

            foreach (string key in comboBoxes.Keys)
            {
                if (GetSelectedPawn().passionNodes[key] == null || GetSelectedPawn().passionNodes[key].InnerText == null)
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
                    comboBoxes[key].SelectedText = GetSelectedPawn().passionNodes[key].InnerText;
                }
            }
        }

        private void RefreshThoughts()
        {
            thoughtBox.Items.Clear();
            foreach (string defName in GetSelectedPawn().thoughtNodes.Keys)
            {
                thoughtBox.Items.Add(defName);
            }
        }

        private void removeThoughtButton_Click(object sender, EventArgs e)
        {
            string key = thoughtBox.GetItemText(thoughtBox.SelectedItem);
            GetSelectedPawn().thoughtNodes[key].ParentNode.RemoveChild(GetSelectedPawn().thoughtNodes[key]);
            GetSelectedPawn().thoughtNodes.Remove(key);
            RefreshThoughts();
        }

        private void updateBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.updateCheckEnabled = !Settings.Default.updateCheckEnabled;
            Settings.Default.Save();
            if(!updateChecked)
            {
                CheckUpdate();
            }
        }

        private void addThoughtButton_Click(object sender, EventArgs e)
        {
            string defName = tDefDumper.defListReverse[availableThoughtBox.GetItemText(availableThoughtBox.SelectedItem)];
            if (defName.Contains(':'))
            {
                defName = defName.Split(':')[0];
            }
            handler.AddThought(defName, GetSelectedPawn().pawn);
            GetSelectedPawn().thoughtNodes = handler.RepopThoughtsForPawn(GetSelectedPawn().pawn);
            RefreshThoughts();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderSelector = new FolderBrowserDialog();
            folderSelector.Description = "Please find the path to your rimworld directory. This is important for thoughtDefs and traitDefs to function.";
            if (folderSelector.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.rimworldDir = folderSelector.SelectedPath;
                Settings.Default.rimworldDirSet = true;
                Settings.Default.Save();
            }

            if (Settings.Default.rimworldDirSet)
            {
                tDefDumper = new ThoughtDefDumper();
                tDefDumper.Dump();
                foreach (string defName in tDefDumper.defList.Keys)
                {
                    availableThoughtBox.Items.Add(tDefDumper.defList[defName]);
                }
            }
        }
    }
}
