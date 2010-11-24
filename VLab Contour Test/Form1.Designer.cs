namespace VLab_Contour_Test
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            OpenWire.Proxy.SinkPin sinkPin12 = new OpenWire.Proxy.SinkPin();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            OpenWire.Proxy.SinkPin sinkPin13 = new OpenWire.Proxy.SinkPin();
            OpenWire.Proxy.SourcePin sourcePin8 = new OpenWire.Proxy.SourcePin();
            Mitov.VideoLab.OptionalRectRegion optionalRectRegion5 = new Mitov.VideoLab.OptionalRectRegion();
            Mitov.VisionLab.ContourAproximation contourAproximation4 = new Mitov.VisionLab.ContourAproximation();
            OpenWire.Proxy.SinkPin sinkPin14 = new OpenWire.Proxy.SinkPin();
            OpenWire.Proxy.SourcePin sourcePin9 = new OpenWire.Proxy.SourcePin();
            this.canny1 = new Mitov.VisionLab.Canny(this.components);
            this.findContours1 = new Mitov.VisionLab.FindContours(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bildÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findContoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.canny1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.findContours1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // canny1
            // 
            sinkPin12.ConnectionData = ((OpenWire.PinConnections)(resources.GetObject("sinkPin12.ConnectionData")));
            this.canny1.EnablePin = sinkPin12;
            this.canny1.HighThreshold = 90F;
            sinkPin13.ConnectionData = ((OpenWire.PinConnections)(resources.GetObject("sinkPin13.ConnectionData")));
            this.canny1.InputPin = sinkPin13;
            this.canny1.LowThreshold = 20F;
            sourcePin8.ConnectionData = ((OpenWire.PinConnections)(resources.GetObject("sourcePin8.ConnectionData")));
            this.canny1.OutputPin = sourcePin8;
            optionalRectRegion5.Height = ((ushort)(50));
            optionalRectRegion5.Left = ((ushort)(0));
            optionalRectRegion5.Top = ((ushort)(0));
            optionalRectRegion5.Width = ((ushort)(50));
            this.canny1.WorkArea = optionalRectRegion5;
            // 
            // findContours1
            // 
            contourAproximation4.Accuracy = 0;
            this.findContours1.Aproximation = contourAproximation4;
            sinkPin14.ConnectionData = ((OpenWire.PinConnections)(resources.GetObject("sinkPin14.ConnectionData")));
            this.findContours1.InputPin = sinkPin14;
            sourcePin9.ConnectionData = ((OpenWire.PinConnections)(resources.GetObject("sourcePin9.ConnectionData")));
            this.findContours1.OutputPin = sourcePin9;
            this.findContours1.Contours += new Mitov.VisionLab.ContoursEvent(this.findContours1_Contours);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(816, 408);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bildÖffnenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // bildÖffnenToolStripMenuItem
            // 
            this.bildÖffnenToolStripMenuItem.Name = "bildÖffnenToolStripMenuItem";
            this.bildÖffnenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bildÖffnenToolStripMenuItem.Text = "Bild öffnen";
            this.bildÖffnenToolStripMenuItem.Click += new System.EventHandler(this.bildÖffnenToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findContoursToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // findContoursToolStripMenuItem
            // 
            this.findContoursToolStripMenuItem.Name = "findContoursToolStripMenuItem";
            this.findContoursToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.findContoursToolStripMenuItem.Text = "Find Contours";
            this.findContoursToolStripMenuItem.Click += new System.EventHandler(this.findContoursToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 432);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canny1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.findContours1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mitov.VisionLab.Canny canny1;
        private Mitov.VisionLab.FindContours findContours1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bildÖffnenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findContoursToolStripMenuItem;
    }
}

