namespace TutorialCSharp2
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtImageCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileNameToSave = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdSobel = new System.Windows.Forms.Button();
            this.cmdRotation = new System.Windows.Forms.Button();
            this.cmdStack = new System.Windows.Forms.Button();
            this.cmdBlobRepositioning2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSobel = new System.Windows.Forms.TabPage();
            this.tpRotation = new System.Windows.Forms.TabPage();
            this.txtRotationAngle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpStackCreator = new System.Windows.Forms.TabPage();
            this.tpBlobRepositioning2 = new System.Windows.Forms.TabPage();
            this.tpBlobExplorer = new System.Windows.Forms.TabPage();
            this.cmdBlobExplorer = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bROI = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bAnalyseImage = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.bCreateDrawImage = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.bConvolutionPersonal = new System.Windows.Forms.Button();
            this.bConvolution = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImageSize = new System.Windows.Forms.TextBox();
            this.txtInfo = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpSobel.SuspendLayout();
            this.tpRotation.SuspendLayout();
            this.tpStackCreator.SuspendLayout();
            this.tpBlobRepositioning2.SuspendLayout();
            this.tpBlobExplorer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(12, 217);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(428, 378);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(13, 31);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(38, 23);
            this.cmdLoad.TabIndex = 2;
            this.cmdLoad.Text = "load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(58, 33);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(703, 20);
            this.txtFileName.TabIndex = 3;
            this.txtFileName.Text = "lenna_color.bmp";
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // txtImageCount
            // 
            this.txtImageCount.Enabled = false;
            this.txtImageCount.Location = new System.Drawing.Point(86, 58);
            this.txtImageCount.Name = "txtImageCount";
            this.txtImageCount.Size = new System.Drawing.Size(53, 20);
            this.txtImageCount.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "image count";
            // 
            // txtFileNameToSave
            // 
            this.txtFileNameToSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileNameToSave.Location = new System.Drawing.Point(58, 84);
            this.txtFileNameToSave.Name = "txtFileNameToSave";
            this.txtFileNameToSave.Size = new System.Drawing.Size(703, 20);
            this.txtFileNameToSave.TabIndex = 7;
            this.txtFileNameToSave.Text = "outputCSharp.tif";
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(13, 82);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(38, 23);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdSobel
            // 
            this.cmdSobel.Location = new System.Drawing.Point(6, 6);
            this.cmdSobel.Name = "cmdSobel";
            this.cmdSobel.Size = new System.Drawing.Size(55, 23);
            this.cmdSobel.TabIndex = 8;
            this.cmdSobel.Text = "Sobel";
            this.cmdSobel.UseVisualStyleBackColor = true;
            this.cmdSobel.Click += new System.EventHandler(this.cmdSobel_Click);
            // 
            // cmdRotation
            // 
            this.cmdRotation.Location = new System.Drawing.Point(6, 6);
            this.cmdRotation.Name = "cmdRotation";
            this.cmdRotation.Size = new System.Drawing.Size(55, 23);
            this.cmdRotation.TabIndex = 9;
            this.cmdRotation.Text = "Rotation";
            this.cmdRotation.UseVisualStyleBackColor = true;
            this.cmdRotation.Click += new System.EventHandler(this.cmdRotation_Click);
            // 
            // cmdStack
            // 
            this.cmdStack.Location = new System.Drawing.Point(6, 6);
            this.cmdStack.Name = "cmdStack";
            this.cmdStack.Size = new System.Drawing.Size(55, 23);
            this.cmdStack.TabIndex = 10;
            this.cmdStack.Text = "Stack";
            this.cmdStack.UseVisualStyleBackColor = true;
            this.cmdStack.Click += new System.EventHandler(this.cmdStack_Click);
            // 
            // cmdBlobRepositioning2
            // 
            this.cmdBlobRepositioning2.Location = new System.Drawing.Point(6, 7);
            this.cmdBlobRepositioning2.Name = "cmdBlobRepositioning2";
            this.cmdBlobRepositioning2.Size = new System.Drawing.Size(115, 23);
            this.cmdBlobRepositioning2.TabIndex = 11;
            this.cmdBlobRepositioning2.Text = "BlobRepositioning2";
            this.cmdBlobRepositioning2.UseVisualStyleBackColor = true;
            this.cmdBlobRepositioning2.Click += new System.EventHandler(this.cmdBlobRepositioning2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpSobel);
            this.tabControl1.Controls.Add(this.tpRotation);
            this.tabControl1.Controls.Add(this.tpStackCreator);
            this.tabControl1.Controls.Add(this.tpBlobRepositioning2);
            this.tabControl1.Controls.Add(this.tpBlobExplorer);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 111);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(749, 62);
            this.tabControl1.TabIndex = 14;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpSobel
            // 
            this.tpSobel.Controls.Add(this.cmdSobel);
            this.tpSobel.Location = new System.Drawing.Point(4, 22);
            this.tpSobel.Name = "tpSobel";
            this.tpSobel.Padding = new System.Windows.Forms.Padding(3);
            this.tpSobel.Size = new System.Drawing.Size(741, 36);
            this.tpSobel.TabIndex = 0;
            this.tpSobel.Text = "Sobel";
            this.tpSobel.UseVisualStyleBackColor = true;
            // 
            // tpRotation
            // 
            this.tpRotation.Controls.Add(this.txtRotationAngle);
            this.tpRotation.Controls.Add(this.label3);
            this.tpRotation.Controls.Add(this.cmdRotation);
            this.tpRotation.Location = new System.Drawing.Point(4, 22);
            this.tpRotation.Name = "tpRotation";
            this.tpRotation.Padding = new System.Windows.Forms.Padding(3);
            this.tpRotation.Size = new System.Drawing.Size(741, 36);
            this.tpRotation.TabIndex = 1;
            this.tpRotation.Text = "Rotation";
            this.tpRotation.UseVisualStyleBackColor = true;
            // 
            // txtRotationAngle
            // 
            this.txtRotationAngle.Location = new System.Drawing.Point(133, 7);
            this.txtRotationAngle.Name = "txtRotationAngle";
            this.txtRotationAngle.Size = new System.Drawing.Size(45, 20);
            this.txtRotationAngle.TabIndex = 11;
            this.txtRotationAngle.Text = "66";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "angle";
            // 
            // tpStackCreator
            // 
            this.tpStackCreator.Controls.Add(this.cmdStack);
            this.tpStackCreator.Location = new System.Drawing.Point(4, 22);
            this.tpStackCreator.Name = "tpStackCreator";
            this.tpStackCreator.Padding = new System.Windows.Forms.Padding(3);
            this.tpStackCreator.Size = new System.Drawing.Size(741, 36);
            this.tpStackCreator.TabIndex = 2;
            this.tpStackCreator.Text = "StackCreator";
            this.tpStackCreator.UseVisualStyleBackColor = true;
            // 
            // tpBlobRepositioning2
            // 
            this.tpBlobRepositioning2.Controls.Add(this.cmdBlobRepositioning2);
            this.tpBlobRepositioning2.Location = new System.Drawing.Point(4, 22);
            this.tpBlobRepositioning2.Name = "tpBlobRepositioning2";
            this.tpBlobRepositioning2.Padding = new System.Windows.Forms.Padding(3);
            this.tpBlobRepositioning2.Size = new System.Drawing.Size(741, 36);
            this.tpBlobRepositioning2.TabIndex = 3;
            this.tpBlobRepositioning2.Text = "BlobRepositioning2";
            this.tpBlobRepositioning2.UseVisualStyleBackColor = true;
            // 
            // tpBlobExplorer
            // 
            this.tpBlobExplorer.Controls.Add(this.cmdBlobExplorer);
            this.tpBlobExplorer.Location = new System.Drawing.Point(4, 22);
            this.tpBlobExplorer.Name = "tpBlobExplorer";
            this.tpBlobExplorer.Padding = new System.Windows.Forms.Padding(3);
            this.tpBlobExplorer.Size = new System.Drawing.Size(741, 36);
            this.tpBlobExplorer.TabIndex = 4;
            this.tpBlobExplorer.Text = "BlobExplorer";
            this.tpBlobExplorer.UseVisualStyleBackColor = true;
            // 
            // cmdBlobExplorer
            // 
            this.cmdBlobExplorer.Location = new System.Drawing.Point(6, 6);
            this.cmdBlobExplorer.Name = "cmdBlobExplorer";
            this.cmdBlobExplorer.Size = new System.Drawing.Size(55, 23);
            this.cmdBlobExplorer.TabIndex = 9;
            this.cmdBlobExplorer.Text = "Blob";
            this.cmdBlobExplorer.UseVisualStyleBackColor = true;
            this.cmdBlobExplorer.Click += new System.EventHandler(this.cmdBlobExplorer_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bROI);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(741, 36);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "RegionOfInterest ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bROI
            // 
            this.bROI.Location = new System.Drawing.Point(6, 6);
            this.bROI.Name = "bROI";
            this.bROI.Size = new System.Drawing.Size(185, 23);
            this.bROI.TabIndex = 9;
            this.bROI.Text = "test ROI on filter [filterEnvelope]";
            this.bROI.UseVisualStyleBackColor = true;
            this.bROI.Click += new System.EventHandler(this.bROI_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.bAnalyseImage);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(741, 36);
            this.tabPage2.TabIndex = 6;
            this.tabPage2.Text = "analyse image";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bAnalyseImage
            // 
            this.bAnalyseImage.Location = new System.Drawing.Point(6, 7);
            this.bAnalyseImage.Name = "bAnalyseImage";
            this.bAnalyseImage.Size = new System.Drawing.Size(246, 23);
            this.bAnalyseImage.TabIndex = 10;
            this.bAnalyseImage.Text = "search the intensity min and max of the image";
            this.bAnalyseImage.UseVisualStyleBackColor = true;
            this.bAnalyseImage.Click += new System.EventHandler(this.bAnalyseImage_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.bCreateDrawImage);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(741, 36);
            this.tabPage3.TabIndex = 7;
            this.tabPage3.Text = "create image";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // bCreateDrawImage
            // 
            this.bCreateDrawImage.Location = new System.Drawing.Point(6, 6);
            this.bCreateDrawImage.Name = "bCreateDrawImage";
            this.bCreateDrawImage.Size = new System.Drawing.Size(142, 23);
            this.bCreateDrawImage.TabIndex = 9;
            this.bCreateDrawImage.Text = "create/draw an image";
            this.bCreateDrawImage.UseVisualStyleBackColor = true;
            this.bCreateDrawImage.Click += new System.EventHandler(this.bCreateDrawImage_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.bConvolutionPersonal);
            this.tabPage4.Controls.Add(this.bConvolution);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(741, 36);
            this.tabPage4.TabIndex = 8;
            this.tabPage4.Text = "Convolution";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // bConvolutionPersonal
            // 
            this.bConvolutionPersonal.Location = new System.Drawing.Point(86, 6);
            this.bConvolutionPersonal.Name = "bConvolutionPersonal";
            this.bConvolutionPersonal.Size = new System.Drawing.Size(74, 23);
            this.bConvolutionPersonal.TabIndex = 11;
            this.bConvolutionPersonal.Text = "personal";
            this.bConvolutionPersonal.UseVisualStyleBackColor = true;
            this.bConvolutionPersonal.Click += new System.EventHandler(this.bConvolutionPersonal_Click);
            // 
            // bConvolution
            // 
            this.bConvolution.Location = new System.Drawing.Point(6, 6);
            this.bConvolution.Name = "bConvolution";
            this.bConvolution.Size = new System.Drawing.Size(74, 23);
            this.bConvolution.TabIndex = 10;
            this.bConvolution.Text = "Laplace";
            this.bConvolution.UseVisualStyleBackColor = true;
            this.bConvolution.Click += new System.EventHandler(this.bConvolution_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "image size";
            // 
            // txtImageSize
            // 
            this.txtImageSize.Enabled = false;
            this.txtImageSize.Location = new System.Drawing.Point(76, 191);
            this.txtImageSize.Name = "txtImageSize";
            this.txtImageSize.Size = new System.Drawing.Size(192, 20);
            this.txtImageSize.TabIndex = 16;
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfo.FormattingEnabled = true;
            this.txtInfo.Location = new System.Drawing.Point(459, 217);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(298, 368);
            this.txtInfo.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(495, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Project Filters at http://filters.sourceforge.net ,  contact: filters@edurand.com" +
                "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 602);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.txtImageSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtFileNameToSave);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImageCount);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Filters - Tutorial C# 2";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpSobel.ResumeLayout(false);
            this.tpRotation.ResumeLayout(false);
            this.tpRotation.PerformLayout();
            this.tpStackCreator.ResumeLayout(false);
            this.tpBlobRepositioning2.ResumeLayout(false);
            this.tpBlobExplorer.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtImageCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileNameToSave;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdSobel;
        private System.Windows.Forms.Button cmdRotation;
        private System.Windows.Forms.Button cmdStack;
        private System.Windows.Forms.Button cmdBlobRepositioning2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSobel;
        private System.Windows.Forms.TabPage tpRotation;
        private System.Windows.Forms.TabPage tpStackCreator;
        private System.Windows.Forms.TabPage tpBlobRepositioning2;
        private System.Windows.Forms.TabPage tpBlobExplorer;
        private System.Windows.Forms.TextBox txtRotationAngle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImageSize;
        private System.Windows.Forms.Button cmdBlobExplorer;
        private System.Windows.Forms.ListBox txtInfo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button bROI;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button bAnalyseImage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button bCreateDrawImage;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button bConvolution;
        private System.Windows.Forms.Button bConvolutionPersonal;
        private System.Windows.Forms.Label label2;
    }
}

