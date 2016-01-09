namespace CsharpBattleship
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
            this.playerPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PlayerLabel = new System.Windows.Forms.Label();
            this.enemyPanel = new System.Windows.Forms.Panel();
            this.OpponentLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.triggerLabel = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.RichTextBox();
            this.placeShipsLabel = new System.Windows.Forms.Label();
            this.attackOpponentLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playerPanel
            // 
            this.playerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerPanel.Location = new System.Drawing.Point(12, 43);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(300, 300);
            this.playerPanel.TabIndex = 0;
            this.playerPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.playerPanel_MouseClick);
            this.playerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playerPanel_MouseDown);
            this.playerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.playerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(318, 233);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 35);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Create Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Items.AddRange(new object[] {
            "AircraftCarrier",
            "Battleship",
            "Submarine",
            "Destroyer",
            "PatrolBoat"});
            this.listBox.Location = new System.Drawing.Point(318, 274);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(75, 69);
            this.listBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // PlayerLabel
            // 
            this.PlayerLabel.AutoSize = true;
            this.PlayerLabel.Location = new System.Drawing.Point(12, 27);
            this.PlayerLabel.Name = "PlayerLabel";
            this.PlayerLabel.Size = new System.Drawing.Size(62, 13);
            this.PlayerLabel.TabIndex = 6;
            this.PlayerLabel.Text = "Player (you)";
            // 
            // enemyPanel
            // 
            this.enemyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enemyPanel.Location = new System.Drawing.Point(399, 43);
            this.enemyPanel.Name = "enemyPanel";
            this.enemyPanel.Size = new System.Drawing.Size(300, 300);
            this.enemyPanel.TabIndex = 7;
            this.enemyPanel.Click += new System.EventHandler(this.enemyPanel_Click);
            this.enemyPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.enemyPanel_MouseMove);
            // 
            // OpponentLabel
            // 
            this.OpponentLabel.AutoSize = true;
            this.OpponentLabel.Location = new System.Drawing.Point(399, 26);
            this.OpponentLabel.Name = "OpponentLabel";
            this.OpponentLabel.Size = new System.Drawing.Size(54, 13);
            this.OpponentLabel.TabIndex = 8;
            this.OpponentLabel.Text = "Opponent";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(706, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            // 
            // triggerLabel
            // 
            this.triggerLabel.AutoSize = true;
            this.triggerLabel.Location = new System.Drawing.Point(605, 4);
            this.triggerLabel.Name = "triggerLabel";
            this.triggerLabel.Size = new System.Drawing.Size(36, 13);
            this.triggerLabel.TabIndex = 10;
            this.triggerLabel.Text = "trigger";
            this.triggerLabel.TextChanged += new System.EventHandler(this.triggerLabel_TextChanged);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(208, 352);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(310, 96);
            this.log.TabIndex = 11;
            this.log.Text = "";
            // 
            // placeShipsLabel
            // 
            this.placeShipsLabel.AutoSize = true;
            this.placeShipsLabel.Location = new System.Drawing.Point(12, 349);
            this.placeShipsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.placeShipsLabel.Name = "placeShipsLabel";
            this.placeShipsLabel.Size = new System.Drawing.Size(131, 39);
            this.placeShipsLabel.TabIndex = 12;
            this.placeShipsLabel.Text = "Choose a ship and \r\nclick above to place it.\r\nPress your \'r\' key to rotate.";
            // 
            // attackOpponentLabel
            // 
            this.attackOpponentLabel.AutoSize = true;
            this.attackOpponentLabel.Location = new System.Drawing.Point(564, 349);
            this.attackOpponentLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.attackOpponentLabel.Name = "attackOpponentLabel";
            this.attackOpponentLabel.Size = new System.Drawing.Size(133, 26);
            this.attackOpponentLabel.TabIndex = 13;
            this.attackOpponentLabel.Text = "Click here during your turn \r\nto fire at opponent";
            this.attackOpponentLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 473);
            this.Controls.Add(this.attackOpponentLabel);
            this.Controls.Add(this.placeShipsLabel);
            this.Controls.Add(this.log);
            this.Controls.Add(this.triggerLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.OpponentLabel);
            this.Controls.Add(this.enemyPanel);
            this.Controls.Add(this.PlayerLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel playerPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PlayerLabel;
        private System.Windows.Forms.Panel enemyPanel;
        private System.Windows.Forms.Label OpponentLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label triggerLabel;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Label placeShipsLabel;
        private System.Windows.Forms.Label attackOpponentLabel;
    }
}

