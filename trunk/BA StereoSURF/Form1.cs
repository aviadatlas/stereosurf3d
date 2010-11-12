using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using OpenSURFcs;

namespace BA_StereoSURF
{
    public partial class Form1 : Form
    {
        private string _sourceFileName;
        private ExtendedImage _sourceFile;
        private OpenFileDialog _sourceOpenFileDialog;
        private ImageCorrelation _imageCorrelation;

        private IDictionary<string, ExtendedImage> _refFiles;
        private IDictionary<string, ExtendedImage> _refFilesHidden;
        private OpenFileDialog _refOpenFileDialog;
        private List<int> _refFilesHiddenIndices;

        private ProgressForm _currentProgressForm;

        public Form1()
        {
            InitializeComponent();
            

            _sourceOpenFileDialog = new OpenFileDialog();
            _sourceOpenFileDialog.Multiselect = false;
            _sourceOpenFileDialog.DefaultExt = "bmp";
            _sourceOpenFileDialog.Filter = "JPEG File (*.jpg)|*.jpg|Bitmap File (*.bmp)|*.bmp";

            _refOpenFileDialog = new OpenFileDialog();
            _refOpenFileDialog.Multiselect = true;
            _refOpenFileDialog.DefaultExt = "bmp";
            _refOpenFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|Bitmap Files (*.bmp)|*.bmp";

            _refFiles = new Dictionary<string, ExtendedImage>();
            _refFilesHidden = new Dictionary<string, ExtendedImage>();
            _refFilesHiddenIndices = new List<int>();
        }

        private void s1_b_select_Click(object sender, EventArgs e)
        {
            selectSourceImage();
        }
        private void selectSourceImage()
        {
            _sourceOpenFileDialog.ShowDialog();
            if (_sourceOpenFileDialog.CheckFileExists)
                _sourceFileName = _sourceOpenFileDialog.FileName;
            else
                _sourceFileName = "";

            if (_sourceFileName.Length > 0)
            {
                s1_l_file.Text = getFileNameWithoutPath(_sourceFileName);
                _sourceFile = new ExtendedImage(this, _sourceFileName);
                s1_pb_source.Image = _sourceFile.Image;
            }
            tab_double.Invalidate();
            tab_single.Invalidate();
        }

        private string getFileNameWithoutPath(string path)
        {
            string[] arrPath = path.Split('\\');
            return arrPath[arrPath.Length-1];
        }

        private void s2_b_select_Click(object sender, EventArgs e)
        {
            selectRefFiles();
        }
        private void selectRefFiles()
        {
            _refOpenFileDialog.ShowDialog();            
            foreach (string file_name in _refOpenFileDialog.FileNames)
            {
                if (!_refFiles.ContainsKey(getFileNameWithoutPath(file_name)))
                {
                    KeyValuePair<string, ExtendedImage> kvp = new KeyValuePair<string, ExtendedImage>(getFileNameWithoutPath(file_name), new ExtendedImage(this, file_name));
                    _refFiles.Add(kvp);                    
                }
            }
            refreshRefFilesList();
        }

        private void refreshRefFilesList()
        {
            s2_clb_images.Items.Clear();
            foreach (string file_name in _refFiles.Keys)
            {
                s2_clb_images.Items.Add(file_name);
                s2_clb_images.SetItemChecked(s2_clb_images.Items.Count-1, true);
            }            

            s2_clb_imagesHidden.Items.Clear();
            if (_refFilesHidden.Count > 0)
            {
                foreach (string file_name in _refFilesHidden.Keys)
                {
                    s2_clb_imagesHidden.Items.Add(file_name);
                }

                s2_clb_images.Height = 72;
                s2_clb_imagesHidden.Visible = true;
            }
            else
            {
                s2_clb_imagesHidden.Visible = false;
                s2_clb_images.Height = 126;
            }
            refreshRefFilesImage();
        }
        private void refreshRefFilesImage()
        {
            if (s2_clb_images.SelectedItems.Count == 0)
            {
                if (s2_clb_images.Items.Count > 0)
                {
                    s2_pb_selected.Image = _refFiles[s2_clb_images.Items[0].ToString()].Image;
                    s2_l_surfdots.Text = "SURFpts: " + _refFiles[s2_clb_images.Items[0].ToString()].InterestPointsSURF.Count.ToString(); 
                }
            }
            else
            {
                s2_pb_selected.Image = _refFiles[s2_clb_images.SelectedItems[0].ToString()].Image;
                s2_l_surfdots.Text = "SURFpts: " + _refFiles[s2_clb_images.SelectedItems[0].ToString()].InterestPointsSURF.Count.ToString(); 
            }
            tab_double.Invalidate();
            tab_single.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void s2_clb_images_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshRefFilesImage();
        }

