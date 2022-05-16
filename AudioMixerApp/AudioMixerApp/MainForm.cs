using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;

namespace AudioMixerApp
{
    public partial class mainForm : Form
    {

        public mainForm()
        {
            InitializeComponent();
            deck1.Id = 0;
            deck2.Id = 1;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
