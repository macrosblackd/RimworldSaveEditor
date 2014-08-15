﻿using RimWorldSaveEditor.Properties;
namespace RimWorldSaveEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.updateBox = new System.Windows.Forms.CheckBox();
            this.removeThoughtButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.thoughtBox = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.healthBox = new System.Windows.Forms.TextBox();
            this.backupCheck = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.openFileButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.socialPassion = new System.Windows.Forms.ComboBox();
            this.shootingPassion = new System.Windows.Forms.ComboBox();
            this.researchPassion = new System.Windows.Forms.ComboBox();
            this.miningPassion = new System.Windows.Forms.ComboBox();
            this.meleePassion = new System.Windows.Forms.ComboBox();
            this.medicinePassion = new System.Windows.Forms.ComboBox();
            this.growingPassion = new System.Windows.Forms.ComboBox();
            this.craftingPassion = new System.Windows.Forms.ComboBox();
            this.cookingPassion = new System.Windows.Forms.ComboBox();
            this.constructionPassion = new System.Windows.Forms.ComboBox();
            this.artisticPassion = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.socialBox = new System.Windows.Forms.TextBox();
            this.shootingBox = new System.Windows.Forms.TextBox();
            this.researchBox = new System.Windows.Forms.TextBox();
            this.miningBox = new System.Windows.Forms.TextBox();
            this.meleeBox = new System.Windows.Forms.TextBox();
            this.medicineBox = new System.Windows.Forms.TextBox();
            this.growingBox = new System.Windows.Forms.TextBox();
            this.craftingBox = new System.Windows.Forms.TextBox();
            this.cookingBox = new System.Windows.Forms.TextBox();
            this.constructionBox = new System.Windows.Forms.TextBox();
            this.artisticBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colonistListBox = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(535, 401);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.updateBox);
            this.tabPage1.Controls.Add(this.removeThoughtButton);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.backupCheck);
            this.tabPage1.Controls.Add(this.saveButton);
            this.tabPage1.Controls.Add(this.openFileButton);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(527, 375);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Colonist";
            // 
            // updateBox
            // 
            this.updateBox.AutoSize = true;
            this.updateBox.Checked = global::RimWorldSaveEditor.Properties.Settings.Default.updateCheckEnabled;
            this.updateBox.Location = new System.Drawing.Point(210, 327);
            this.updateBox.Name = "updateBox";
            this.updateBox.Size = new System.Drawing.Size(115, 17);
            this.updateBox.TabIndex = 8;
            this.updateBox.Text = "Check for Updates";
            this.updateBox.UseVisualStyleBackColor = true;
            this.updateBox.CheckedChanged += new System.EventHandler(this.updateBox_CheckedChanged);
            // 
            // removeThoughtButton
            // 
            this.removeThoughtButton.Location = new System.Drawing.Point(388, 323);
            this.removeThoughtButton.Name = "removeThoughtButton";
            this.removeThoughtButton.Size = new System.Drawing.Size(100, 23);
            this.removeThoughtButton.TabIndex = 7;
            this.removeThoughtButton.Text = "Remove Thought";
            this.removeThoughtButton.UseVisualStyleBackColor = true;
            this.removeThoughtButton.Click += new System.EventHandler(this.removeThoughtButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.thoughtBox);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.healthBox);
            this.groupBox3.Location = new System.Drawing.Point(353, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 311);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(59, 45);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Thoughts";
            // 
            // thoughtBox
            // 
            this.thoughtBox.FormattingEnabled = true;
            this.thoughtBox.Location = new System.Drawing.Point(14, 65);
            this.thoughtBox.Name = "thoughtBox";
            this.thoughtBox.Size = new System.Drawing.Size(148, 225);
            this.thoughtBox.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(59, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Health";
            // 
            // healthBox
            // 
            this.healthBox.Location = new System.Drawing.Point(6, 19);
            this.healthBox.Name = "healthBox";
            this.healthBox.Size = new System.Drawing.Size(47, 20);
            this.healthBox.TabIndex = 0;
            this.healthBox.Tag = "health";
            this.healthBox.TextChanged += new System.EventHandler(this.healthBox_TextChanged);
            // 
            // backupCheck
            // 
            this.backupCheck.AutoSize = true;
            this.backupCheck.Checked = true;
            this.backupCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backupCheck.Location = new System.Drawing.Point(19, 352);
            this.backupCheck.Name = "backupCheck";
            this.backupCheck.Size = new System.Drawing.Size(99, 17);
            this.backupCheck.TabIndex = 4;
            this.backupCheck.Text = "Enable Backup";
            this.backupCheck.UseVisualStyleBackColor = true;
            this.backupCheck.CheckedChanged += new System.EventHandler(this.backupCheck_CheckedChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(84, 323);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(88, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save Changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(3, 323);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 2;
            this.openFileButton.Text = "Load";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.socialPassion);
            this.groupBox2.Controls.Add(this.shootingPassion);
            this.groupBox2.Controls.Add(this.researchPassion);
            this.groupBox2.Controls.Add(this.miningPassion);
            this.groupBox2.Controls.Add(this.meleePassion);
            this.groupBox2.Controls.Add(this.medicinePassion);
            this.groupBox2.Controls.Add(this.growingPassion);
            this.groupBox2.Controls.Add(this.craftingPassion);
            this.groupBox2.Controls.Add(this.cookingPassion);
            this.groupBox2.Controls.Add(this.constructionPassion);
            this.groupBox2.Controls.Add(this.artisticPassion);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.socialBox);
            this.groupBox2.Controls.Add(this.shootingBox);
            this.groupBox2.Controls.Add(this.researchBox);
            this.groupBox2.Controls.Add(this.miningBox);
            this.groupBox2.Controls.Add(this.meleeBox);
            this.groupBox2.Controls.Add(this.medicineBox);
            this.groupBox2.Controls.Add(this.growingBox);
            this.groupBox2.Controls.Add(this.craftingBox);
            this.groupBox2.Controls.Add(this.cookingBox);
            this.groupBox2.Controls.Add(this.constructionBox);
            this.groupBox2.Controls.Add(this.artisticBox);
            this.groupBox2.Location = new System.Drawing.Point(156, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 311);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Skills";
            // 
            // socialPassion
            // 
            this.socialPassion.FormattingEnabled = true;
            this.socialPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.socialPassion.Location = new System.Drawing.Point(126, 279);
            this.socialPassion.Name = "socialPassion";
            this.socialPassion.Size = new System.Drawing.Size(56, 21);
            this.socialPassion.TabIndex = 32;
            // 
            // shootingPassion
            // 
            this.shootingPassion.FormattingEnabled = true;
            this.shootingPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.shootingPassion.Location = new System.Drawing.Point(126, 253);
            this.shootingPassion.Name = "shootingPassion";
            this.shootingPassion.Size = new System.Drawing.Size(56, 21);
            this.shootingPassion.TabIndex = 31;
            // 
            // researchPassion
            // 
            this.researchPassion.FormattingEnabled = true;
            this.researchPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.researchPassion.Location = new System.Drawing.Point(126, 227);
            this.researchPassion.Name = "researchPassion";
            this.researchPassion.Size = new System.Drawing.Size(56, 21);
            this.researchPassion.TabIndex = 30;
            // 
            // miningPassion
            // 
            this.miningPassion.FormattingEnabled = true;
            this.miningPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.miningPassion.Location = new System.Drawing.Point(126, 201);
            this.miningPassion.Name = "miningPassion";
            this.miningPassion.Size = new System.Drawing.Size(56, 21);
            this.miningPassion.TabIndex = 29;
            // 
            // meleePassion
            // 
            this.meleePassion.FormattingEnabled = true;
            this.meleePassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.meleePassion.Location = new System.Drawing.Point(126, 175);
            this.meleePassion.Name = "meleePassion";
            this.meleePassion.Size = new System.Drawing.Size(56, 21);
            this.meleePassion.TabIndex = 28;
            // 
            // medicinePassion
            // 
            this.medicinePassion.FormattingEnabled = true;
            this.medicinePassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.medicinePassion.Location = new System.Drawing.Point(126, 149);
            this.medicinePassion.Name = "medicinePassion";
            this.medicinePassion.Size = new System.Drawing.Size(56, 21);
            this.medicinePassion.TabIndex = 27;
            // 
            // growingPassion
            // 
            this.growingPassion.FormattingEnabled = true;
            this.growingPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.growingPassion.Location = new System.Drawing.Point(126, 123);
            this.growingPassion.Name = "growingPassion";
            this.growingPassion.Size = new System.Drawing.Size(56, 21);
            this.growingPassion.TabIndex = 26;
            // 
            // craftingPassion
            // 
            this.craftingPassion.FormattingEnabled = true;
            this.craftingPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.craftingPassion.Location = new System.Drawing.Point(126, 97);
            this.craftingPassion.Name = "craftingPassion";
            this.craftingPassion.Size = new System.Drawing.Size(56, 21);
            this.craftingPassion.TabIndex = 25;
            // 
            // cookingPassion
            // 
            this.cookingPassion.FormattingEnabled = true;
            this.cookingPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.cookingPassion.Location = new System.Drawing.Point(126, 71);
            this.cookingPassion.Name = "cookingPassion";
            this.cookingPassion.Size = new System.Drawing.Size(56, 21);
            this.cookingPassion.TabIndex = 24;
            // 
            // constructionPassion
            // 
            this.constructionPassion.FormattingEnabled = true;
            this.constructionPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.constructionPassion.Location = new System.Drawing.Point(126, 45);
            this.constructionPassion.Name = "constructionPassion";
            this.constructionPassion.Size = new System.Drawing.Size(56, 21);
            this.constructionPassion.TabIndex = 23;
            // 
            // artisticPassion
            // 
            this.artisticPassion.DisplayMember = "passionValues";
            this.artisticPassion.FormattingEnabled = true;
            this.artisticPassion.Items.AddRange(new object[] {
            "None",
            "Major",
            "Minor"});
            this.artisticPassion.Location = new System.Drawing.Point(126, 19);
            this.artisticPassion.Name = "artisticPassion";
            this.artisticPassion.Size = new System.Drawing.Size(56, 21);
            this.artisticPassion.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(51, 282);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Social";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(51, 256);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Shooting";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 230);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Research";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(51, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Mining";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Melee";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Medicine";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Growing";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Crafting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Cooking";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Construction";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Artistic";
            // 
            // socialBox
            // 
            this.socialBox.Location = new System.Drawing.Point(6, 279);
            this.socialBox.Name = "socialBox";
            this.socialBox.Size = new System.Drawing.Size(39, 20);
            this.socialBox.TabIndex = 10;
            // 
            // shootingBox
            // 
            this.shootingBox.Location = new System.Drawing.Point(6, 253);
            this.shootingBox.Name = "shootingBox";
            this.shootingBox.Size = new System.Drawing.Size(39, 20);
            this.shootingBox.TabIndex = 9;
            // 
            // researchBox
            // 
            this.researchBox.Location = new System.Drawing.Point(6, 227);
            this.researchBox.Name = "researchBox";
            this.researchBox.Size = new System.Drawing.Size(39, 20);
            this.researchBox.TabIndex = 8;
            // 
            // miningBox
            // 
            this.miningBox.Location = new System.Drawing.Point(6, 201);
            this.miningBox.Name = "miningBox";
            this.miningBox.Size = new System.Drawing.Size(39, 20);
            this.miningBox.TabIndex = 7;
            // 
            // meleeBox
            // 
            this.meleeBox.Location = new System.Drawing.Point(6, 175);
            this.meleeBox.Name = "meleeBox";
            this.meleeBox.Size = new System.Drawing.Size(39, 20);
            this.meleeBox.TabIndex = 6;
            // 
            // medicineBox
            // 
            this.medicineBox.Location = new System.Drawing.Point(6, 149);
            this.medicineBox.Name = "medicineBox";
            this.medicineBox.Size = new System.Drawing.Size(39, 20);
            this.medicineBox.TabIndex = 5;
            // 
            // growingBox
            // 
            this.growingBox.Location = new System.Drawing.Point(6, 123);
            this.growingBox.Name = "growingBox";
            this.growingBox.Size = new System.Drawing.Size(39, 20);
            this.growingBox.TabIndex = 4;
            // 
            // craftingBox
            // 
            this.craftingBox.Location = new System.Drawing.Point(6, 97);
            this.craftingBox.Name = "craftingBox";
            this.craftingBox.Size = new System.Drawing.Size(39, 20);
            this.craftingBox.TabIndex = 3;
            // 
            // cookingBox
            // 
            this.cookingBox.Location = new System.Drawing.Point(6, 71);
            this.cookingBox.Name = "cookingBox";
            this.cookingBox.Size = new System.Drawing.Size(39, 20);
            this.cookingBox.TabIndex = 2;
            // 
            // constructionBox
            // 
            this.constructionBox.Location = new System.Drawing.Point(6, 45);
            this.constructionBox.Name = "constructionBox";
            this.constructionBox.Size = new System.Drawing.Size(39, 20);
            this.constructionBox.TabIndex = 1;
            // 
            // artisticBox
            // 
            this.artisticBox.Location = new System.Drawing.Point(6, 19);
            this.artisticBox.Name = "artisticBox";
            this.artisticBox.Size = new System.Drawing.Size(39, 20);
            this.artisticBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.colonistListBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 311);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colonists";
            // 
            // colonistListBox
            // 
            this.colonistListBox.FormattingEnabled = true;
            this.colonistListBox.Location = new System.Drawing.Point(6, 19);
            this.colonistListBox.Name = "colonistListBox";
            this.colonistListBox.Size = new System.Drawing.Size(135, 277);
            this.colonistListBox.TabIndex = 0;
            this.colonistListBox.SelectedIndexChanged += new System.EventHandler(this.colonistListBox_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(213, 347);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(302, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Warning: removing thoughts may have unintended side-effects";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(279, 360);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(138, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "USE AT YOUR OWN RISK";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 424);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox backupCheck;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox socialBox;
        private System.Windows.Forms.TextBox shootingBox;
        private System.Windows.Forms.TextBox researchBox;
        private System.Windows.Forms.TextBox miningBox;
        private System.Windows.Forms.TextBox meleeBox;
        private System.Windows.Forms.TextBox medicineBox;
        private System.Windows.Forms.TextBox growingBox;
        private System.Windows.Forms.TextBox craftingBox;
        private System.Windows.Forms.TextBox cookingBox;
        private System.Windows.Forms.TextBox constructionBox;
        private System.Windows.Forms.TextBox artisticBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox colonistListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox healthBox;
        private System.Windows.Forms.ComboBox socialPassion;
        private System.Windows.Forms.ComboBox shootingPassion;
        private System.Windows.Forms.ComboBox researchPassion;
        private System.Windows.Forms.ComboBox miningPassion;
        private System.Windows.Forms.ComboBox meleePassion;
        private System.Windows.Forms.ComboBox medicinePassion;
        private System.Windows.Forms.ComboBox growingPassion;
        private System.Windows.Forms.ComboBox craftingPassion;
        private System.Windows.Forms.ComboBox cookingPassion;
        private System.Windows.Forms.ComboBox constructionPassion;
        private System.Windows.Forms.ComboBox artisticPassion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox thoughtBox;
        private System.Windows.Forms.Button removeThoughtButton;
        private System.Windows.Forms.CheckBox updateBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

