using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Mitov.VideoLab;

namespace VLab_Contour_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void findContours1_Contours(object Sender, Mitov.VisionLab.ContoursEventArgs Args)
        {
            System.Diagnostics.Debug.WriteLine("blubb");
        }

        private void bildÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            try
            {
                pictureBox1.Image = new System.Drawing.Bitmap(openFileDialog1.FileName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void findContoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenericFilter genFilter = new GenericFilter(canny1);
            //genFilter.SendStartCommand((uint)pictureBox1.Image.Width, (uint)pictureBox1.Image.Height, 0);
            //Mitov.VideoLab.VideoBuffer buffer = new VideoBuffer(pictureBox1.Image.Width, pictureBox1.Image.Height, VideoFormat.RGB);
            //buffer.FromBitmap((System.Drawing.Bitmap)pictureBox1.Image);
            //genFilter.SendData(buffer);
            pictureBox1.Image = genFilter.ProcessBitmap((System.Drawing.Bitmap)pictureBox1.Image);
            
            pictureBox1.Invalidate();
            //genFilter.SendStopCommand();

            System.Diagnostics.Debug.WriteLine("canny done");
        }
    }
}
