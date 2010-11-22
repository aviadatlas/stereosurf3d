using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FiltersDllDotNet;

namespace TutorialCSharp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Filters.initialize();
            txtVersion.Text = Filters.getVersion();
            Int32 fMax = Filters.getFiltersCount();
            for (int f = 0; f < fMax; f++)
            {
                string filterName = Filters.getFiltersNameAtIndex(f);
                cbFiltersName.Items.Add(filterName);
            }
        }

        ~Form1()
        {
            Filters.unInitialize();
        }

        private void cbFiltersName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterName = (string)(cbFiltersName.SelectedItem);
            // create a instance of the selected Filter
            Int32 filter = Filters.createFilter(filterName);
            // for each parameters of this Filters
            lstParameters.Items.Clear();
            Int32 pMax = Filters.getParametersCount(filter);
            for (int p = 0; p < pMax; p++)
            {
                // we get the name and help of parameter
                StringBuilder parameterName = new StringBuilder(1024);
                StringBuilder parameterHelp = new StringBuilder(10240);
                Filters.getParameterName(filter, p, parameterName);
                Filters.getParameterHelp(filter, p, parameterHelp);
                string tmpStr = parameterName + " : " + parameterHelp;
                lstParameters.Items.Add(tmpStr);
            }
            // for each outputs of this Filters
            lstOutputs.Items.Clear();
            Int32 oMax = Filters.getOutputsCount(filter);
            for (int o = 0; o < oMax; o++)
            {
                // we get the name of output
                StringBuilder outputName = new StringBuilder(1024);
                Filters.getOutputName(filter, o, outputName);
                string tmpStr = outputName.ToString();
                lstOutputs.Items.Add(tmpStr);
            }
            // we delete this instance of the selected Filter
            Filters.deleteFilter(filter);
        }

    }
}