        private void s2_clb_images_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _refFilesHiddenIndices = new List<int>();
            foreach (object item in s2_clb_images.Items)
            {
                if (!s2_clb_images.CheckedItems.Contains(item) ||
                    (s2_clb_images.Items.IndexOf(item) == e.Index && e.NewValue == CheckState.Unchecked))
                {
                    if (!(s2_clb_images.Items.IndexOf(item) == e.Index && e.NewValue == CheckState.Checked))
                        _refFilesHiddenIndices.Add(s2_clb_images.Items.IndexOf(item));
                }
            }
            s2_b_delete.Text = String.Format("({0}) Entfernen", _refFilesHiddenIndices.Count);
        }

        private void s2_b_delete_Click(object sender, EventArgs e)
        {
            if (_refFilesHiddenIndices.Count > 0)
            {
                foreach (int index in _refFilesHiddenIndices)
                {
                    _refFiles.Remove(s2_clb_images.Items[index].ToString());
                }
                refreshRefFilesList();
            }
        }

        private void tab_double_Paint(object sender, PaintEventArgs e)
        {
            if (_sourceFile!=null)
                t2_pb_source.Image = _sourceFile.ImageSurfed;

            if (_refFiles.Count > 0)
                t2_pb_ref.Image = _refFiles.ElementAt(0).Value.ImageSurfed;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tabControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_sourceFile != null && _refFiles.Count > 0 && _refFiles.ElementAt(0).Value != null)
            {
                StartProgress("Wende SURF-Algorithmus an", ProgressMode.Continuously, WatcherMode.PointsOfInterest, _refFiles.Last().Value);
                _sourceFile.calculatePointsOfInterest(InterestAlgorithm.SURF);
                foreach (KeyValuePair<string, ExtendedImage> kvp in _refFiles)
                {
                    kvp.Value.calculatePointsOfInterest(InterestAlgorithm.SURF);
                }
                //_refFiles.ElementAt(0).Value.calculatePointsOfInterest(InterestAlgorithm.SURF);
            }
            else
            {
                MessageBox.Show("Noch kein Ausgangs- und Bezugsbild gewählt!");
            }
            // callback for StopProgress();
            // StopProgress();
        }

        public void StartProgress(string text, ProgressMode mode, WatcherMode wmode, object watchObj)
        {
            _currentProgressForm = new ProgressForm(this, text, mode, wmode, watchObj);
            _currentProgressForm.Show();
        }
        public void StopProgress()
        {
            _currentProgressForm.Close();
        }
        
        public ProgressForm CurrentProgressForm
        {
            get { return _currentProgressForm; }
            set { _currentProgressForm = value; }
        }
        public decimal Pref_Surf_Threshold
        {
            get { return pref_surf_threshold.Value; }
        }
        public decimal Pref_Surf_Octaves
        {
            get { return pref_surf_octaves.Value; }
        }
        public decimal Pref_Surf_Samples
        {
            get { return pref_surf_samples.Value; }
        }

        private void s1_pb_source_Click(object sender, EventArgs e)
        {
            selectSourceImage();
        }

        private void s2_pb_selected_Click(object sender, EventArgs e)
        {
            if (s2_pb_selected.Image == null)
                selectRefFiles();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (t2_pb_mix.Image != null)
            {
                ImageView iv = new ImageView(this, (Bitmap)t2_pb_mix.Image, getFileNameWithoutPath(_sourceFileName));
                iv.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (t2_pb_source.Image != null)
            {
                ImageView iv = new ImageView(this, (Bitmap)t2_pb_source.Image, getFileNameWithoutPath(_sourceFileName));
                iv.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (t2_pb_ref.Image != null)
            {
                ImageView iv = new ImageView(this, (Bitmap)t2_pb_ref.Image, getFileNameWithoutPath(s2_clb_images.CheckedItems[0].ToString()));
                iv.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Genau":
                    pref_surf_threshold.Value = 0.0002m;
                    pref_surf_octaves.Value = 3m;
                    pref_surf_samples.Value = 3m;
                    break;
                case "Sehr Genau":
                    pref_surf_threshold.Value = 0.00010m;
                    pref_surf_octaves.Value = 3m;
                    pref_surf_samples.Value = 2m;
                    break;
                case "Extrem Genau":
                    pref_surf_threshold.Value = 0.00005m;
                    pref_surf_octaves.Value = 3m;
                    pref_surf_samples.Value = 1m;
                    break;
                case "Ausgeglichen":
                    pref_surf_threshold.Value = 0.0005m;
                    pref_surf_octaves.Value = 5m;
                    pref_surf_samples.Value = 4m;
                    break;
                case "Vereinfacht":
                    pref_surf_threshold.Value = 0.0025m;
                    pref_surf_octaves.Value = 5m;
                    pref_surf_samples.Value = 4m;
                    break;
                case "Stark Vereinfacht":
                    pref_surf_threshold.Value = 0.0075m;
                    pref_surf_octaves.Value = 5m;
                    pref_surf_samples.Value = 5m;
                    break;
            }
        }
        
        private void t2_cb_bildA_surf_CheckedChanged(object sender, EventArgs e)
        {
            _sourceFile.ApplySurfFilter = t2_cb_bildA_surf.Checked;
            t2_pb_source.Invalidate();
        }
        private void t2_cb_bildB_surf_CheckedChanged(object sender, EventArgs e)
        {
            _refFiles.ElementAt(0).Value.ApplySurfFilter = t2_cb_bildB_surf.Checked;
            t2_pb_ref.Invalidate();                
        }  
        private void t2_cb_bildA_outlines_CheckedChanged(object sender, EventArgs e)
        {
            _sourceFile.ApplyEdgeFilter = t2_cb_bildA_outlines.Checked;
            t2_pb_source.Invalidate();
        }
        private void t2_cb_bildB_outlines_CheckedChanged(object sender, EventArgs e)
        {
            _refFiles.ElementAt(0).Value.ApplyEdgeFilter = t2_cb_bildB_outlines.Checked;
            t2_pb_ref.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StartProgress("Comparison Detection", ProgressMode.Continuously, WatcherMode.None, null);
            _imageCorrelation = new ImageCorrelation(_sourceFile);
            _imageCorrelation.AddReferenceImages(new List<ExtendedImage>(_refFiles.Values));

            List<CorrelationInfo> correlationInfos = _imageCorrelation.GetCorrelationInfo(_refFiles.ElementAt(0).Value);

            Bitmap bmp = new Bitmap(_sourceFile.Image.Width + _refFiles.ElementAt(0).Value.Image.Width, _sourceFile.Image.Height);
                           
            Graphics g1 = Graphics.FromImage(t2_pb_source.Image);
            Graphics g2 = Graphics.FromImage(t2_pb_ref.Image);
            g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;            
            g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Random rand = new Random();
            foreach (CorrelationInfo ci in correlationInfos)
            {
                float m = (ci.Yb-ci.Ya) / ((t2_pb_source.Image.Width-ci.Xa)+(ci.Xb));
                float n = ci.Ya - (m * ci.Xa);
                
                Pen pen = new Pen(Color.FromArgb(63, Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255))));
                SolidBrush brush = new SolidBrush(Color.FromArgb(pen.Color.R, pen.Color.G, pen.Color.B));
                
                g1.DrawLine(pen, ci.Xa, ci.Ya, t2_pb_source.Image.Width - 1, m * t2_pb_source.Image.Width + n);
                g1.FillRectangle(brush, ci.Xa - 2, ci.Ya - 1, 5, 3);
                
                g2.DrawLine(pen, 0, m * (t2_pb_source.Image.Width - ci.Xa) + n, ci.Xb, ci.Yb);
                g2.FillRectangle(brush, ci.Xb - 2, ci.Yb - 1, 5, 3);
            }
            g1.Dispose();
            g2.Dispose();

            renderMixImage();
            StopProgress();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            renderMixImage();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            renderMixImage();
        }

        private void renderMixImage()
        {
            if (radioButton2.Checked)
            {
                Bitmap bmp = new Bitmap(t2_pb_source.Image.Width + t2_pb_ref.Image.Width, t2_pb_source.Image.Height);

                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(t2_pb_source.Image, 0.0f, 0.0f);
                g.DrawImage(t2_pb_ref.Image, (float)t2_pb_ref.Image.Width, 0.0f);
                g.Dispose();
                t2_pb_mix.Image = bmp;
            }
            else if (radioButton1.Checked)
            {
                Bitmap bmp = new Bitmap(t2_pb_source.Image.Width, t2_pb_source.Image.Height);

                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(t2_pb_source.Image, 0.0f, 0.0f);
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                Bitmap bmp2 = new Bitmap(t2_pb_ref.Image);
                bmp2 = (Bitmap)SetImgOpacity((Image)bmp2, 0.5f);
                g.DrawImage(bmp2, 0.0f, 0.0f);
                g.Dispose();

                t2_pb_mix.Image = bmp;
            }
        }



        public static Image SetImgOpacity(Image imgPic, float imgOpac)
        {
            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            ColorMatrix cmxPic = new ColorMatrix();
            cmxPic.Matrix33 = imgOpac;

            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new System.Drawing.Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();

            return bmpPic;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bmp = _imageCorrelation.GenerateDepthmap(true, false);
            if (bmp != null)
            {
                ImageView iv = new ImageView(this, bmp, getFileNameWithoutPath("depthmap_"+s2_clb_images.CheckedItems[0].ToString()));
                iv.Show();
            }
            else
            {
                MessageBox.Show("Tiefenbild konnte nicht errechnet werden!");
            }
        }

        
    }
}
