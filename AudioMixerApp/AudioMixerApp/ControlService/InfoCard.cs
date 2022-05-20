using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public void LoadWaveStream(String path, TimeSpan duration)
        {
            durationLabel.Text = FormatTimeSpan(duration);
            trackName.Text = Path.GetFileName(path);
            waveform.readWaveFile(path);
        }

        public void UpdateWaveForm(TimeSpan time, double progress)
        {
            timeLabel.Text = FormatTimeSpan(time);
            waveform.DrawWave(progress);
        }

        private static string FormatTimeSpan(TimeSpan time)
        {
            return time.ToString("mm':'ss':'ff");
        }
    }
}
