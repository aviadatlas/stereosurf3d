namespace BA_StereoSURF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.s1_gb = new System.Windows.Forms.GroupBox();
            this.s1_b_select = new System.Windows.Forms.Button();
            this.s1_l_file = new System.Windows.Forms.Label();
            this.s1_pb_source = new System.Windows.Forms.PictureBox();
            this.s2_gb = new System.Windows.Forms.GroupBox();
            this.s2_l_surfdots = new System.Windows.Forms.Label();
            this.s2_b_down = new System.Windows.Forms.Button();
            this.s2_b_up = new System.Windows.Forms.Button();
            this.s2_b_delete = new System.Windows.Forms.Button();
            this.s2_b_select = new System.Windows.Forms.Button();
            this.s2_clb_images = new System.Windows.Forms.CheckedListBox();
            this.s2_pb_selected = new System.Windows.Forms.PictureBox();
            this.s2_clb_imagesHidden = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_double = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.t2_cb_bildB_background = new System.Windows.Forms.CheckBox();
            this.t2_cb_bildB_outlines = new System.Windows.Forms.CheckBox();
            this.t2_cb_bildB_surf = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.t2_cb_vectors = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.t2_cb_bildA_background = new System.Windows.Forms.CheckBox();
            this.t2_cb_bildA_outlines = new System.Windows.Forms.CheckBox();
            this.t2_cb_bildA_surf = new System.Windows.Forms.CheckBox();
            this.t2_pb_mix = new System.Windows.Forms.PictureBox();
            this.t2_pb_ref = new System.Windows.Forms.PictureBox();
            this.t2_pb_source = new System.Windows.Forms.PictureBox();
            this.tab_single = new System.Windows.Forms.TabPage();
            this.t1_pb_output = new System.Windows.Forms.PictureBox();
            this.tab_prefs = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pref_surf_samples = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.pref_surf_octaves = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pref_surf_threshold = new System.Windows.Forms.NumericUpDown();
            this.tab_debug = new System.Windows.Forms.TabPage();
            this.rtb_debug = new System.Windows.Forms.RichTextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button6 = new System.Windows.Forms.Button();
            this.s1_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s1_pb_source)).BeginInit();
            this.s2_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s2_pb_selected)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_double.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t2_pb_mix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t2_pb_ref)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t2_pb_source)).BeginInit();
            this.tab_single.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t1_pb_output)).BeginInit();
            this.tab_prefs.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pref_surf_samples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pref_surf_octaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pref_surf_threshold)).BeginInit();
            this.tab_debug.SuspendLayout();
            this.SuspendLayout();
            // 
            // s1_gb
            // 
            this.s1_gb.Controls.Add(this.s1_b_select);
            this.s1_gb.Controls.Add(this.s1_l_file);
            this.s1_gb.Controls.Add(this.s1_pb_source);
            this.s1_gb.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s1_gb.Location = new System.Drawing.Point(13, 13);
            this.s1_gb.Margin = new System.Windows.Forms.Padding(4);
            this.s1_gb.Name = "s1_gb";
            this.s1_gb.Padding = new System.Windows.Forms.Padding(4);
            this.s1_gb.Size = new System.Drawing.Size(239, 216);
            this.s1_gb.TabIndex = 0;
            this.s1_gb.TabStop = false;
            this.s1_gb.Text = "Schritt 1 - Ausgangsbild";
            // 
            // s1_b_select
            // 
            this.s1_b_select.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s1_b_select.Location = new System.Drawing.Point(151, 187);
            this.s1_b_select.Name = "s1_b_select";
            this.s1_b_select.Size = new System.Drawing.Size(82, 24);
            this.s1_b_select.TabIndex = 2;
            this.s1_b_select.Text = "Auswahl";
            this.s1_b_select.UseVisualStyleBackColor = true;
            this.s1_b_select.Click += new System.EventHandler(this.s1_b_select_Click);
            // 
            // s1_l_file
            // 
            this.s1_l_file.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s1_l_file.Location = new System.Drawing.Point(7, 184);
            this.s1_l_file.Name = "s1_l_file";
            this.s1_l_file.Size = new System.Drawing.Size(143, 27);
            this.s1_l_file.TabIndex = 1;
            // 
            // s1_pb_source
            // 
            this.s1_pb_source.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.s1_pb_source.Location = new System.Drawing.Point(7, 21);
            this.s1_pb_source.Name = "s1_pb_source";
            this.s1_pb_source.Size = new System.Drawing.Size(225, 163);
            this.s1_pb_source.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.s1_pb_source.TabIndex = 0;
            this.s1_pb_source.TabStop = false;
            this.s1_pb_source.Click += new System.EventHandler(this.s1_pb_source_Click);
            // 
            // s2_gb
            // 
            this.s2_gb.Controls.Add(this.s2_l_surfdots);
            this.s2_gb.Controls.Add(this.s2_b_down);
            this.s2_gb.Controls.Add(this.s2_b_up);
            this.s2_gb.Controls.Add(this.s2_b_delete);
            this.s2_gb.Controls.Add(this.s2_b_select);
            this.s2_gb.Controls.Add(this.s2_clb_images);
            this.s2_gb.Controls.Add(this.s2_pb_selected);
            this.s2_gb.Controls.Add(this.s2_clb_imagesHidden);
            this.s2_gb.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_gb.Location = new System.Drawing.Point(12, 233);
            this.s2_gb.Name = "s2_gb";
            this.s2_gb.Size = new System.Drawing.Size(240, 341);
            this.s2_gb.TabIndex = 1;
            this.s2_gb.TabStop = false;
            this.s2_gb.Text = "Schritt 2 - Referenzbilder";
            // 
            // s2_l_surfdots
            // 
            this.s2_l_surfdots.AutoSize = true;
            this.s2_l_surfdots.BackColor = System.Drawing.Color.DimGray;
            this.s2_l_surfdots.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_l_surfdots.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.s2_l_surfdots.Location = new System.Drawing.Point(12, 162);
            this.s2_l_surfdots.Name = "s2_l_surfdots";
            this.s2_l_surfdots.Size = new System.Drawing.Size(0, 13);
            this.s2_l_surfdots.TabIndex = 8;
            this.s2_l_surfdots.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // s2_b_down
            // 
            this.s2_b_down.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_b_down.Image = global::BA_StereoSURF.Properties.Resources.arrow_down;
            this.s2_b_down.Location = new System.Drawing.Point(129, 312);
            this.s2_b_down.Name = "s2_b_down";
            this.s2_b_down.Size = new System.Drawing.Size(22, 24);
            this.s2_b_down.TabIndex = 6;
            this.s2_b_down.UseVisualStyleBackColor = true;
            // 
            // s2_b_up
            // 
            this.s2_b_up.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_b_up.Image = global::BA_StereoSURF.Properties.Resources.arrow_up;
            this.s2_b_up.Location = new System.Drawing.Point(108, 312);
            this.s2_b_up.Name = "s2_b_up";
            this.s2_b_up.Size = new System.Drawing.Size(22, 24);
            this.s2_b_up.TabIndex = 5;
            this.s2_b_up.UseVisualStyleBackColor = true;
            // 
            // s2_b_delete
            // 
            this.s2_b_delete.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_b_delete.Location = new System.Drawing.Point(8, 312);
            this.s2_b_delete.Name = "s2_b_delete";
            this.s2_b_delete.Size = new System.Drawing.Size(99, 24);
            this.s2_b_delete.TabIndex = 4;
            this.s2_b_delete.Text = "(0) Entfernen";
            this.s2_b_delete.UseVisualStyleBackColor = true;
            this.s2_b_delete.Click += new System.EventHandler(this.s2_b_delete_Click);
            // 
            // s2_b_select
            // 
            this.s2_b_select.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_b_select.Location = new System.Drawing.Point(152, 312);
            this.s2_b_select.Name = "s2_b_select";
            this.s2_b_select.Size = new System.Drawing.Size(82, 24);
            this.s2_b_select.TabIndex = 3;
            this.s2_b_select.Text = "Hinzufügen";
            this.s2_b_select.UseVisualStyleBackColor = true;
            this.s2_b_select.Click += new System.EventHandler(this.s2_b_select_Click);
            // 
            // s2_clb_images
            // 
            this.s2_clb_images.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_clb_images.FormattingEnabled = true;
            this.s2_clb_images.IntegralHeight = false;
            this.s2_clb_images.Location = new System.Drawing.Point(8, 183);
            this.s2_clb_images.Name = "s2_clb_images";
            this.s2_clb_images.Size = new System.Drawing.Size(225, 126);
            this.s2_clb_images.TabIndex = 2;
            this.s2_clb_images.SelectedIndexChanged += new System.EventHandler(this.s2_clb_images_SelectedIndexChanged);
            this.s2_clb_images.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.s2_clb_images_ItemCheck);
            // 
            // s2_pb_selected
            // 
            this.s2_pb_selected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.s2_pb_selected.Location = new System.Drawing.Point(8, 21);
            this.s2_pb_selected.Name = "s2_pb_selected";
            this.s2_pb_selected.Size = new System.Drawing.Size(225, 163);
            this.s2_pb_selected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.s2_pb_selected.TabIndex = 1;
            this.s2_pb_selected.TabStop = false;
            this.s2_pb_selected.Click += new System.EventHandler(this.s2_pb_selected_Click);
            // 
            // s2_clb_imagesHidden
            // 
            this.s2_clb_imagesHidden.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2_clb_imagesHidden.FormattingEnabled = true;
            this.s2_clb_imagesHidden.Location = new System.Drawing.Point(8, 254);
            this.s2_clb_imagesHidden.Name = "s2_clb_imagesHidden";
            this.s2_clb_imagesHidden.Size = new System.Drawing.Size(225, 55);
            this.s2_clb_imagesHidden.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 577);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 173);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Schritt 3 - Methoden";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(7, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "3.1 SURF-Algorithmus";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tab_double);
            this.tabControl1.Controls.Add(this.tab_single);
            this.tabControl1.Controls.Add(this.tab_prefs);
            this.tabControl1.Controls.Add(this.tab_debug);
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(268, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(984, 737);
            this.tabControl1.TabIndex = 3;
            // 
            // tab_double
            // 
            this.tab_double.Controls.Add(this.button4);
            this.tab_double.Controls.Add(this.button3);
            this.tab_double.Controls.Add(this.button2);
            this.tab_double.Controls.Add(this.groupBox4);
            this.tab_double.Controls.Add(this.groupBox2);
            this.tab_double.Controls.Add(this.label2);
            this.tab_double.Controls.Add(this.label1);
            this.tab_double.Controls.Add(this.groupBox3);
            this.tab_double.Controls.Add(this.t2_pb_mix);
            this.tab_double.Controls.Add(this.t2_pb_ref);
            this.tab_double.Controls.Add(this.t2_pb_source);
            this.tab_double.Location = new System.Drawing.Point(4, 27);
            this.tab_double.Name = "tab_double";
            this.tab_double.Padding = new System.Windows.Forms.Padding(3);
            this.tab_double.Size = new System.Drawing.Size(976, 706);
            this.tab_double.TabIndex = 1;
            this.tab_double.Text = "Doppel-Ansicht";
            this.tab_double.UseVisualStyleBackColor = true;
            this.tab_double.Paint += new System.Windows.Forms.PaintEventHandler(this.tab_double_Paint);
            // 
            // button4
            // 
            this.button4.Image = global::BA_StereoSURF.Properties.Resources.map_magnify;
            this.button4.Location = new System.Drawing.Point(919, 320);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(37, 37);
            this.button4.TabIndex = 11;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Image = global::BA_StereoSURF.Properties.Resources.map_magnify;
            this.button3.Location = new System.Drawing.Point(443, 320);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(37, 37);
            this.button3.TabIndex = 10;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = global::BA_StereoSURF.Properties.Resources.map_magnify;
            this.button2.Location = new System.Drawing.Point(729, 662);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 37);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.t2_cb_bildB_background);
            this.groupBox4.Controls.Add(this.t2_cb_bildB_outlines);
            this.groupBox4.Controls.Add(this.t2_cb_bildB_surf);
            this.groupBox4.Location = new System.Drawing.Point(729, 363);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(232, 146);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bild B";
            // 
            // t2_cb_bildB_background
            // 
            this.t2_cb_bildB_background.AutoSize = true;
            this.t2_cb_bildB_background.Location = new System.Drawing.Point(6, 81);
            this.t2_cb_bildB_background.Name = "t2_cb_bildB_background";
            this.t2_cb_bildB_background.Size = new System.Drawing.Size(127, 22);
            this.t2_cb_bildB_background.TabIndex = 4;
            this.t2_cb_bildB_background.Text = "Bild ausblenden";
            this.t2_cb_bildB_background.UseVisualStyleBackColor = true;
            // 
            // t2_cb_bildB_outlines
            // 
            this.t2_cb_bildB_outlines.AutoSize = true;
            this.t2_cb_bildB_outlines.Location = new System.Drawing.Point(6, 53);
            this.t2_cb_bildB_outlines.Name = "t2_cb_bildB_outlines";
            this.t2_cb_bildB_outlines.Size = new System.Drawing.Size(138, 22);
            this.t2_cb_bildB_outlines.TabIndex = 3;
            this.t2_cb_bildB_outlines.Text = "Kantenerkennung";
            this.t2_cb_bildB_outlines.UseVisualStyleBackColor = true;
            this.t2_cb_bildB_outlines.CheckedChanged += new System.EventHandler(this.t2_cb_bildB_outlines_CheckedChanged);
            // 
            // t2_cb_bildB_surf
            // 
            this.t2_cb_bildB_surf.AutoSize = true;
            this.t2_cb_bildB_surf.Location = new System.Drawing.Point(6, 25);
            this.t2_cb_bildB_surf.Name = "t2_cb_bildB_surf";
            this.t2_cb_bildB_surf.Size = new System.Drawing.Size(149, 22);
            this.t2_cb_bildB_surf.TabIndex = 2;
            this.t2_cb_bildB_surf.Text = "SURF Dots anzeigen";
            this.t2_cb_bildB_surf.UseVisualStyleBackColor = true;
            this.t2_cb_bildB_surf.CheckedChanged += new System.EventHandler(this.t2_cb_bildB_surf_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.t2_cb_vectors);
            this.groupBox2.Location = new System.Drawing.Point(729, 515);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 141);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mischbild";
            // 
            // t2_cb_vectors
            // 
            this.t2_cb_vectors.AutoSize = true;
            this.t2_cb_vectors.Location = new System.Drawing.Point(6, 21);
            this.t2_cb_vectors.Name = "t2_cb_vectors";
            this.t2_cb_vectors.Size = new System.Drawing.Size(179, 22);
            this.t2_cb_vectors.TabIndex = 5;
            this.t2_cb_vectors.Text = "Vektorlinien einblenden";
            this.t2_cb_vectors.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(680, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bezugsbild (erstes)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ausgangsbild";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.t2_cb_bildA_background);
            this.groupBox3.Controls.Add(this.t2_cb_bildA_outlines);
            this.groupBox3.Controls.Add(this.t2_cb_bildA_surf);
            this.groupBox3.Location = new System.Drawing.Point(10, 363);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 146);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bild A";
            // 
            // t2_cb_bildA_background
            // 
            this.t2_cb_bildA_background.AutoSize = true;
            this.t2_cb_bildA_background.Location = new System.Drawing.Point(6, 81);
            this.t2_cb_bildA_background.Name = "t2_cb_bildA_background";
            this.t2_cb_bildA_background.Size = new System.Drawing.Size(127, 22);
            this.t2_cb_bildA_background.TabIndex = 4;
            this.t2_cb_bildA_background.Text = "Bild ausblenden";
            this.t2_cb_bildA_background.UseVisualStyleBackColor = true;
            // 
            // t2_cb_bildA_outlines
            // 
            this.t2_cb_bildA_outlines.AutoSize = true;
            this.t2_cb_bildA_outlines.Location = new System.Drawing.Point(6, 53);
            this.t2_cb_bildA_outlines.Name = "t2_cb_bildA_outlines";
            this.t2_cb_bildA_outlines.Size = new System.Drawing.Size(138, 22);
            this.t2_cb_bildA_outlines.TabIndex = 3;
            this.t2_cb_bildA_outlines.Text = "Kantenerkennung";
            this.t2_cb_bildA_outlines.UseVisualStyleBackColor = true;
            this.t2_cb_bildA_outlines.CheckedChanged += new System.EventHandler(this.t2_cb_bildA_outlines_CheckedChanged);
            // 
            // t2_cb_bildA_surf
            // 
            this.t2_cb_bildA_surf.AutoSize = true;
            this.t2_cb_bildA_surf.Location = new System.Drawing.Point(6, 25);
            this.t2_cb_bildA_surf.Name = "t2_cb_bildA_surf";
            this.t2_cb_bildA_surf.Size = new System.Drawing.Size(149, 22);
            this.t2_cb_bildA_surf.TabIndex = 2;
            this.t2_cb_bildA_surf.Text = "SURF Dots anzeigen";
            this.t2_cb_bildA_surf.UseVisualStyleBackColor = true;
            this.t2_cb_bildA_surf.CheckedChanged += new System.EventHandler(this.t2_cb_bildA_surf_CheckedChanged);
            // 
            // t2_pb_mix
            // 
            this.t2_pb_mix.BackColor = System.Drawing.Color.Gainsboro;
            this.t2_pb_mix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.t2_pb_mix.Location = new System.Drawing.Point(248, 363);
            this.t2_pb_mix.Name = "t2_pb_mix";
            this.t2_pb_mix.Size = new System.Drawing.Size(475, 336);
            this.t2_pb_mix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.t2_pb_mix.TabIndex = 5;
            this.t2_pb_mix.TabStop = false;
            // 
            // t2_pb_ref
            // 
            this.t2_pb_ref.BackColor = System.Drawing.Color.Gainsboro;
            this.t2_pb_ref.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.t2_pb_ref.Location = new System.Drawing.Point(486, 26);
            this.t2_pb_ref.Name = "t2_pb_ref";
            this.t2_pb_ref.Size = new System.Drawing.Size(475, 336);
            this.t2_pb_ref.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.t2_pb_ref.TabIndex = 3;
            this.t2_pb_ref.TabStop = false;
            // 
            // t2_pb_source
            // 
            this.t2_pb_source.BackColor = System.Drawing.Color.Gainsboro;
            this.t2_pb_source.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.t2_pb_source.Location = new System.Drawing.Point(10, 26);
            this.t2_pb_source.Name = "t2_pb_source";
            this.t2_pb_source.Size = new System.Drawing.Size(475, 336);
            this.t2_pb_source.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.t2_pb_source.TabIndex = 2;
            this.t2_pb_source.TabStop = false;
            // 
            // tab_single
            // 
            this.tab_single.Controls.Add(this.t1_pb_output);
            this.tab_single.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_single.Location = new System.Drawing.Point(4, 27);
            this.tab_single.Name = "tab_single";
            this.tab_single.Padding = new System.Windows.Forms.Padding(3);
            this.tab_single.Size = new System.Drawing.Size(976, 706);
            this.tab_single.TabIndex = 0;
            this.tab_single.Text = "Output";
            this.tab_single.UseVisualStyleBackColor = true;
            // 
            // t1_pb_output
            // 
            this.t1_pb_output.BackColor = System.Drawing.Color.Gainsboro;
            this.t1_pb_output.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.t1_pb_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.t1_pb_output.Location = new System.Drawing.Point(3, 27);
            this.t1_pb_output.Name = "t1_pb_output";
            this.t1_pb_output.Size = new System.Drawing.Size(968, 654);
            this.t1_pb_output.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.t1_pb_output.TabIndex = 0;
            this.t1_pb_output.TabStop = false;
            // 
            // tab_prefs
            // 
            this.tab_prefs.Controls.Add(this.groupBox5);
            this.tab_prefs.Location = new System.Drawing.Point(4, 27);
            this.tab_prefs.Name = "tab_prefs";
            this.tab_prefs.Size = new System.Drawing.Size(976, 706);
            this.tab_prefs.TabIndex = 2;
            this.tab_prefs.Text = "Einstellungen";
            this.tab_prefs.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.pref_surf_samples);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.pref_surf_octaves);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.pref_surf_threshold);
            this.groupBox5.Location = new System.Drawing.Point(12, 14);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(144, 191);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SURF-Algorithmus";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Extrem Genau",
            "Sehr Genau",
            "Genau",
            "Ausgeglichen",
            "Vereinfacht",
            "Stark Vereinfacht"});
            this.comboBox1.Location = new System.Drawing.Point(8, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(130, 26);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Text = "Sehr Genau";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Samples (min)";
            // 
            // pref_surf_samples
            // 
            this.pref_surf_samples.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pref_surf_samples.Location = new System.Drawing.Point(8, 154);
            this.pref_surf_samples.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pref_surf_samples.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pref_surf_samples.Name = "pref_surf_samples";
            this.pref_surf_samples.ReadOnly = true;
            this.pref_surf_samples.Size = new System.Drawing.Size(130, 26);
            this.pref_surf_samples.TabIndex = 4;
            this.pref_surf_samples.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Octave";
            // 
            // pref_surf_octaves
            // 
            this.pref_surf_octaves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pref_surf_octaves.Location = new System.Drawing.Point(8, 114);
            this.pref_surf_octaves.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.pref_surf_octaves.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pref_surf_octaves.Name = "pref_surf_octaves";
            this.pref_surf_octaves.ReadOnly = true;
            this.pref_surf_octaves.Size = new System.Drawing.Size(130, 26);
            this.pref_surf_octaves.TabIndex = 2;
            this.pref_surf_octaves.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Threshold";
            // 
            // pref_surf_threshold
            // 
            this.pref_surf_threshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pref_surf_threshold.DecimalPlaces = 5;
            this.pref_surf_threshold.Increment = new decimal(new int[] {
            5,
            0,
            0,
            327680});
            this.pref_surf_threshold.Location = new System.Drawing.Point(8, 74);
            this.pref_surf_threshold.Name = "pref_surf_threshold";
            this.pref_surf_threshold.ReadOnly = true;
            this.pref_surf_threshold.Size = new System.Drawing.Size(130, 26);
            this.pref_surf_threshold.TabIndex = 0;
            this.pref_surf_threshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            // 
            // tab_debug
            // 
            this.tab_debug.Controls.Add(this.rtb_debug);
            this.tab_debug.Location = new System.Drawing.Point(4, 27);
            this.tab_debug.Name = "tab_debug";
            this.tab_debug.Size = new System.Drawing.Size(976, 706);
            this.tab_debug.TabIndex = 3;
            this.tab_debug.Text = "Debug";
            this.tab_debug.UseVisualStyleBackColor = true;
            // 
            // rtb_debug
            // 
            this.rtb_debug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_debug.Location = new System.Drawing.Point(0, 0);
            this.rtb_debug.Name = "rtb_debug";
            this.rtb_debug.Size = new System.Drawing.Size(976, 706);
            this.rtb_debug.TabIndex = 0;
            this.rtb_debug.Text = "";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(7, 55);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(225, 28);
            this.button5.TabIndex = 1;
            this.button5.Text = "3.2 Korrelation erzeugen";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(6, 46);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(108, 22);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Überblenden";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(6, 70);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(124, 22);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Nebeneinander";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(7, 91);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(225, 28);
            this.button6.TabIndex = 2;
            this.button6.Text = "3.3 Depthmap generieren";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1264, 762);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.s2_gb);
            this.Controls.Add(this.s1_gb);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FHE Bachelor - StereoSURF 2D";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.s1_gb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.s1_pb_source)).EndInit();
            this.s2_gb.ResumeLayout(false);
            this.s2_gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s2_pb_selected)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tab_double.ResumeLayout(false);
            this.tab_double.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t2_pb_mix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t2_pb_ref)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t2_pb_source)).EndInit();
            this.tab_single.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.t1_pb_output)).EndInit();
            this.tab_prefs.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pref_surf_samples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pref_surf_octaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pref_surf_threshold)).EndInit();
            this.tab_debug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox s1_gb;
        private System.Windows.Forms.Button s1_b_select;
        private System.Windows.Forms.Label s1_l_file;
        private System.Windows.Forms.PictureBox s1_pb_source;
        private System.Windows.Forms.GroupBox s2_gb;
        private System.Windows.Forms.PictureBox s2_pb_selected;
        private System.Windows.Forms.CheckedListBox s2_clb_images;
        private System.Windows.Forms.Button s2_b_select;
        private System.Windows.Forms.Button s2_b_delete;
        private System.Windows.Forms.Button s2_b_up;
        private System.Windows.Forms.Button s2_b_down;
        private System.Windows.Forms.CheckedListBox s2_clb_imagesHidden;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tab_single;
        private System.Windows.Forms.TabPage tab_double;
        private System.Windows.Forms.TabPage tab_prefs;
        private System.Windows.Forms.PictureBox t1_pb_output;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox t2_pb_source;
        private System.Windows.Forms.TabPage tab_debug;
        private System.Windows.Forms.RichTextBox rtb_debug;
        private System.Windows.Forms.PictureBox t2_pb_ref;
        private System.Windows.Forms.PictureBox t2_pb_mix;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox t2_cb_bildA_surf;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox t2_cb_bildB_background;
        private System.Windows.Forms.CheckBox t2_cb_bildB_outlines;
        private System.Windows.Forms.CheckBox t2_cb_bildB_surf;
        private System.Windows.Forms.CheckBox t2_cb_vectors;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox t2_cb_bildA_background;
        private System.Windows.Forms.CheckBox t2_cb_bildA_outlines;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown pref_surf_threshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown pref_surf_samples;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown pref_surf_octaves;
        private System.Windows.Forms.Label s2_l_surfdots;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button6;
    }
}

