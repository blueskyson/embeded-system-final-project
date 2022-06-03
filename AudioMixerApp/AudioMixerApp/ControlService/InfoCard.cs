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
        public Deck deck { get; set; }

        public void setId(int id, Deck deck)
        {
            title.Text = "Track " + id;
            this.deck = deck;
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
            progressBar.Maximum = (int)duration.TotalMilliseconds;
            progressBar.Value = 0;
        }

        public void UpdateWaveForm(TimeSpan time, double progress)
        {
            timeLabel.Text = FormatTimeSpan(time);
            waveform.DrawWave(progress);
            progressBar.Value = (int)time.TotalMilliseconds;
        }

        private static string FormatTimeSpan(TimeSpan time)
        {
            return time.ToString("mm':'ss':'ff");
        }

        private bool isChangingPosition = false;

        private void progressBar_MouseDown(object sender, MouseEventArgs e)
        {
            isChangingPosition = true;
        }

        private void progressBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isChangingPosition)
                return;
            double pos = (double)e.X / progressBar.Width;
            pos = Math.Min(0.999, Math.Max(0.0, pos));
            deck.move(pos);
        }

        private void progressBar_MouseUp(object sender, MouseEventArgs e)
        {
            isChangingPosition = false;
        }
    }
}
