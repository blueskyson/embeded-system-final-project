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
    public partial class Deck : UserControl
    {
        private WaveOutEvent outputDevice = null;
        private AudioFileReader audioFile = null;

        public Deck()
        {
            InitializeComponent();
        }

        private void volumnTrackbar_Scroll(object sender, EventArgs e)
        {

        }

        private void Deck_Load(object sender, EventArgs e)
        {

        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select audio file";
            dialog.Filter = "mp3 (*.mp3)|*.mp3|wav (*.wav)|*.wav";
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK) {
                audioFile = new AudioFileReader(dialog.FileName);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
            } else if (res == DialogResult.No) {
                MessageBox.Show("Error opening file");
            }
        }

        private void playIcon_Click(object sender, EventArgs e)
        {
            if (audioFile == null) {
                MessageBox.Show("Please open an audio file");
                return;
            }
            playIcon.Image = Properties.Resources.play2;
            pauseIcon.Image = Properties.Resources.pause;
            
            outputDevice.Play();
        }

        private void pauseIcon_Click(object sender, EventArgs e)
        {
            playIcon.Image = Properties.Resources.play;
            pauseIcon.Image = Properties.Resources.pause2;
            
            if (outputDevice == null) {
                return;
            }

            outputDevice.Pause();
        }
    }
}
