using System.Xml.Linq;
using RimWorldSaveEditor.models;
using RimWorldSaveEditor.Properties;
using System;
using System.Collections.Generic;
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
        SortedList<string, TextBox> skillTextBoxes;
        Dictionary<TextBox, string> skillTextBoxesReverse;
        SortedList<string, ComboBox> skillComboBoxes;
        Dictionary<ComboBox, string> skillComboBoxesReverse;
        ThoughtDefDumper tDefDumper;
        private SortedList<string, ComboboxItem<Backstory>> backstoryItems;

        bool toggleOnce,updateChecked;

        //Will need to null check colony name
        public Form1()
        {
            InitializeComponent();
            Version asmVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string version = String.Format("{0}.{1}.{2}.{3}", asmVersion.Major, asmVersion.Minor, asmVersion.Build, asmVersion.Revision);
            currentVersion = String.Format(version + ".{0}", asmVersion.Revision);
            this.Text = String.Format("RimWorld Save Editor Version: {0}", version);
            PopulateSkillControlLists();
            PopulateBackstories();
            AssignSkillsEventHandler();
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

        //Assign textboxes and comboboxes in skillGroup to a single event.
        public void AssignSkillsEventHandler()
        {
            foreach (Control ctrl in skillsGroup.Controls)
            {
                var txt = ctrl as TextBox;
                var cbo = ctrl as ComboBox;
                if (txt != null)
                {
                    txt.TextChanged += SkillTextBoxChanged;
                }
                else if (cbo != null)
                {
                    cbo.SelectedIndexChanged += SkillComboBoxChanged; 
                }
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
                PopulateAvailableThoughts();
            }
        }

        public static string CapitalizedNoSpaces(string s)
        {
            string str1 = s;
            char[] chArray = new char[1];
            int index = 0;
            int num = 32;
            chArray[index] = (char)num;
            string[] strArray = str1.Split(chArray);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str2 in strArray)
            {
                if (str2.Length > 0)
                    stringBuilder.Append(char.ToUpper(str2[0]));
                if (str2.Length > 1)
                    stringBuilder.Append(str2.Substring(1));
            }
            return stringBuilder.ToString();
        }

        private void SkillTextBoxChanged(object sender, EventArgs args)
        {
            var changedBox = (TextBox)sender;
            XmlNode modNode = GetSelectedPawn().skillNodes[skillTextBoxesReverse[changedBox]];
            handler.ModifyNode(modNode, changedBox.Text);
        }

        private void SkillComboBoxChanged(object sender, EventArgs args)
        {
            var changedBox = (ComboBox)sender;
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
                handler.RemovePassionNode(GetSelectedPawn().skillNodes[skillComboBoxesReverse[changedBox]].ParentNode);
            }

            XmlNode pNode = GetSelectedPawn().passionNodes[skillComboBoxesReverse[changedBox]];
            if (pNode != null && pNode.InnerText != null)
            {
                handler.ModifyNode(pNode, value);
            }
            else
            {
                XmlNode passNode = GetSelectedPawn().skillNodes[skillComboBoxesReverse[changedBox]].ParentNode;
                //GetSelectedPawn().passionNodes[skillComboBoxesReverse[changedBox]] = 
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
            RefreshColonistBackstories();
            RefreshHealth();
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
            foreach(string key in skillTextBoxes.Keys)
            {
                skillTextBoxes[key].Enabled = !skillTextBoxes[key].Enabled;
            }
            foreach(string key in skillComboBoxes.Keys)
            {
                skillComboBoxes[key].Enabled = !skillComboBoxes[key].Enabled;
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
        private void PopulateSkillControlLists()
        {
            skillTextBoxes = new SortedList<string, TextBox>();
            skillTextBoxes.Add("Artistic", artisticBox);
            skillTextBoxes.Add("Construction", constructionBox);
            skillTextBoxes.Add("Cooking", cookingBox);
            skillTextBoxes.Add("Crafting", craftingBox);
            skillTextBoxes.Add("Growing", growingBox);
            skillTextBoxes.Add("Medicine", medicineBox);
            skillTextBoxes.Add("Melee", meleeBox);
            skillTextBoxes.Add("Mining", miningBox);
            skillTextBoxes.Add("Research", researchBox);
            skillTextBoxes.Add("Shooting", shootingBox);
            skillTextBoxes.Add("Social", socialBox);
            skillTextBoxesReverse = skillTextBoxes.ToDictionary(x => x.Value, x => x.Key);


            skillComboBoxes = new SortedList<string, ComboBox>();
            skillComboBoxes.Add("Artistic", artisticPassion);
            skillComboBoxes.Add("Construction", constructionPassion);
            skillComboBoxes.Add("Cooking", cookingPassion);
            skillComboBoxes.Add("Crafting", craftingPassion);
            skillComboBoxes.Add("Growing", growingPassion);
            skillComboBoxes.Add("Medicine", medicinePassion);
            skillComboBoxes.Add("Melee", meleePassion);
            skillComboBoxes.Add("Mining", miningPassion);
            skillComboBoxes.Add("Research", researchPassion);
            skillComboBoxes.Add("Shooting", shootingPassion);
            skillComboBoxes.Add("Social", socialPassion);
            skillComboBoxesReverse = skillComboBoxes.ToDictionary(x => x.Value, x => x.Key);
        }

        //the following refresh* methods were made to encapsulate changes so we're not refreshing everything when one thing changes except when needed
        private void RefreshSkills()
        {
            foreach (string key in skillTextBoxes.Keys)
            {
                skillTextBoxes[key].Text = GetSelectedPawn().skillNodes[key].InnerText;
            }

        }

        private void RefreshPassions()
        {

            foreach (string key in skillComboBoxes.Keys)
            {
                if (GetSelectedPawn().passionNodes[key] == null || GetSelectedPawn().passionNodes[key].InnerText == null)
                {
                    if (skillComboBoxes[key].SelectedText != null)
                    {
                        skillComboBoxes[key].ResetText();
                    }
                    skillComboBoxes[key].SelectedText = "None";
                }
                else
                {
                    if (skillComboBoxes[key].SelectedText != null)
                    {
                        skillComboBoxes[key].ResetText();
                    }
                    skillComboBoxes[key].SelectedText = GetSelectedPawn().passionNodes[key].InnerText;
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

        private void RefreshColonistBackstories()
        {
            childBackstory.SelectedItem = backstoryItems[GetSelectedPawn().childhood.InnerText];
            adultBackstory.SelectedItem = backstoryItems[GetSelectedPawn().adulthood.InnerText];
        }

        private void RefreshHealth()
        {
            //TODO: SETUP LATER
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
                PopulateAvailableThoughts();
            }
        }

        private void PopulateBackstories()
        {
            backstoryItems = new SortedList<string, ComboboxItem<Backstory>>(BackstoryLoader.LoadBackstories()
                .ToDictionary(s => s.DefName, s => new ComboboxItem<Backstory>(s.Title, s)));
            adultBackstory.Items.AddRange(backstoryItems.Values.Where(s => s.Value.Slot == BackstorySlot.Adulthood).ToArray());
            childBackstory.Items.AddRange(backstoryItems.Values.Where(s => s.Value.Slot == BackstorySlot.Childhood).ToArray());
        }

        private void PopulateAvailableThoughts()
        {
            tDefDumper = new ThoughtDefDumper();
            tDefDumper.Dump();
            foreach (string defName in tDefDumper.defList.Keys)
            {
                availableThoughtBox.Items.Add(tDefDumper.defList[defName]);
            }
        }

        private class ComboboxItem<T>
        {
            public string Name { get; private set; }
            public T Value { get; private set; }

            public ComboboxItem(string name, T value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
