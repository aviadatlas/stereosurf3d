using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BA_StereoSURF
{
    public enum ProgressMode { Continuously, Progress };
    public enum WatcherMode { PointsOfInterest, None };

    public partial class ProgressForm : Form
    {
        private string _text;
        private string _newText;
        private ProgressMode _mode;
        private object _watchObj;
        private WatcherMode _wmode;

        Form1 __root;

        public ProgressForm(Form1 root, string text, ProgressMode mode, WatcherMode wmode, object watchObj)
        {
            InitializeComponent();

            _text = text;
            _newText = text;
            label1.Text = _text;
            this.Text = _text;
            _mode = mode;
            __root = root;
            __root.Cursor = Cursors.AppStarting;
            __root.Enabled = false;
            _wmode = wmode;
            _watchObj = watchObj;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_mode == ProgressMode.Continuously)
            {
                if (progressBar1.Value == progressBar1.Maximum)
                    progressBar1.Value = 0;
                else
                    progressBar1.Value++;
            }
            label2.Text = progressBar1.Value.ToString();

            if (_text != _newText)
            {
                _text = _newText;
                label1.Text = _text;
                this.Text = _text;
            }

            switch (_wmode)
            {
                case WatcherMode.PointsOfInterest:
                    if (((ExtendedImage)_watchObj).InterestPointsSURF.Count > 0)
                        this.Close();
                    break;
            }            
        }

        /// <summary>
        /// Gets/Sets current value of progress
        /// </summary>
        public int Current
        {
            get { return progressBar1.Value; }
            set { progressBar1.Value = value; }
        }
        /// <summary>
        /// Gets/Sets Maximum of progress-value (default=100)
        /// </summary>
        public int Maximum
        {
            get { return progressBar1.Maximum; }
            set { progressBar1.Maximum = value; }
        }
        /// <summary>
        /// Gets/Sets Minimum of progress-value (default=0)
        /// </summary>
        public int Minimum
        {
            get { return progressBar1.Minimum; }
            set { progressBar1.Minimum = value; }
        }

        public string StatusText
        {
            get { return _text; }
            set { _newText = value; }
        }

        private void ProgressForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void ProgressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            __root.Cursor = Cursors.Default;
            __root.Enabled = true;
            __root.Focus();
        }

    }
}
