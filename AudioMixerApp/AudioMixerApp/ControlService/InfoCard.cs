using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;

namespace AudioMixerApp
{
    public partial class InfoCard : UserControl
    {
        private int _id;
        
        public void setId(int id)
        {
            _id = id;
            title.Text = "Track " + id;
        }
        
        public InfoCard()
        {
            InitializeComponent();
        }

        public void LoadWaveStream(String path)
        {
            waveform.WaveStream = new WaveFileReader(path);
        }

        public void UpdateWaveForm(TimeSpan time)
        {
            timeLabel.Text = time.ToString("mm':'ss':'ff");
            //waveform.DrawWave(time);
        }

        private static string FormatTimeSpan(TimeSpan time)
        {
            var timeStr = time.ToString("c");
            int index = timeStr.IndexOf('.');
            if (index > 0)
                timeStr = timeStr.Substring(0, index);
            return timeStr;
        }
    }
}
