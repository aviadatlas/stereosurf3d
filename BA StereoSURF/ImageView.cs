using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BA_StereoSURF
{
    public partial class ImageView : Form
    {
        private Form1 __root;
        private Bitmap _img;
        private string _name;

        public ImageView(Form1 root, Bitmap img, string name)
        {
            InitializeComponent();

            __root = root;
            _img = img;            
            adaptSize();
            pictureBox1.Image = _img;
            _name = name;
            string[] arrName = name.Split('.');
            arrName[arrName.Length-1] = "output";
            saveFileDialog1.FileName = String.Join("_", arrName);
        }

        private void adaptSize()
        {
            Rectangle rect = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
            foreach (Screen screen in Screen.AllScreens)
                rect = Rectangle.Union(rect, screen.Bounds);

            //Console.WriteLine("(width, height) = ({0}, {1})", rect.Width, rect.Height);

            if (_img.Width > rect.Width || _img.Height > rect.Height)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = new Size(_img.Width+16, _img.Height+62);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            adaptSize();
        }

        private void ImageView_Resize(object sender, EventArgs e)
        {
            string t = ""+this.Text.Split('(')[0];
            this.Text = String.Format("{0}({1},{2})", t, this.Size.Width, this.Size.Height);
            pictureBox1.Invalidate();
        }

        private void speichernUnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _img.Save(saveFileDialog1.FileName);
            }
        }
    }
}
