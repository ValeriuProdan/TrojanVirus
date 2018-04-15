namespace Server
{
    partial class ClientForm
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
            this.messageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.rotateDisplayButton = new System.Windows.Forms.Button();
            this.getHierarchyButton = new System.Windows.Forms.Button();
            this.commandRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sendCommandButton = new System.Windows.Forms.Button();
            this.appendCheckBox = new System.Windows.Forms.CheckBox();
            this.destroyProcessorButton = new System.Windows.Forms.Button();
            this.destroyRamButton = new System.Windows.Forms.Button();
            this.filePathRichTextBox = new System.Windows.Forms.RichTextBox();
            this.copyFileButton = new System.Windows.Forms.Button();
            this.viewLogFilebutton = new System.Windows.Forms.Button();
            this.destroyHardButton = new System.Windows.Forms.Button();
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // messageRichTextBox
            // 
            this.messageRichTextBox.Location = new System.Drawing.Point(3, 12);
            this.messageRichTextBox.Name = "messageRichTextBox";
            this.messageRichTextBox.Size = new System.Drawing.Size(232, 23);
            this.messageRichTextBox.TabIndex = 0;
            this.messageRichTextBox.Text = "";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(241, 12);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(96, 23);
            this.sendMessageButton.TabIndex = 2;
            this.sendMessageButton.Text = "sendMessage";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // rotateDisplayButton
            // 
            this.rotateDisplayButton.Location = new System.Drawing.Point(12, 41);
            this.rotateDisplayButton.Name = "rotateDisplayButton";
            this.rotateDisplayButton.Size = new System.Drawing.Size(96, 23);
            this.rotateDisplayButton.TabIndex = 3;
            this.rotateDisplayButton.Text = "rotateDisplay";
            this.rotateDisplayButton.UseVisualStyleBackColor = true;
            this.rotateDisplayButton.Click += new System.EventHandler(this.rotateDisplayButton_Click);
            // 
            // getHierarchyButton
            // 
            this.getHierarchyButton.Location = new System.Drawing.Point(12, 80);
            this.getHierarchyButton.Name = "getHierarchyButton";
            this.getHierarchyButton.Size = new System.Drawing.Size(75, 23);
            this.getHierarchyButton.TabIndex = 4;
            this.getHierarchyButton.Text = "getHierarchy";
            this.getHierarchyButton.UseVisualStyleBackColor = true;
            this.getHierarchyButton.Click += new System.EventHandler(this.getHierarchyButton_Click);
            // 
            // commandRichTextBox
            // 
            this.commandRichTextBox.Location = new System.Drawing.Point(3, 109);
            this.commandRichTextBox.Name = "commandRichTextBox";
            this.commandRichTextBox.Size = new System.Drawing.Size(232, 23);
            this.commandRichTextBox.TabIndex = 5;
            this.commandRichTextBox.Text = "";
            // 
            // sendCommandButton
            // 
            this.sendCommandButton.Location = new System.Drawing.Point(309, 107);
            this.sendCommandButton.Name = "sendCommandButton";
            this.sendCommandButton.Size = new System.Drawing.Size(96, 23);
            this.sendCommandButton.TabIndex = 6;
            this.sendCommandButton.Text = "sendCommand";
            this.sendCommandButton.UseVisualStyleBackColor = true;
            this.sendCommandButton.Click += new System.EventHandler(this.sendCommandButton_Click);
            // 
            // appendCheckBox
            // 
            this.appendCheckBox.AutoSize = true;
            this.appendCheckBox.Location = new System.Drawing.Point(241, 111);
            this.appendCheckBox.Name = "appendCheckBox";
            this.appendCheckBox.Size = new System.Drawing.Size(62, 17);
            this.appendCheckBox.TabIndex = 7;
            this.appendCheckBox.Text = "append";
            this.appendCheckBox.UseVisualStyleBackColor = true;
            // 
            // destroyProcessorButton
            // 
            this.destroyProcessorButton.Location = new System.Drawing.Point(3, 150);
            this.destroyProcessorButton.Name = "destroyProcessorButton";
            this.destroyProcessorButton.Size = new System.Drawing.Size(96, 23);
            this.destroyProcessorButton.TabIndex = 8;
            this.destroyProcessorButton.Text = "destroyProcessor";
            this.destroyProcessorButton.UseVisualStyleBackColor = true;
            this.destroyProcessorButton.Click += new System.EventHandler(this.destroyProcessorButton_Click);
            // 
            // destroyRamButton
            // 
            this.destroyRamButton.Location = new System.Drawing.Point(139, 150);
            this.destroyRamButton.Name = "destroyRamButton";
            this.destroyRamButton.Size = new System.Drawing.Size(96, 23);
            this.destroyRamButton.TabIndex = 9;
            this.destroyRamButton.Text = "destroyRam";
            this.destroyRamButton.UseVisualStyleBackColor = true;
            this.destroyRamButton.Click += new System.EventHandler(this.destroyRamButton_Click);
            // 
            // filePathRichTextBox
            // 
            this.filePathRichTextBox.Location = new System.Drawing.Point(3, 191);
            this.filePathRichTextBox.Name = "filePathRichTextBox";
            this.filePathRichTextBox.Size = new System.Drawing.Size(495, 23);
            this.filePathRichTextBox.TabIndex = 10;
            this.filePathRichTextBox.Text = "";
            // 
            // copyFileButton
            // 
            this.copyFileButton.Location = new System.Drawing.Point(504, 191);
            this.copyFileButton.Name = "copyFileButton";
            this.copyFileButton.Size = new System.Drawing.Size(96, 23);
            this.copyFileButton.TabIndex = 11;
            this.copyFileButton.Text = "copyFile";
            this.copyFileButton.UseVisualStyleBackColor = true;
            this.copyFileButton.Click += new System.EventHandler(this.copyFileButton_Click);
            // 
            // viewLogFilebutton
            // 
            this.viewLogFilebutton.Location = new System.Drawing.Point(3, 220);
            this.viewLogFilebutton.Name = "viewLogFilebutton";
            this.viewLogFilebutton.Size = new System.Drawing.Size(96, 23);
            this.viewLogFilebutton.TabIndex = 12;
            this.viewLogFilebutton.Text = "viewLogFile";
            this.viewLogFilebutton.UseVisualStyleBackColor = true;
            this.viewLogFilebutton.Click += new System.EventHandler(this.viewLogFilebutton_Click);
            // 
            // destroyHardButton
            // 
            this.destroyHardButton.Location = new System.Drawing.Point(270, 150);
            this.destroyHardButton.Name = "destroyHardButton";
            this.destroyHardButton.Size = new System.Drawing.Size(96, 23);
            this.destroyHardButton.TabIndex = 13;
            this.destroyHardButton.Text = "destroyHard";
            this.destroyHardButton.UseVisualStyleBackColor = true;
            this.destroyHardButton.Click += new System.EventHandler(this.destroyHardButton_Click);
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.Location = new System.Drawing.Point(681, 14);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.Size = new System.Drawing.Size(569, 333);
            this.outputRichTextBox.TabIndex = 14;
            this.outputRichTextBox.Text = "";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 450);
            this.Controls.Add(this.outputRichTextBox);
            this.Controls.Add(this.destroyHardButton);
            this.Controls.Add(this.viewLogFilebutton);
            this.Controls.Add(this.copyFileButton);
            this.Controls.Add(this.filePathRichTextBox);
            this.Controls.Add(this.destroyRamButton);
            this.Controls.Add(this.destroyProcessorButton);
            this.Controls.Add(this.appendCheckBox);
            this.Controls.Add(this.sendCommandButton);
            this.Controls.Add(this.commandRichTextBox);
            this.Controls.Add(this.getHierarchyButton);
            this.Controls.Add(this.rotateDisplayButton);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.messageRichTextBox);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox messageRichTextBox;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Button rotateDisplayButton;
        private System.Windows.Forms.Button getHierarchyButton;
        private System.Windows.Forms.RichTextBox commandRichTextBox;
        private System.Windows.Forms.Button sendCommandButton;
        private System.Windows.Forms.CheckBox appendCheckBox;
        private System.Windows.Forms.Button destroyProcessorButton;
        private System.Windows.Forms.Button destroyRamButton;
        private System.Windows.Forms.RichTextBox filePathRichTextBox;
        private System.Windows.Forms.Button copyFileButton;
        private System.Windows.Forms.Button viewLogFilebutton;
        private System.Windows.Forms.Button destroyHardButton;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
    }
}