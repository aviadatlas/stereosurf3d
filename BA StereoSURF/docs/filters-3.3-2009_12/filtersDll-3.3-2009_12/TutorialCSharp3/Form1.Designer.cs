namespace TutorialCSharp3
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowDevices = new System.Windows.Forms.Button();
            this.cbWebCamDevice = new System.Windows.Forms.ListBox();
            this.lstReadDeviceFormat = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkWebCamLive = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtImageSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(495, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Project Filters at http://filters.sourceforge.net ,  contact: filters@edurand.com" +
                "";
            // 
            // btnShowDevices
            // 
            this.btnShowDevices.Location = new System.Drawing.Point(13, 28);
            this.btnShowDevices.Name = "btnShowDevices";
            this.btnShowDevices.Size = new System.Drawing.Size(140, 23);
            this.btnShowDevices.TabIndex = 20;
            this.btnShowDevices.Text = "Show video devices";
            this.btnShowDevices.UseVisualStyleBackColor = true;
            this.btnShowDevices.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbWebCamDevice
            // 
            this.cbWebCamDevice.FormattingEnabled = true;
            this.cbWebCamDevice.Location = new System.Drawing.Point(13, 58);
            this.cbWebCamDevice.Name = "cbWebCamDevice";
            this.cbWebCamDevice.Size = new System.Drawing.Size(400, 95);
            this.cbWebCamDevice.TabIndex = 21;
            this.cbWebCamDevice.SelectedIndexChanged += new System.EventHandler(this.cbWebCamDevice_SelectedIndexChanged);
            // 
            // lstReadDeviceFormat
            // 
            this.lstReadDeviceFormat.FormattingEnabled = true;
            this.lstReadDeviceFormat.Location = new System.Drawing.Point(13, 177);
            this.lstReadDeviceFormat.Name = "lstReadDeviceFormat";
            this.lstReadDeviceFormat.Size = new System.Drawing.Size(400, 95);
            this.lstReadDeviceFormat.TabIndex = 22;
            this.lstReadDeviceFormat.SelectedIndexChanged += new System.EventHandler(this.lstReadDeviceFormat_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "formats";
            // 
            // chkWebCamLive
            // 
            this.chkWebCamLive.AutoSize = true;
            this.chkWebCamLive.Location = new System.Drawing.Point(13, 295);
            this.chkWebCamLive.Name = "chkWebCamLive";
            this.chkWebCamLive.Size = new System.Drawing.Size(42, 17);
            this.chkWebCamLive.TabIndex = 24;
            this.chkWebCamLive.Text = "live";
            this.chkWebCamLive.UseVisualStyleBackColor = true;
            this.chkWebCamLive.CheckedChanged += new System.EventHandler(this.chkWebCamLive_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtImageSize);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(435, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 408);
            this.panel1.TabIndex = 25;
            // 
            // txtImageSize
            // 
            this.txtImageSize.Enabled = false;
            this.txtImageSize.Location = new System.Drawing.Point(72, 3);
            this.txtImageSize.Name = "txtImageSize";
            this.txtImageSize.Size = new System.Drawing.Size(192, 20);
            this.txtImageSize.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "image size";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(1, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(428, 378);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(13, 319);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 26;
            this.btnCapture.Text = "capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 446);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkWebCamLive);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstReadDeviceFormat);
            this.Controls.Add(this.cbWebCamDevice);
            this.Controls.Add(this.btnShowDevices);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Filters - Tutorial C# 3";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShowDevices;
        private System.Windows.Forms.ListBox cbWebCamDevice;
        private System.Windows.Forms.ListBox lstReadDeviceFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkWebCamLive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtImageSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCapture;
    }
}

