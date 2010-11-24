namespace TutorialCSharp1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.cbFiltersName = new System.Windows.Forms.ComboBox();
            this.lstParameters = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstOutputs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filters DLL Version =";
            // 
            // txtVersion
            // 
            this.txtVersion.Enabled = false;
            this.txtVersion.Location = new System.Drawing.Point(122, 13);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(134, 20);
            this.txtVersion.TabIndex = 1;
            // 
            // cbFiltersName
            // 
            this.cbFiltersName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltersName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbFiltersName.FormattingEnabled = true;
            this.cbFiltersName.Location = new System.Drawing.Point(15, 51);
            this.cbFiltersName.Name = "cbFiltersName";
            this.cbFiltersName.Size = new System.Drawing.Size(465, 21);
            this.cbFiltersName.TabIndex = 2;
            this.cbFiltersName.SelectedIndexChanged += new System.EventHandler(this.cbFiltersName_SelectedIndexChanged);
            // 
            // lstParameters
            // 
            this.lstParameters.FormattingEnabled = true;
            this.lstParameters.Location = new System.Drawing.Point(12, 92);
            this.lstParameters.Name = "lstParameters";
            this.lstParameters.Size = new System.Drawing.Size(465, 95);
            this.lstParameters.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "parameters :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "outputs :";
            // 
            // lstOutputs
            // 
            this.lstOutputs.FormattingEnabled = true;
            this.lstOutputs.Location = new System.Drawing.Point(12, 213);
            this.lstOutputs.Name = "lstOutputs";
            this.lstOutputs.Size = new System.Drawing.Size(465, 95);
            this.lstOutputs.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 324);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstOutputs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstParameters);
            this.Controls.Add(this.cbFiltersName);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Filters - Tutorial C# 1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.ComboBox cbFiltersName;
        private System.Windows.Forms.ListBox lstParameters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstOutputs;
    }
}

